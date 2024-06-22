scr_guiContainerDestroy()
instance_destroy(block_gui)
global.inv_select = false
if (!audio_is_playing(snd_ui_close_window_st))
    audio_play_sound(snd_ui_close_window_st, 1, 0)
with (o_player)
    ds_list_delete(lock_skills, 0)
with (o_skill)
    event_user(8)
__dsDebuggerListDestroy(loot_list)
loot_list = -4
with (o_modificatorsMenu)
{
    tradeMenuActive = false
    event_user(1)
}

if (parent != noone && instance_exists(parent) && parent.object_index == o_inv_magicbackpack)
{
    with (parent)
    {
        can_swap = true
        is_open = false
    }
} else
{
    with (o_inventory)
        event_user(15)
}

scr_escapeButtonListRemove()
scr_camera_set_target(o_player)
with (owner)
    is_dialog = false

