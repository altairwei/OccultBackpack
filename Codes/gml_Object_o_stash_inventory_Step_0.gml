if (parent != noone && instance_exists(parent) && parent.object_index == o_inv_magicbackpack)
{
    if parent.select
    {
        if (!guiDestroyed)
        {
            is_execute = true
            category = "all"
            scr_save_item(parent.loot_list, id, false, false)
            instance_activate_object(parent)
            instance_destroy()
        }
    }
}
