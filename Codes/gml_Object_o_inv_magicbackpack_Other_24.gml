var is_open = false

var list = ds_map_find_value(global.containersLootDataMap, "caravan_stash")
if (!__is_undefined(list))
    is_open = true

if (is_open && (!instance_exists(o_container)) && (!instance_exists(o_stash_inventory)))
{
    scr_actionsLog("searchBackpack", [scr_id_get_name(o_player), scr_actionsLogGetItemColorName(id)])
    scr_load_container("caravan_stash")
    scr_loadContainerContent(loot_list, o_stash_inventory)
}
else
{
    audio_play_sound(snd_gui_disable_mode, 3, 0)
    scr_actionsLog("openMagicBackpackInvalid", [scr_id_get_name(o_player), scr_actionsLogGetItemColorName(id)])
}

