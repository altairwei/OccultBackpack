// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

using System;
using System.Collections.Generic;
using System.Linq;

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib;
using UndertaleModLib.Models;

namespace SimpleBiggerBackpack;
public class SimpleBiggerBackpack : Mod
{
    public override string Author => "Altair Wei";
    public override string Name => "Simple Bigger Backpack";
    public override string Description => "Now the tailor of Osbrook will sell a bigger backpack.";
    public override string Version => "1.0.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        // Make the backpack only take up 2x3 space in your inventory
        UndertaleSprite s_backpack = Msl.GetSprite("s_inv_travellersbackpack");
        s_backpack.Width = 54;
        s_backpack.Height = 81;
        s_backpack.MarginLeft = 1;
        s_backpack.MarginRight = 53;
        s_backpack.MarginBottom = 80;
        s_backpack.MarginTop = 1;

        UndertaleTexturePageItem t_backpack = s_backpack.Textures[0].Texture;
        t_backpack.TargetX = 4;
        t_backpack.TargetY = 2;
        t_backpack.BoundingWidth = 54;
        t_backpack.BoundingHeight = 81;

        // Make the Backpack hold more stuff
        UndertaleGameObject o_container_backpack = Msl.GetObject("o_container_backpack");
        o_container_backpack.Sprite = Msl.GetSprite("s_container");
        Msl.LoadGML("gml_Object_o_container_backpack_Other_10")
            .MatchFrom("closeButton = scr_adaptiveCloseButtonCreate(id, (depth - 1), 121, 3)")
            .ReplaceBy("closeButton = scr_adaptiveCloseButtonCreate(id, (depth - 1), 229, 3)")
            .Save();
        Msl.LoadGML("gml_Object_o_container_backpack_Other_10")
            .MatchFrom("getbutton = scr_adaptiveTakeAllButtonCreate(id, (depth - 1), 122, 27)")
            .ReplaceBy("getbutton = scr_adaptiveTakeAllButtonCreate(id, (depth - 1), 230, 27)")
            .Save();
        Msl.LoadGML("gml_Object_o_container_backpack_Other_10")
            .MatchFrom("cellsRowSize = 3")
            .ReplaceBy("cellsRowSize = 7")
            .Save();
        Msl.LoadGML("gml_Object_o_container_backpack_Other_10")
            .MatchFrom("scr_inventory_add_cells(id, cellsContainer, cellsRowSize, 4)")
            .ReplaceBy("scr_inventory_add_cells(id, cellsContainer, cellsRowSize, 5)")
            .Save();
        
        // Add backpacks to the goods of the Osbrook tailor
        //Msl.LoadGML("gml_Object_o_npc_tailor_Create_0")
        //    .MatchFrom("ds_list_add(selling_loot_object, 2689, 2.5, 2926, 2.5, 2931, 2.5, 3088, 2.5)")
        //    .InsertBelow("ds_list_add(selling_loot_object, 2936, 2.5)") // backpack: inv_object = 2936
        //    .Save();

        // Init the mini quest of backpack making
        Msl.LoadGML("gml_GlobalScript_scr_init_quests")
            .MatchFrom("scr_quest_init(\"fetchOrmond\", \"\", [\"fetchOrmond_find\", 1, \"fetchOrmond_desc\", []])")
            .InsertBelow("scr_quest_init(\"makeBackpackOrmond\", \"\", [\"makeBackpackOrmond_find\", 1, \"makeBackpackOrmond_desc\", []])")
            .Save();
        Msl.LoadGML("gml_GlobalScript_table_Quests_text").Apply(QeustIterator).Save();

        // Add dialogs to questions map
        Msl.LoadGML("gml_Object_o_npc_tailor_Alarm_1").MatchAll()
            .InsertBelow(ModFiles.GetCode("gml_Object_o_npc_tailor_Alarm_1.gml")).Save();
        // Add dialog texts
        Msl.LoadGML("gml_GlobalScript_table_NPC_Lines").Apply(DialogLinesIterator).Save();

