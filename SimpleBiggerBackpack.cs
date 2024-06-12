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

        // Init the mini quest of backpack making (only works in a new game save)
        Msl.LoadGML("gml_GlobalScript_scr_init_quests")
            .MatchFrom("scr_quest_init(\"fetchOrmond\", \"\", [\"fetchOrmond_find\", 1, \"fetchOrmond_desc\", []])")
            .InsertBelow("scr_quest_init(\"makeBackpackOrmond\", \"\", [\"makeBackpackOrmond_find\", 1, \"makeBackpackOrmond_desc\", []])")
            .Save();
        
        // Add quest name and description
        Msl.LoadGML("gml_GlobalScript_table_Quests_text").Apply(QeustIterator).Save();

        // Add dialogs to questions map
        Msl.LoadGML("gml_Object_o_npc_tailor_Alarm_1").MatchAll()
            .InsertBelow(ModFiles.GetCode("gml_Object_o_npc_tailor_Alarm_1.gml")).Save();
        // Add dialog texts
        Msl.InjectTableDialogLocalization(
            new LocalizationSentence(
                "tailor_backpack_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "How come you don't sell backpacks here?"},
                    {ModLanguage.Chinese, "你这里怎么没有卖背包？"}
                }
            ),
            new LocalizationSentence(
                "tailor_backpack_inquiry",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, string.Join("#", new string[] {
                        "A backpack? Ha ha ha ha. Who's going to buy a backpack when Osbrook is this big?",
                        "The war is so chaotic right now, no one dares to travel far, so even more so, no one would buy one. ",
                        "Aldor's traditional travelling backpacks are so big and bulky that they're not practical at all, and only those inexperienced rookies would buy them. I'm the one who won't make this rubbish."
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "背包？哈哈哈哈。奥村就这么大点，谁会买背包啊？况且现在战争这么乱，谁也不敢出远门，更没人买了。",
                        "奥尔多传统的旅行背包又大又笨重，根本不实用，只有那些没经验的菜鸟会买。我才会不会制作这种垃圾。"
                    })}
                }
            ),
            new LocalizationSentence(
                "tailor_backpack_inquiry_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Does that mean you can make a small and functional backpack?"},
                    {ModLanguage.Chinese, "那意思是你能制作一个小巧又实用的背包？"}
                }
            ),
            new LocalizationSentence(
                "tailor_backpack_quest",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, string.Join("#", new string[] {
                        "Since you've asked, I must demonstrate my ancestral craft. But at the moment I don't have the right materials on hand.",
                        "This way, you find a deer pelt, a bolt of cloth, and a spool of thread. I'll only charge you 50 craft fee to make you a deerskin backpack."
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "既然你都这么问了，我必须展示祖传的手艺了。但目前我手上没有合适的材料。",
                        "这样，你找到一张鹿皮，一卷布，以及一轴毛线。我只收你 50 冠手工费，给你制作一个鹿皮背包。"
                    })}
                }
            ),
            new LocalizationSentence(
                "backpack_materials_collected",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I've collected all the materials for making a backpack."},
                    {ModLanguage.Chinese, "制作背包的材料我都收集好了。"}
                }
            ),
            new LocalizationSentence(
                "tailor_making_backpack",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "OK, backpacks will be ready for you in a minute."},
                    {ModLanguage.Chinese, "行，马上就为你做好背包。"}
                }
            )
        );

        // Change dialog to add a mini quest
        Msl.AddFunction(ModFiles.GetCode("gml_GlobalScript_scr_npc_tailor_backpack_reward.gml"), "scr_npc_tailor_backpack_reward");
        Msl.LoadGML("gml_GlobalScript_scr_npc_miniquest_item_tailor")
            .MatchAll()
            .ReplaceBy(ModFiles.GetCode("gml_GlobalScript_scr_npc_miniquest_item_tailor.gml"))
            .Save();

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
}
