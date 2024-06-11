function scr_npc_tailor_backpack_reward() //gml_Script_scr_npc_tailor_backpack_reward
{
    if (argument_count == 1)
    {
        var _arg = argument[0]
        return scr_instance_exists_item(2997) && scr_instance_exists_item(2973) && scr_instance_exists_item(2974) && (o_player.gold > 50)
    }
    else
    {
        with (scr_guiCreateContainer(global.guiBaseContainerVisible, o_reward_container))
            scr_inventory_add_item(2936)

        scr_delete_item(2997)
        scr_delete_item(2973)
        scr_delete_item(2974)

        scr_gold_write_off(50)
        scr_characterGoldSpend("spentDialogues", 50)
    }
}