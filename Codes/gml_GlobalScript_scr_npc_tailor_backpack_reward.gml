function scr_npc_tailor_backpack_reward() //gml_Script_scr_npc_tailor_backpack_reward
{
    var pelt_acceptable = [o_inv_hide_deer, o_inv_hide_sheep, o_inv_hide_saiga,
                           o_inv_hide_wolf, o_inv_hide_boar, o_inv_hide_moose,
                           o_inv_hide_fox, o_inv_hide_gulon, o_inv_hide_bison,
                           o_inv_hide_bear]

    if (argument_count == 1)
    {
        var _arg = argument[0]

        var _pelt_exists = false
        for (var i = 0; i < array_length(pelt_acceptable); i++) {
            if (scr_instance_exists_item(pelt_acceptable[i])) {
                _pelt_exists = true
                break
            }
        }

        return _pelt_exists && scr_instance_exists_item(o_inv_cloth)
                    && scr_instance_exists_item(o_inv_thread) && (o_player.gold > 50)
    }
    else if (argument_count == 2)
    {
        var _pelt_found = noone
        for (var i = 0; i < array_length(pelt_acceptable); i++) {
            if (scr_instance_exists_item(pelt_acceptable[i])) {
                _pelt_found = pelt_acceptable[i]
                break
            }
        }

        scr_delete_item(_pelt_found)
        scr_delete_item(o_inv_cloth)
        scr_delete_item(o_inv_thread)

        scr_gold_write_off(50)
        scr_characterGoldSpend("spentDialogues", 50)
    }
    else
    {
        with (scr_guiCreateContainer(global.guiBaseContainerVisible, o_reward_container))
            scr_inventory_add_item(o_inv_masterpiecebackpack)
    }
}