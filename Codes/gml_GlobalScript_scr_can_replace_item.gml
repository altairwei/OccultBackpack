function scr_can_replace_item(argument0, argument1, argument2) //gml_Script_scr_can_replace_item
{
    if ((!instance_exists(argument0)) || (!instance_exists(argument1)))
        return false;
    if (!argument2)
    {
        if (argument0.is_open && (argument1.owner.object_index == o_container_backpack || object_is_ancestor(argument1.owner.object_index, o_container_backpack)))
            return false;
        if (argument0.object_index == argument1.owner.contentType || object_is_ancestor(argument0.object_index, argument1.owner.contentType))
            return true;
    }
    else
    {
        if (argument0.is_open && (argument1.object_index == o_container_backpack || object_is_ancestor(argument1.object_index, o_container_backpack) || argument1.object_index == o_stash_inventory))
            return false;
        if (argument0.object_index == argument1.contentType || object_is_ancestor(argument0.object_index, argument1.contentType))
            return true;
    }
    return false;
}

