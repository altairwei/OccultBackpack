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
        // Create Deerskin Backpack
        UndertaleSprite s_deerskinbackpack = Msl.GetSprite("s_inv_deerskinbackpack");
        s_deerskinbackpack.IsSpecialType = true;
        s_deerskinbackpack.SVersion = 3;
        s_deerskinbackpack.Width = 54;
        s_deerskinbackpack.Height = 81;
        s_deerskinbackpack.MarginLeft = 1;
        s_deerskinbackpack.MarginRight = 53;
        s_deerskinbackpack.MarginBottom = 80;
        s_deerskinbackpack.MarginTop = 1;

        UndertaleGameObject o_inv_deerskinbackpack = Msl.AddObject(
            name: "o_inv_deerskinbackpack",
            spriteName: "s_inv_deerskinbackpack", 
            parentName: "o_inv_backpack",
            isVisible: true, 
            isPersistent: true, 
            isAwake: true
        );

        Msl.InjectTableItemLocalization(
            oName: "deerskinbackpack",
            dictName: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Deerskin Backpack"},
                {ModLanguage.Chinese, "鹿皮背包"}
            },
            dictID: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "A deerskin backpack that's smaller and holds more stuff."},
                {ModLanguage.Chinese, "一个更加小巧能装更多的东西的鹿皮背包。"}
            },
            dictDescription: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "The brainchild of the Osbrook tailor. While the style is similar to a traditional backpack, the deerskin backpack is more compact and can hold more."},
                {ModLanguage.Chinese, "奥斯布鲁克裁缝的呕心沥血之作。虽然款式与传统背包差不多，但鹿皮背包更加小巧，能装更多的东西。"}
            }
        );

        o_inv_deerskinbackpack.ApplyEvent(ModFiles, 
            new MslEvent("gml_Object_o_inv_deerskinbackpack_Other_24.gml", EventType.Other, 24)
        );

        // Make the Backpack hold more stuff
        UndertaleGameObject o_container_deerskinbackpack = Msl.AddObject(
            name: "o_container_deerskinbackpack",
            spriteName: "s_container", 
            parentName: "o_container_backpack",
            isVisible: true, 
            isAwake: true
        );

        o_container_deerskinbackpack.ApplyEvent(ModFiles, 
            new MslEvent("gml_Object_o_container_deerskinbackpack_Other_10.gml", EventType.Other, 10)
        );

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
