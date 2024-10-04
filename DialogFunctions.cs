
using ModShardLauncher;
using ModShardLauncher.Mods;

namespace OccultBackpack;

public class DialogFunctions
{
    public static void LowcreyDialogFunctionsPatching() 
    {
        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyShouldStartOcultBackpackQuest",
            codeAsString: @"function scr_occultbackpack_lowcreyShouldStartOcultBackpackQuest()
{
    return scr_quest_get_started(""makeMagicBackpack"")
                && scr_dialogue_uncomplete(""magicbackpack_materials_to_repair_lowcrey_pc_4"")
}");

        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyCheckRottenWillowReputationLow",
            codeAsString: @"function scr_occultbackpack_lowcreyCheckRottenWillowReputationLow()
{
    var _reputation = 0
    with (o_npc_enchanter)
        _reputation = scr_globaltile_get(""reputation"", village_xy[0], village_xy[1])
    return _reputation < 3000 && scr_atr(""nameKey"") != ""Jonna""; // < benevolence
}");

        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyCheckRottenWillowReputationJonna",
            codeAsString: @"function scr_occultbackpack_lowcreyCheckRottenWillowReputationJonna()
{
    var _reputation = 0
    with (o_npc_enchanter)
        _reputation = scr_globaltile_get(""reputation"", village_xy[0], village_xy[1])
    return _reputation < 3000 && scr_atr(""nameKey"") == ""Jonna"";
}");

        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyCheckRottenWillowReputationHigh",
            codeAsString: @"function scr_occultbackpack_lowcreyCheckRottenWillowReputationHigh()
{
    var _reputation = 0
    with (o_npc_enchanter)
        _reputation = scr_globaltile_get(""reputation"", village_xy[0], village_xy[1])
    return _reputation >= 3000 && scr_dialogue_complete(""magicbackpack_inquire_lowcrey_pc_2a"")
}");

        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyCheckRottenWillowReputationHighFirstInquiry",
            codeAsString: @"function scr_occultbackpack_lowcreyCheckRottenWillowReputationHighFirstInquiry()
{
    var _reputation = 0
    with (o_npc_enchanter)
        _reputation = scr_globaltile_get(""reputation"", village_xy[0], village_xy[1])
    return _reputation >= 3000 && scr_dialogue_uncomplete(""magicbackpack_inquire_lowcrey_pc_2a"")
}");

        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyFindMaterialsForRepair",
            codeAsString: @"function scr_occultbackpack_lowcreyFindMaterialsForRepair()
{
    scr_dialogue_complete(""magicbackpack_materials_to_repair_lowcrey_pc_4"", true)
    scr_quest_next_target(""makeMagicBackpack"")
    scr_close_dialog()
}");

        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyShouldStartMakingBackpackQuest",
            codeAsString: @"function scr_occultbackpack_lowcreyShouldStartMakingBackpackQuest()
{
    return scr_dialogue_complete(""magicbackpack_materials_to_repair_lowcrey_pc_4"")
        && scr_quest_get_progress(""makeMagicBackpack"", ""makeMagicBackpack_rotten_willow"") == 1
        && scr_quest_get_progress(""makeMagicBackpack"", ""makeMagicBackpack_find_materials"") == 0
        && scr_instance_exists_item(o_inv_masterpiecebackpack) && scr_instance_exists_item(o_inv_ruby)
        && scr_instance_exists_item(o_inv_emerald) && (o_player.gold > 1000)
}");

        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyBackpackEmpty",
            codeAsString: @"function scr_occultbackpack_lowcreyBackpackEmpty()
{
    with (o_inv_masterpiecebackpack)
    {
        var _contentList = ds_map_find_value(data, ""lootList"")
        var _size = ds_list_size(_contentList)
    }

    return _size == 0;
}");

        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyCaravanStashAvailable",
            codeAsString: @"function scr_occultbackpack_lowcreyCaravanStashAvailable()
{
    return !__is_undefined(ds_map_find_value(global.containersLootDataMap, ""caravan_stash""))
}");

        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyRepairingOccultBackpack",
            codeAsString: @"function scr_occultbackpack_lowcreyRepairingOccultBackpack()
{
    scr_delete_item(o_inv_ruby)
    scr_delete_item(o_inv_emerald)

    scr_gold_write_off(1000)
    scr_characterGoldSpend(""spentDialogues"", 1000)

    scr_delete_item(o_inv_masterpiecebackpack)

    with (o_npc_enchanter)
    {
        var _timestamp = scr_timeGetTimestamp()
        scr_npc_set_global_info(""reward_occult_backpack_timestamp"", _timestamp)
    }

    scr_quest_next_target(""makeMagicBackpack"")
    scr_dialogue_complete(""magicbackpack_ready_to_make_lowcrey_4"", true)

    scr_close_dialog()
}");

        Msl.AddFunction(
            name: "scr_occultbackpack_lowcreyOccultBackpackReward",
            codeAsString: @"function scr_occultbackpack_lowcreyOccultBackpackReward()
{
    if (argument_count == 1)
    {
        with (o_npc_enchanter)
        {
            var _timestamp = scr_npc_get_global_info(""reward_occult_backpack_timestamp"")
            var _daysPassed = scr_timeGetPassed(_timestamp, ""days"")
        }

        return _daysPassed >= 2 && scr_dialogue_uncomplete(""magicbackpack_ready_to_make_pc_6"")
            && scr_quest_get_progress(""makeMagicBackpack"", ""makeMagicBackpack_find_materials"") == 1
            &&  scr_quest_get_progress(""makeMagicBackpack"", ""makeMagicBackpack_reward"") == 0
    }
    else
    {
        with (scr_guiCreateContainer(global.guiBaseContainerVisible, o_reward_container))
            scr_inventory_add_item(o_inv_magicbackpack)

        scr_dialogue_complete(""magicbackpack_ready_to_make_pc_6"", true)
        scr_quest_set_progress(""makeMagicBackpack"", ""makeMagicBackpack_reward"", 1)
        scr_quest_set_complete(""makeMagicBackpack"")

        scr_close_dialog()
    }
}");

    }
}