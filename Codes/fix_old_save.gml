var _map = ds_map_find_value(global.questsDataMap, "makeMagicBackpack")
var _tasks = ds_map_find_value(_map, "Tasks")

ds_list_add(_tasks, "makeMagicBackpack_find_materials", 1, -1, "makeMagicBackpack_find_materials_desc", __dsDebuggerListCreate())
ds_list_mark_as_list(_tasks, (ds_list_size(_tasks) - 1))

ds_list_add(_tasks, "makeMagicBackpack_reward", 1, -1, "makeMagicBackpack_reward_desc", __dsDebuggerListCreate())
ds_list_mark_as_list(_tasks, (ds_list_size(_tasks) - 1))

audio_play_sound(snd_quest_update, 3, 0)