        // Change dialog to add a mini quest
        Msl.LoadGML("gml_GlobalScript_scr_npc_lines_tailor_hub")
            .MatchAll()
            .ReplaceBy(ModFiles.GetCode("gml_GlobalScript_scr_npc_lines_tailor_hub.gml"))
            .Save();

        Msl.AddFunction(ModFiles.GetCode("gml_GlobalScript_scr_npc_tailor_backpack_reward.gml").Peek(), "scr_npc_tailor_backpack_reward");
    }

    private static IEnumerable<string> QeustIterator(IEnumerable<string> input)
    {
        string id = "makeBackpackOrmond";
        string text_en = @"Hold the Tailor's Backpack Making";
        string text_zh = @"裁缝霍特背包制作";
        string makeBackpackOrmond = $"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9));

        id = "makeBackpackOrmond_find";
        text_en = @"Find a Dear Pelt, a Bolt of Cloth, and a Spool of Thread";
        text_zh = @"寻找鹿皮、亚麻布和毛线";
        string makeBackpackOrmond_find = $"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9));

        id = "makeBackpackOrmond_desc";
        text_en = @"Hold the Tailor from Osbrook doesn't sell backpacks, but is willing to help me make a new one. He asked me to get him a dear pelt, a bolt of cloth, and a spool of thread, plus fifty crowns for the craft.";
        text_zh = @"奥斯布鲁克的裁缝霍特不卖背包，但愿意帮我制作一个新的。他让我给他弄一张鹿皮、一卷亚麻布和一轴毛线，再加上五十冠的手工费。";
        string makeBackpackOrmond_desc = $"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9));

        string questend = "\";" + string.Concat(Enumerable.Repeat("text_end;", 12)) + "\"";

        foreach(string item in input)
        {
            if(item.Contains(questend))
            {
                string newItem = item;
                newItem = newItem.Insert(newItem.IndexOf(questend), $"\"{makeBackpackOrmond}\",\"{makeBackpackOrmond_find}\",\"{makeBackpackOrmond_desc}\",");
                yield return newItem;
            }
            else
            {
                yield return item;
            }
        }
    }

    private static IEnumerable<string> DialogLinesIterator(IEnumerable<string> input)
    {
        string id = "tailor_backpack_pc";
        string text_en = @"How come you don't sell backpacks here?";
        string text_zh = @"你这里怎么没有卖背包？";
        string tailor_backpack_pc = $"{id};any;tailor;any;;Osbrook;{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9));

        id = "tailor_backpack_quest";
        text_en = @"You guess?";
        text_zh = @"你猜？";
        string tailor_backpack_quest = $"{id};any;tailor;any;;Osbrook;{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9));

        id = "backpack_materials_collected";
        text_en = @"I've collected all the materials for making a backpack.";
        text_zh = @"制作背包的材料我都收集好了。";
        string backpack_materials_collected = $"{id};any;tailor;any;;Osbrook;{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9));

        // 参考曼郡药师，出去一趟回来。
        id = "tailor_making_backpack";
        text_en = @"OK, backpacks will be ready for you in a minute.";
        text_zh = @"行，马上就为你做好背包。";
        string tailor_making_backpack = $"{id};any;tailor;any;;Osbrook;{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9));

        string dialog_end = "\";;;;;;" + string.Concat(Enumerable.Repeat("// FOLLOWERS - MASTER;", 12)) + "\"";

        foreach(string item in input)
        {
            if(item.Contains(dialog_end))
            {
                string newItem = item;
                newItem = newItem.Insert(newItem.IndexOf(dialog_end), $"\"{tailor_backpack_pc}\",\"{tailor_backpack_quest}\",\"{tailor_making_backpack}\",");
                yield return newItem;
            }
            else
            {
                yield return item;
            }
        }

    }
}
