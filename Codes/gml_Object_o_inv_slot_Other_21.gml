if slotDestroyed
    return;
ds_list_clear(meetingList)
var _cellsMeetingListSize = instance_place_list(x, y, 2683, meetingList, false)
for (var _i = 0; _i < _cellsMeetingListSize; _i++)
{
    with (ds_list_find_value(meetingList, _i))
    {
        if scr_can_replace_item(other.id, id, false)
        {
            image_alpha = 1
            if (other.select == 0)
                image_blend = c_white
            else
                in_focus = true
        }
        else
            other.highlight_index = 1
    }
}
if slotDestroyed
    return;
ds_list_clear(meetingList)
var _slotsMeetingListSize = instance_place_list(x, y, 2685, meetingList, false)
for (_i = 0; _i < _slotsMeetingListSize; _i++)
{
    with (ds_list_find_value(meetingList, _i))
    {
        if (object_index == o_inv_backpack || object_is_ancestor(object_index, o_inv_backpack) || object_is_ancestor(object_index, o_inv_quiver_parent))
            other.draw_back = false
        if (other.select && (!equipped) && mouse_check_button_pressed(mb_left) && ((object_index == o_inv_backpack || object_is_ancestor(object_index, o_inv_backpack)) || (object_is_ancestor(object_index, o_inv_quiver_parent) && (other.object_index == contentType || object_is_ancestor(other.object_index, contentType)))))
        {
            // Exit the event if any side inventory window exists,
            // except for `o_side_inventory.parent != id` which allows Occult Backpack to be functional.
            if (instance_exists(o_trade_inventory) || (instance_exists(o_stash_inventory) && o_stash_inventory.parent != id))
                return;
            // Don't allow Tailor's Backpack to be opened when o_stash_inventory already opened
            if (!instance_exists(o_container_parent) || (object_index == o_inv_masterpiecebackpack && !instance_exists(o_stash_inventory)))
                event_user(14) // Open container

            if is_open
            {
                var _is_add = false
                if other.can_stack
                {
                    if instance_exists(o_container_parent)
                    {
                        if scr_inventory_stack(o_container_parent.id, other.id)
                            _is_add = true
                    } else if instance_exists(o_stash_inventory)
                    {
                        if scr_inventory_stack(o_stash_inventory.id, other.id)
                            _is_add = true
                    }
                }
                else
                {
                    if instance_exists(o_container_parent)
                    {
                        if scr_inventory_add(o_container_parent.id, other.id)
                            _is_add = true
                    } else if instance_exists(o_stash_inventory)
                    {
                        if scr_inventory_add(o_stash_inventory.id, other.id)
                            _is_add = true
                    }
                }

                if _is_add
                {
                    other.select = false
                    scr_guiPositionOffsetUpdate(other.id, 0, 0)
                    global.inv_select = false
                    event_user(1)
                    o_floor_target.draw_cursor = true
                    audio_play_sound(other.drop_gui_sound, 4, 0)
                    scr_allturn()
                }
            }
        }
        else
        {
            if instance_exists(other.owner)
            {
                if (other.owner.object_index == o_trade_inventory)
                    return;
                if instance_exists(owner)
                {
                    if (owner.object_index == o_trade_inventory)
                        return;
                    if (!((object_is_ancestor(other.object_index, owner.contentType) || other.object_index == owner.contentType)))
                        return;
                }
                if other.is_open
                    return;
            }
            if other.select
                scr_inventory_swap_id(other.id, id)
        }
    }
}
if slotDestroyed
    return;
ds_list_clear(meetingList)
if select
{
    var _twohandedBlocked = false
    if (hands == 2)
    {
        if (o_inv_right_hand.wound_block || o_inv_left_hand.wound_block || o_inv_left_hand.is_cursed || o_inv_right_hand.is_cursed)
            _twohandedBlocked = true
    }
    if (!_twohandedBlocked)
    {
        var _equipMeetingListSize = instance_place_list(x, y, 2684, meetingList, true)
        if _equipMeetingListSize
        {
            var _equipSlotNearest = ds_list_find_value(meetingList, 0)
            _i = 0
            while (_i < _equipMeetingListSize)
            {
                var _equipSlot = ds_list_find_value(meetingList, _i)
                if (_equipSlot.slot == slot)
                {
                    _equipSlotNearest = _equipSlot
                    break
                }
                else
                {
                    _i++
                    continue
                }
            }
            with (_equipSlotNearest)
            {
                if ((!is_lock) && (!wound_block))
                {
                    if (image_xscale > 0)
                    {
                        if (slot == other.slot)
                        {
                            if (children == noone)
                            {
                                subframe_index = 2
                                subframe_alpha = 0.2
                            }
                            else if children.can_remove
                            {
                                children.equipped_highlight_index = 2
                                children.equipped_highlight_alpha = 0.2
                                subframe_index = 2
                            }
                            else
                            {
                                children.equipped_highlight_index = 1
                                children.equipped_highlight_alpha = 0.2
                                subframe_index = 3
                            }
                        }
                        else if (children == noone)
                        {
                            subframe_index = 3
                            subframe_alpha = 0.2
                        }
                        else
                        {
                            children.equipped_highlight_index = 1
                            children.equipped_highlight_alpha = 0.2
                            subframe_index = 3
                        }
                        if ((!other.equipped) && slot == other.slot && other.alarm[1] < 0 && mouse_check_button_pressed(mb_left) && subframe_index != 3)
                        {
                            var _can_equip = true
                            if (other.can_equip && other.owner.object_index == o_trade_inventory)
                            {
                                if (o_player.all_money < other.price)
                                    _can_equip = false
                                else
                                {
                                    other.owner = owner
                                    scr_trade_item(o_inventory.id, o_trade_inventory.id, other.id, 0)
                                }
                            }
                            other.owner = owner
                            if _can_equip
                            {
                                global.inv_select = false
                                other.select = false
                                audio_play_sound(other.drop_gui_sound, 4, 0)
                                o_floor_target.draw_cursor = true
                                other.equipped = equipped_number
                                other.equipped_id = id
                                other.was_equipped = false
                                with (other.id)
                                    event_user(1)
                                scr_guiContainerChildAdd(id, other.id)
                                scr_guiLayoutOffsetUpdate(other.id, ((sprite_width - other.sprite_width) / 2), ((sprite_height - other.sprite_height) / 2))
                                scr_guiContainerRebuild(id)
                                scr_guiContainerUpdate(id)
                                if (children != noone)
                                {
                                    with (children)
                                    {
                                        equipped = false
                                        equipped_id = -4
                                        equipped_highlight_alpha = 0
                                        was_equipped = false
                                        select = true
                                        event_user(1)
                                        alarm[1] = 10
                                        global.inv_select = true
                                        o_floor_target.draw_cursor = false
                                    }
                                }
                                children = other.id
                                with (children)
                                    scr_steal()
                                event_user(6)
                                if ds_map_find_value(other.data, "is_cursed")
                                    audio_play_sound(choose(654, 655, 656), 4, 0)
                                scr_weapon_is_change()
                                scr_allturn()
                            }
                        }
                    }
                }
            }
        }
    }
}
if slotDestroyed
    return;
