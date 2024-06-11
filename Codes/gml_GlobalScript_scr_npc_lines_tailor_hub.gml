function scr_npc_lines_tailor_hub() //gml_Script_scr_npc_lines_tailor_hub
{
    var _dialog = owner.questions_map
    with (owner)
        event_user_number = 9
    var _story_adress_array = [-4, -4, -4]
    switch line_story
    {
        case 1:
            story_text = ""
            var _answer = [ds_map_find_value(_dialog, "tailor_main_line1_1_pc"), ds_map_find_value(_dialog, "tailor_main_line2_pc"), ds_map_find_value(_dialog, "tailor_main_line3_pc")]
            _story_adress_array = [2, 5, 6]

            if (scr_quest_get_progress("makeBackpackOrmond", "makeBackpackOrmond_find") < 0)
            {
                array_push(_answer, ds_map_find_value(_dialog, "tailor_backpack_pc"))
                array_push(_story_adress_array, 7)
            }
            else if (!(scr_quest_get_progress("makeBackpackOrmond", "makeBackpackOrmond_find")))
            {
                if scr_npc_tailor_backpack_reward(true)
                {
                    array_push(_answer, ds_map_find_value(_dialog, "backpack_materials_collected"))
                    array_push(_story_adress_array, 10)
                }
            }

            break
        case 2:
            story_text = "tailor_main_line1_1"
            _answer = [ds_map_find_value(_dialog, "tailor_main_line1_2_pc"), ds_map_find_value(_dialog, "tailor_main_line1_2_1_pc")]
            _story_adress_array = [3, 4]
            break
        case 3:
            story_text = "tailor_main_line1_2"
            _answer = [ds_map_find_value(_dialog, "tailor_main_line1_3_pc")]
            _story_adress_array = [4]
            break
        case 4:
            story_text = "tailor_main_line1_3"
            _answer = [scr_player_answer("continue")]
            _story_adress_array = [1]
            break
        case 5:
            story_text = "tailor_main_line2"
            _answer = [scr_player_answer("continue")]
            _story_adress_array = [1]
            break
        case 6:
            story_text = "tailor_main_line3"
            _answer = [scr_player_answer("continue")]
            _story_adress_array = [1]
            break
        case 7:
            story_text = "tailor_backpack_quest"
            _answer = scr_add_answer_to_dialog("miniquest_accept", "miniquest_reject")
            _story_adress_array = [8, 9]
            break
        case 8:
            story_text = ""
            scr_quest_set_progress("makeBackpackOrmond", "makeBackpackOrmond_find", 0)
            _story_adress_array = [1]
            scr_back_to_hub()
            return;
        case 9:
            story_text = ""
            _story_adress_array = [1]
            scr_back_to_hub()
            return;
        case 10:
            story_text = "tailor_making_backpack"
            _answer = [scr_player_answer("back")]
            _story_adress_array = [1]
            scr_back_to_hub()
            scr_quest_set_progress("makeBackpackOrmond", "makeBackpackOrmond_find", 1)
            scr_quest_set_complete("makeBackpackOrmond")
            event_user_number = 9
            scr_npc_tailor_backpack_reward()
            break
        default:
            line_story = 1
            event_user(9)
            break
    }

    scr_dialog_choose(owner.questions_map, story_text, _answer, 9, _story_adress_array)
}

