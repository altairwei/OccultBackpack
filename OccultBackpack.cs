// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib;
using UndertaleModLib.Models;

namespace OccultBackpack;
public class OccultBackpack : Mod
{
    public override string Author => "Altair";
    public override string Name => "Occult Backpack";
    public override string Description => "A new artifact, and the series of quests surrounding its creation.";
    public override string Version => "2.0.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        // Create the container of Backpack
        UndertaleGameObject o_container_masterpiecebackpack = Msl.AddObject(
            name: "o_container_masterpiecebackpack",
            spriteName: "s_container_masterpiecebackpack",
            parentName: "o_container_backpack",
            isVisible: true, 
            isAwake: true
        );

        o_container_masterpiecebackpack.ApplyEvent(ModFiles, 
            new MslEvent("gml_Object_o_container_masterpiecebackpack_Other_10.gml", EventType.Other, 10)
        );

        int index = DataLoader.data.GameObjects.IndexOf(DataLoader.data.GameObjects.First(x => x.Name.Content == "o_container_backpack"));
        Msl.LoadGML("gml_GlobalScript_scr_adaptiveCloseButtonCreate")
            .MatchFrom($"                case {index}:")
            .InsertBelow("                case o_container_masterpiecebackpack:")
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_adaptiveTakeAllButtonCreate")
            .MatchFrom($"                case {index}:")
            .InsertBelow("                case o_container_masterpiecebackpack:")
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_adaptiveMenusGetOffset")
            .MatchFrom($"        case {index}:")
            .InsertBelow("        case o_container_masterpiecebackpack:")
            .Save();

        Msl.LoadGML("gml_Object_o_inv_slot_Mouse_4")
            .MatchFrom($"                    case {index}:")
            .InsertBelow("                    case o_container_masterpiecebackpack:")
            .Save();

        Msl.LoadGML("gml_Object_o_inv_slot_Other_13")
            .MatchFrom($"                case {index}:")
            .InsertBelow("                case o_container_masterpiecebackpack:")
            .Save();

        /* Don't patch gml_GlobalScript_scr_adaptiveMenusPositionUpdate,
         * I moved the relevant codes to gml_Object_o_container_masterpiecebackpack_Other_10.gml
        int index_o_container = DataLoader.data.GameObjects.IndexOf(DataLoader.data.GameObjects.First(x => x.Name.Content == "o_container"));
        Msl.LoadGML("gml_GlobalScript_scr_adaptiveMenusPositionUpdate")
            .MatchFrom($"        case {index_o_container}:")
            .InsertBelow("        case o_container_masterpiecebackpack:")
            .Save();

        // FIXME: Make Character Menu Crash!
        Msl.LoadGML("gml_GlobalScript_scr_adaptiveMenusPositionUpdate")
            .MatchFrom("            scr_guiLayoutOffsetUpdate(id, ((-sprite_width) * (!active)))")
            //.ReplaceBy("            scr_msl_debug(string(id) + \", \" + string(-sprite_width) + \" * \" + string(!active))")
            .ReplaceBy("")
            .Peek()
            .Save();
        */

        Msl.LoadGML("gml_GlobalScript_scr_can_replace_item")
            .MatchAll()
            .ReplaceBy(ModFiles, "gml_GlobalScript_scr_can_replace_item.gml")
            .Save();

        // Create the Masterpiece Backpack
        UndertaleSprite s_masterpiecebackpack = Msl.GetSprite("s_inv_masterpiecebackpack");
        s_masterpiecebackpack.CollisionMasks.RemoveAt(0);
        s_masterpiecebackpack.IsSpecialType = true;
        s_masterpiecebackpack.SVersion = 3;
        s_masterpiecebackpack.Width = 54;
        s_masterpiecebackpack.Height = 81;
        s_masterpiecebackpack.MarginLeft = 1;
        s_masterpiecebackpack.MarginRight = 53;
        s_masterpiecebackpack.MarginBottom = 80;
        s_masterpiecebackpack.MarginTop = 1;

        UndertaleGameObject o_inv_masterpiecebackpack = Msl.AddObject(
            name: "o_inv_masterpiecebackpack",
            spriteName: "s_inv_masterpiecebackpack", 
            parentName: "o_inv_backpack",
            isVisible: true, 
            isPersistent: true,
            isAwake: true
        );

