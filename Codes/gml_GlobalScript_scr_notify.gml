function scr_notify(argument0) //gml_Script_scr_notify
{
    if instance_exists(o_diary)
    {
        var _is_change_true = false
        change_contract_map = noone
        var _change_number = 0
        var _check_all = true
        var _is_quest = false
        var _id = id
        var _check_dungeon_id = false
        switch argument0
        {
            case "Delete":
                _change_number = -1
                _is_quest = ds_map_find_value(data, "is_quest_item")
                if ((!_is_quest) && (object_index == o_inv_backpack || object_is_ancestor(object_index, o_inv_backpack)))
                {
                    var _loot_list = ds_map_find_value(data, "lootList")
                    var k = 0
                    while (k < ds_list_size(_loot_list))
                    {
                        var _loot = ds_list_find_value(_loot_list, k)
                        var _data = ds_list_find_value(_loot, 1)
                        if ds_map_find_value(_data, "is_quest_item")
                        {
                            _is_quest = true
                            _id = ds_map_find_value(_data, "id")
                            break
                        }
                        else
                        {
                            k++
                            continue
                        }
                    }
                }
                break
            case "Delete_enemy":
                _change_number = 1
                _is_quest = ds_map_find_value(data, "is_quest_item")
                _check_dungeon_id = true
                break
            case "Create":
            case "Save_NPC":
                _change_number = 1
                if (argument0 == "Save_NPC")
                    _is_quest = scr_npc_get_global_info("is_quest")
                else
                {
                    _is_quest = ds_map_find_value(data, "is_quest_item")
                    if ((!_is_quest) && (object_index == o_inv_backpack || object_is_ancestor(object_index, o_inv_backpack)))
                    {
                        _loot_list = ds_map_find_value(data, "lootList")
                        k = 0
                        while (k < ds_list_size(_loot_list))
                        {
                            _loot = ds_list_find_value(_loot_list, k)
                            _data = ds_list_find_value(_loot, 1)
                            if ds_map_find_value(_data, "is_quest_item")
                            {
                                _is_quest = true
                                _id = ds_map_find_value(_data, "id")
                                break
                            }
                            else
                            {
                                k++
                                continue
                            }
                        }
                    }
                }
                break
            case "Enter":
                _is_quest = true
                _change_number = 1
                _check_all = false
                break
        }

        if _is_quest
        {
            with (o_console_controller)
            {
                if ds_exists(contract_list, 2)
                {
                    var _size = ds_list_size(contract_list)
                    var i = 0
                    while (i < _size)
                    {
                        var _contract_map = ds_list_find_value(contract_list, i)
                        if ds_map_find_value(_contract_map, "isGenerate")
                        {
                            if scr_contract_target_number_change(_contract_map, _id, _change_number, _check_all, _check_dungeon_id)
                            {
                                _is_change_true = true
                                change_contract_map = _contract_map
                                break
                            }
                            else
                            {
                                i++
                                continue
                            }
                        }
                        else
                        {
                            i++
                            continue
                        }
                    }
                }
            }
        }
        else if (argument0 == "Delete_enemy" && scr_isSafeLocation())
        {
            var _enemy_count = 0
            with (o_enemy)
            {
                if (id != other.id)
                {
                    if (!(object_is_ancestor(object_index, o_NPC)))
                        _enemy_count++
                }
            }
            if (!_enemy_count)
                scr_villagePanicOff()
        }
        if _is_change_true
        {
            with (o_console_controller)
                event_user(2)
        }
    }
}

