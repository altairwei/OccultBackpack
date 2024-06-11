function scr_npc_tailor_backpack_reward() //gml_Script_scr_npc_tailor_backpack_reward
{
    if (argument_count == 1)
    {
        var _arg = argument[0]
        return scr_instance_exists_item(o_inv_hide_deer) && scr_instance_exists_item(o_inv_cloth)
                    && scr_instance_exists_item(o_inv_thread) && (o_player.gold > 50)
    }
    else
    {
        with (scr_guiCreateContainer(global.guiBaseContainerVisible, o_reward_container))
            scr_inventory_add_item(o_inv_backpack)

        scr_delete_item(o_inv_hide_deer)
        scr_delete_item(o_inv_cloth)
        scr_delete_item(o_inv_thread)

        scr_gold_write_off(50)
        scr_characterGoldSpend("spentDialogues", 50)
    }
}