        Msl.InjectTableConsumableParameters(
            metaGroup: Msl.ConsumParamMetaGroup.TOOLS,
            id: "masterpiecebackpack",
            Cat: Msl.ConsumParamCategory.bag,
            Material: Msl.ConsumParamMaterial.leather2,
            Weight: Msl.ConsumParamWeight.Heavy,
            tags: Msl.ConsumParamTags.special,
            Price: 550, EffPrice: 325
        );

        Msl.InjectTableItemLocalization(
            oName: "masterpiecebackpack",
            dictName: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Tailor's Backpack"},
                {ModLanguage.Chinese, "裁缝的背包"}
            },
            dictID: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "A exquisite backpack that's smaller and holds more stuff."},
                {ModLanguage.Chinese, "一个更加小巧，但能装更多东西的背包。"}
            },
            dictDescription: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "The masterpiece of the Osbrook tailor. While the style is similar to a traditional backpack, this one is more compact and can hold more."},
                {ModLanguage.Chinese, "奥斯布鲁克裁缝的呕心沥血之作。虽然款式与传统背包差不多，但这个背包更加小巧，能装更多的东西。"}
            }
        );

        o_inv_masterpiecebackpack.ApplyEvent(ModFiles,
            new MslEvent("gml_Object_o_inv_masterpiecebackpack_Create_0.gml", EventType.Create, 0),
            new MslEvent("gml_Object_o_inv_masterpiecebackpack_Other_24.gml", EventType.Other, 24)
        );

        // Create the loot object of Backpack
        UndertaleGameObject o_loot_masterpiecebackpack = Msl.AddObject(
            name: "o_loot_masterpiecebackpack",
            spriteName: "s_loot_travellersbackpack", 
            parentName: "o_loot_backpack",
            isVisible: true, 
            isAwake: true
        );

        o_loot_masterpiecebackpack.ApplyEvent(ModFiles, 
            new MslEvent("gml_Object_o_loot_masterpiecebackpack_Create_0.gml", EventType.Create, 0)
        );

        // Add the interaction between Backpack and other items in the o_inv_slot
        // FIXME: We had to replace the entire file, otherwise it would have resulted in messed up lines of code
        Msl.LoadGML("gml_Object_o_inv_slot_Other_21")
            .MatchAll()
            .ReplaceBy(ModFiles, "gml_Object_o_inv_slot_Other_21.gml")
            .Save();

        Msl.LoadGML("gml_GlobalScript_scr_gold_count")
            .MatchFrom("            if (owner.object_index == o_inventory || owner.object_index == o_container_backpack)")
            .ReplaceBy("            if (owner.object_index == o_inventory || owner.object_index == o_container_backpack || object_is_ancestor(owner.object_index, o_container_backpack))")
            .Save();
        Msl.LoadGML("gml_GlobalScript_scr_gold_count")
            .MatchFrom("            if (owner.object_index == o_inventory || owner.object_index == o_container_backpack)")
            .ReplaceBy("            if (owner.object_index == o_inventory || owner.object_index == o_container_backpack || object_is_ancestor(owner.object_index, o_container_backpack))")
            .Save();
        Msl.LoadGML("gml_GlobalScript_scr_gold_count")
            .MatchFrom("                if (owner.object_index == o_inventory || owner.object_index == o_container_backpack)")
            .ReplaceBy("                if (owner.object_index == o_inventory || owner.object_index == o_container_backpack || object_is_ancestor(owner.object_index, o_container_backpack))")
            .Save();

        Msl.LoadGML("gml_Object_o_inv_slot_Destroy_0")
            .MatchFrom("    if (owner.object_index != o_container_backpack)")
            .ReplaceBy("    if (owner.object_index != o_container_backpack && !object_is_ancestor(owner.object_index, o_container_backpack))")
            .Save();

        // FIXME: We had to replace the entire file, otherwise it would have resulted in messed up lines of code
        /*
        Msl.LoadGML("gml_GlobalScript_scr_notify")
            .MatchAll()
            .ReplaceBy(ModFiles, "gml_GlobalScript_scr_notify.gml")
            .Save();
        */

        // Create the Magic Backpack
        UndertaleSprite s_magicbackpack = Msl.GetSprite("s_inv_magicbackpack");
        s_magicbackpack.CollisionMasks.RemoveAt(0);
        s_magicbackpack.IsSpecialType = true;
        s_magicbackpack.SVersion = 3;
        s_magicbackpack.Width = 54;
        s_magicbackpack.Height = 81;
        s_magicbackpack.MarginLeft = 1;
        s_magicbackpack.MarginRight = 53;
        s_magicbackpack.MarginBottom = 80;
        s_magicbackpack.MarginTop = 1;

        UndertaleGameObject o_inv_magicbackpack = Msl.AddObject(
            name: "o_inv_magicbackpack",
            spriteName: "s_inv_magicbackpack",
            parentName: "o_inv_backpack",
            isVisible: true, 
            isPersistent: true,
            isAwake: true
        );

        Msl.InjectTableItemLocalization(
            oName: "magicbackpack",
            dictName: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "Occult Backpack"},
                {ModLanguage.Chinese, "玄秘背包"}
            },
            dictID: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "A backpack that connects to a specific space."},
                {ModLanguage.Chinese, "一个能联通特定空间的背包。"}
            },
            dictDescription: new Dictionary<ModLanguage, string>() {
                {ModLanguage.English, "L'Owcrey fixes occult powers on the backpack and links it to your caravan stash as you requested.##Opening the Occult Backpack is equivalent to opening the caravan stash."},
                {ModLanguage.Chinese, "埃欧科里按照你的要求在背包上将玄秘力量固定下来，并与你的大篷车货堆联通。##打开魔法背包等同于打开大篷车货堆。"}
            }
        );

        Msl.InjectTableConsumableParameters(
            metaGroup: Msl.ConsumParamMetaGroup.LEGENDARYTREASURES,
            id: "magicbackpack",
            Cat: Msl.ConsumParamCategory.treasure,
            Material: Msl.ConsumParamMaterial.leather2,
            Weight: Msl.ConsumParamWeight.Heavy,
            tags: Msl.ConsumParamTags.special,
            Price: 2600, EffPrice: 325
        );

        o_inv_magicbackpack.ApplyEvent(ModFiles,
            new MslEvent("gml_Object_o_inv_magicbackpack_Create_0.gml", EventType.Create, 0),
            new MslEvent("gml_Object_o_inv_magicbackpack_Other_24.gml", EventType.Other, 24)
        );

        Msl.AddNewEvent("o_stash_inventory", ModFiles.GetCode(
            "gml_Object_o_stash_inventory_Destroy_0.gml"), EventType.Destroy, 0);
        Msl.AddNewEvent("o_stash_inventory", ModFiles.GetCode(
            "gml_Object_o_stash_inventory_Step_0.gml"), EventType.Step, 0);

        AddActionLogs();

        // DELETE ME!
        Msl.LoadGML("gml_Object_o_player_KeyPress_115")
            .MatchAll()
            .InsertBelow(@"with (o_inventory)
{
    scr_delete_item(o_inv_masterpiecebackpack)
    scr_inventory_add_item(o_inv_magicbackpack)
}")
            .Save();

        // Create the corresponding loot object of magic backpack
        UndertaleGameObject o_loot_magicbackpack = Msl.AddObject(
            name: "o_loot_magicbackpack",
            spriteName: "s_loot_travellersbackpack", 
            parentName: "o_loot_backpack",
            isVisible: true, 
            isAwake: true
        );

        o_loot_magicbackpack.ApplyEvent(ModFiles, 
            new MslEvent("gml_Object_o_loot_magicbackpack_Create_0.gml", EventType.Create, 0)
        );

        // Init the mini quest of backpack making (only works in a new game save)
        Msl.LoadGML("gml_GlobalScript_scr_init_quests")
            .MatchFrom("scr_quest_init(\"fetchOrmond\", \"\", [\"fetchOrmond_find\", 1, \"fetchOrmond_desc\", []])")
            .InsertBelow(ModFiles, "tailor_quests_init.gml")
            .Save();
        
        // Add quest name and description
        AddQeusts();

        // Add dialogs to questions map
        Msl.LoadGML("gml_Object_o_npc_tailor_Alarm_1").MatchAll()
            .InsertBelow(ModFiles.GetCode("gml_Object_o_npc_tailor_Alarm_1.gml")).Save();
        // Add dialog texts
        AddDialogs();

        // Change dialog to add a mini quest
        Msl.AddFunction(ModFiles.GetCode("gml_GlobalScript_scr_npc_tailor_backpack_reward.gml"), "scr_npc_tailor_backpack_reward");
        Msl.LoadGML("gml_GlobalScript_scr_npc_miniquest_item_tailor")
            .MatchAll()
            .ReplaceBy(ModFiles.GetCode("gml_GlobalScript_scr_npc_miniquest_item_tailor.gml"))
            .Save();

        ExportTable("gml_GlobalScript_table_log_text");
    }

    private static void AddDialogs()
    {
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
                        "A backpack? Ha ha. Who's going to buy a backpack when Osbrook is this big?",
                        "The war is so chaotic right now, no one dares to travel far, so even more so, no one would buy one. ",
                        "Aldor's traditional travelling backpacks are so big and bulky that they're not practical at all, and only those inexperienced rookies would buy them. I'm the one who won't make this rubbish."
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "背包？哈哈。奥村就这么大点，谁会买背包啊？况且现在战争这么乱，谁也不敢出远门，更没人买了。",
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
                        "This way, you find a pelt, a bolt of cloth, and a spool of thread. I'll only charge you 50 craft fee to make you a exquisite backpack.",
                        "Remember, don't try my patience with rubbish pelts, like those of dogs, horses or rabbits..."
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "既然你都这么问了，我必须展示祖传的手艺了。但目前我手上没有合适的材料。",
                        "这样，你找到一张动物毛皮，一卷布，以及一轴毛线。我只收你 50 冠手工费，给你制作一个精致的背包。",
                        "记住，别用狗皮、马皮、兔皮这些垃圾来试探我的耐心。"
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
                    {ModLanguage.English, "Get them out so I can see them.#Pelt... Cloth... And a spool of thread... All materials are ready!#Okay, but this backpack is going to take a bit of work to make, so you can come back to me tomorrow."},
                    {ModLanguage.Chinese, "拿出来让我看看。#皮毛...布料...还有一卷毛线...材料齐全了！#行吧，但这个背包还要费点功夫来做，你明天再来找我吧。"}
                }
            ),
            new LocalizationSentence(
                "backpack_claim",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Is my backpack done?"},
                    {ModLanguage.Chinese, "我的背包做好了吗？"}
                }
            ),
            new LocalizationSentence(
                "tailor_backpack_reward",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Done! There you go."},
                    {ModLanguage.Chinese, "做好了！给你。"}
                }
            ),
            // TODO: Refine me
            new LocalizationSentence(
                "masterpiecebackpack_is_good_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "What a great backpack you have made!"},
                    {ModLanguage.Chinese, "你制作的背包真是太好用了！"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_rumor",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "magicbackpack_rumor......."},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "是吗？你们这些人只有见到了真正的本事才会心服口服。",
                        "不过我这背包算不了什么，有一次，我听卖皮毛的猎人聊起过一个神奇的物件。",
                        "他在林子里狩猎时，远远地看见一个脸上涂满花纹的人从一个小小的手提旅行箱里拿出了一大堆东西。",
                        "蒸锅、柴火、帐篷和铺盖卷。迅速地搭起了一个营地。他怀疑那个旅行箱被施了魔法。",
                        "要是有了这个物件，你们这些雇佣兵永远不用担心战利品带不走了。"
                    })}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_rumor_accept",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Wow! That's amazing and worth exploring!"},
                    {ModLanguage.Chinese, "我靠！还有这种魔法，你快告诉去找谁给我背包施施法！"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_rumor_reject",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I don't believe any of this bullshit from you at all."},
                    {ModLanguage.Chinese, "我根本不相信你的这些鬼扯。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_where_to_find",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Wow! That's amazing and worth exploring!"},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "如果你对这神奇的物件真的感兴趣，我建议你去烂柳旅店打探下消息。",
                        "你问烂柳旅店在哪？嗯...真是个好问题，我也没去过，只是听说在曼郡的北边。"
                    })}
                }
            )
        );
    }

    private static void AddQeusts()
    {
        List<string> stringList = new List<string>();

        string id = "makeBackpackOrmond";
        string text_en = @"Hold the Tailor's Backpack Making";
        string text_zh = @"裁缝霍特背包制作";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeBackpackOrmond_find";
        text_en = @"Find a Pelt, a Bolt of Cloth, and a Spool of Thread";
        text_zh = @"寻找毛皮、亚麻布和毛线";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeBackpackOrmond_desc";
        text_en = @"Hold the Tailor from Osbrook doesn't sell backpacks, but is willing to help me make a new one. He asked me to get him a pelt, a bolt of cloth, and a spool of thread, plus fifty crowns for the craft.";
        text_zh = @"奥斯布鲁克的裁缝霍特不卖背包，但愿意帮我制作一个新的。他让我给他弄一张毛皮、一卷亚麻布和一轴毛线，再加上五十冠的手工费。";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeBackpackOrmond_reward";
        text_en = "Claim Your Backpack";
        text_zh = "领取背包";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeBackpackOrmond_reward_desc";
        text_en = "Hold, the tailor, says it takes quite a bit of work to make a fine backpack, and he's asked me to come back this time tomorrow to pick it up.";
        text_zh = "裁缝霍特说制作一个精良的背包需要花费不少功夫，他让我明天再来取。";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeMagicBackpack";
        text_en = "Legendary Backpack";
        text_zh = "传说中的背包";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeMagicBackpack_rotten_willow";
        text_en = "Go to the Rotten Willow Tavern and look for clues.";
        text_zh = "去烂柳旅店寻找线索";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        // TODO: Refine
        id = "makeMagicBackpack_rotten_willow_desc";
        text_en = "makeMagicBackpack_rotten_willow_desc.....";
        text_zh = "裁缝告诉你一个传闻，有一种魔法背包，可以直接联通固定的房间，这样根本不怕东西放不下了。他告诉你可以去烂柳旅店打探下消息。";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        string questend = ";" + string.Concat(Enumerable.Repeat("text_end;", 12));

        List<string> quest_table = ModLoader.GetTable("gml_GlobalScript_table_Quests_text");
        quest_table.InsertRange(quest_table.IndexOf(questend), stringList);
        ModLoader.SetTable(quest_table, "gml_GlobalScript_table_Quests_text");
    }

    private static void AddActionLogs()
    {
        List<string> logList = new List<string>();
        string id = "openMagicBackpackInvalid";
        string text_en = @"~w~$~/~ try to open $, but find that it doesn't work.";
        string text_zh = @"~w~$~/~尝试打开$，但发现不行。";
        logList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        logList.Add("searchBackpack;~w~$~/~ обыскивает контейнер ($).;~w~$~/~ searches the $.;~w~$~/~翻了翻$。;~w~$~/~ durchsucht $.;~w~$~/~ revisa: $.;~w~$~/~ fouille $.;~w~$~/~ perquisisce $.;~w~$~/~ vasculha $.;~w~$~/~ przeszukuje $.;~w~$~/~ araştırdı: $.;~w~$~/~ は $ を調べた;~w~$~/~이(가) $을(를) 뒤졌다.;");

        string logtextend = ";" + string.Concat(Enumerable.Repeat("text_end;", 12));

        List<string> log_table = ModLoader.GetTable("gml_GlobalScript_table_log_text");
        log_table.InsertRange(log_table.IndexOf(logtextend), logList);
        ModLoader.SetTable(log_table, "gml_GlobalScript_table_log_text");
    }

    private static void ExportTable(string table)
    {
        DirectoryInfo dir = new("ModSources/OccultBackpack/tmp");
        if (!dir.Exists) dir.Create();
        List<string>? lines = ModLoader.GetTable(table);
        if (lines != null)
        {
            File.WriteAllLines(
                Path.Join(dir.FullName, Path.DirectorySeparatorChar.ToString(), table + ".tsv"),
                lines.Select(x => string.Join('\t', x.Split(';')))
            );
        }
    }
}
