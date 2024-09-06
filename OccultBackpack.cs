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
    public override string Version => "1.0.2";
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
            Price: 750, EffPrice: 325
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

        Msl.InjectTableConsumableParameters(
            metaGroup: Msl.ConsumParamMetaGroup.LEGENDARYTREASURES,
            id: "magicbackpack",
            Cat: Msl.ConsumParamCategory.treasure,
            Material: Msl.ConsumParamMaterial.leather2,
            Weight: Msl.ConsumParamWeight.Heavy,
            tags: Msl.ConsumParamTags.special,
            Price: 3000, EffPrice: 325
        );

        o_inv_magicbackpack.ApplyEvent(ModFiles,
            new MslEvent("gml_Object_o_inv_magicbackpack_Create_0.gml", EventType.Create, 0),
            new MslEvent("gml_Object_o_inv_magicbackpack_Other_24.gml", EventType.Other, 24)
        );

        Msl.AddNewEvent("o_stash_inventory", ModFiles.GetCode(
            "gml_Object_o_stash_inventory_Destroy_0.gml"), EventType.Destroy, 0);
        Msl.AddNewEvent("o_stash_inventory", ModFiles.GetCode(
            "gml_Object_o_stash_inventory_Step_0.gml"), EventType.Step, 0);

        // DELETE ME!
        /*
        Msl.LoadGML("gml_Object_o_player_KeyPress_115") // F4
            .MatchAll()
            .InsertBelow(ModFiles, "fix_old_save.gml")
            .Save();
        */

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

        // Add dialogs to questions map
        Msl.LoadGML("gml_Object_o_npc_tailor_Alarm_1").MatchAll()
            .InsertBelow(ModFiles.GetCode("gml_Object_o_npc_tailor_Alarm_1.gml")).Save();

        // Change dialog to add a mini quest
        Msl.AddFunction(ModFiles.GetCode("gml_GlobalScript_scr_npc_tailor_backpack_reward.gml"), "scr_npc_tailor_backpack_reward");
        Msl.LoadGML("gml_GlobalScript_scr_npc_miniquest_item_tailor")
            .MatchAll()
            .ReplaceBy(ModFiles.GetCode("gml_GlobalScript_scr_npc_miniquest_item_tailor.gml"))
            .Save();

        // Add function for Occult Backpack quests
        DialogFunctions.LowcreyDialogFunctionsPatching();

        // Add localization
        Localization.ItemsPatching();
        Localization.DialogLinesPatching();
        Localization.QeustsPatching();
        Localization.ActionLogsPatching();
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
