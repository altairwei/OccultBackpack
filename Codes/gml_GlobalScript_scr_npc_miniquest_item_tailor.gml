function scr_npc_miniquest_item_tailor() //gml_Script_scr_npc_miniquest_item_tailor
{
    var _dialog = owner.questions_map
    var _action = ["Continue"]
    var _story_adress_array = array_create(0)
    var _answer = array_create(0)
    story_text = ""
    switch line_story
    {
        case 1:
            var _temp = array_create(0)
            if (scr_quest_get_progress("makeBackpackOrmond", "makeBackpackOrmond_find") < 0)
            {
                _temp =  scr_add_answer_to_dialog("tailor_backpack_pc")
                array_push(_answer, _temp[0])
                array_push(_story_adress_array, 7)
                _action = ["Miniquest"]
            }
            else if (!(scr_quest_get_progress("makeBackpackOrmond", "makeBackpackOrmond_find")))
            {
                if scr_npc_tailor_backpack_reward(true)
                {
                    _temp = scr_add_answer_to_dialog("backpack_materials_collected")
                    array_push(_answer, _temp[0])
                    array_push(_story_adress_array, 12)
                    _action = ["Miniquest"]
                }
            }

            if (scr_quest_get_progress("fetchOrmond", "fetchOrmond_find") < 0)
            {
                _temp = scr_add_answer_to_dialog("miniquest_inquiry")
                array_push(_answer, _temp[0])
                array_push(_story_adress_array, 2)
                _action = ["Miniquest"]
            }
            else if (!(scr_quest_get_progress("fetchOrmond", "fetchOrmond_find")))
            {
                if scr_npc_tailor_miniquest_reward(true)
                {
                    _temp = scr_add_answer_to_dialog("miniquest_complete")
                    array_push(_answer, _temp[0])
                    array_push(_story_adress_array, 6)
                    _action = ["Miniquest"]
                }
            }
            break
        case 2:
            story_text = "ormond_miniquest"
            _answer = scr_add_answer_to_dialog("miniquest_accept", "miniquest_reject")
            _story_adress_array = [3, 4]
            break
        case 3:
            story_text = ""
            scr_quest_set_progress("fetchOrmond", "fetchOrmond_find", 0)
            _story_adress_array = [1]
            scr_back_to_hub()
            return;
        case 4:
            story_text = ""
            _story_adress_array = [1]
            scr_back_to_hub()
            return;
        case 5:
            line_story = 1
            event_user(9)
            with (owner)
                event_user_number = 9
            event_user_number = 9
            return;
        case 6:
            story_text = "ormond_miniquest_complete"
            _answer = [scr_player_answer("back")]
            _story_adress_array = [1]
            _action = ["Back_to_Hub"]
            scr_quest_set_progress("fetchOrmond", "fetchOrmond_find", 1)
            scr_quest_set_complete("fetchOrmond")
            achivmentCounter(5, "Fetch_This!")
            event_user_number = 9
            reward_script = [gml_Script_scr_npc_tailor_miniquest_reward]
            break

        // Backpack making quest
        case 7:
            story_text = "tailor_backpack_inquiry"
            _answer = scr_add_answer_to_dialog("tailor_backpack_inquiry_pc")
            array_push(_answer, scr_player_answer("back"))
            _story_adress_array = [8, 10]
            break
        case 8:
            story_text = "tailor_backpack_quest"
            _answer = scr_add_answer_to_dialog("miniquest_accept", "miniquest_reject")
            _story_adress_array = [9, 10]
            break
        case 9:
            story_text = ""
            scr_quest_set_progress("makeBackpackOrmond", "makeBackpackOrmond_find", 0)
            _story_adress_array = [1]
            scr_back_to_hub()
            return;
        case 10:
            story_text = ""
            _story_adress_array = [1]
            scr_back_to_hub()
            return;
        case 11:
            line_story = 1
            event_user(9)
            with (owner)
                event_user_number = 9
            event_user_number = 9
            return;
        case 12:
            story_text = "tailor_making_backpack"
            _answer = [scr_player_answer("back")]
            _story_adress_array = [1]
            _action = ["Back_to_Hub"]
            scr_quest_set_progress("makeBackpackOrmond", "makeBackpackOrmond_find", 1)
            scr_quest_set_complete("makeBackpackOrmond")
            event_user_number = 9
            reward_script = [gml_Script_scr_npc_tailor_backpack_reward]
            break
        default:
            line_story = 1
            event_user(9)
            return;
    }

    scr_dialog_choose(owner.questions_map, story_text, _answer, 15, _story_adress_array, _action)
}

