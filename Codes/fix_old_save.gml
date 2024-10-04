// var _map = ds_map_find_value(global.questsDataMap, "makeMagicBackpack")
// var _tasks = ds_map_find_value(_map, "Tasks")

// ds_list_add(_tasks, "makeMagicBackpack_find_materials", 1, -1, "makeMagicBackpack_find_materials_desc", __dsDebuggerListCreate())
// ds_list_mark_as_list(_tasks, (ds_list_size(_tasks) - 1))

// ds_list_add(_tasks, "makeMagicBackpack_reward", 1, -1, "makeMagicBackpack_reward_desc", __dsDebuggerListCreate())
// ds_list_mark_as_list(_tasks, (ds_list_size(_tasks) - 1))

if (global.add_occult_backpack_quests)
{
    scr_quest_init("makeBackpackOrmond", "", [
        "makeBackpackOrmond_find", 1, "makeBackpackOrmond_desc", [],
        "makeBackpackOrmond_reward", 1, "makeBackpackOrmond_reward_desc", []
    ])

    var _RottenWillow = scr_glmap_getLocation("RottenWillow")
    scr_quest_init("makeMagicBackpack", "", [
        "makeMagicBackpack_rotten_willow", 1, "makeMagicBackpack_rotten_willow_desc", [_RottenWillow.x, _RottenWillow.y],
        "makeMagicBackpack_find_materials", 1, "makeMagicBackpack_find_materials_desc", [],
        "makeMagicBackpack_reward", 1, "makeMagicBackpack_reward_desc", []
    ])

    audio_play_sound(snd_quest_update, 3, 0)
}

