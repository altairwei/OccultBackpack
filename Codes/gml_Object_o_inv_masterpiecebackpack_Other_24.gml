var list = ds_map_find_value(data, "lootList")
var _pregen_loot_list = ds_map_find_value(data, "pregenLootList")
if (ds_list_size(list) == 0 && (!__is_undefined(_pregen_loot_list)))
{
    if (!instance_exists(o_container_parent))
    {
        with (scr_container_create(o_container_masterpiecebackpack))
        {
            parent = other.id
            name = other.name
            var _size = ds_list_size(_pregen_loot_list)
            for (var i = 0; i < _size; i++)
            {
                var _item = ds_list_find_value(_pregen_loot_list, i)
                var _asset = asset_get_index(_item)
                if _asset
                    scr_inventory_add_item(_asset)
                else
                    scr_inventory_add_weapon(_pregen_loot_list[i])
            }
        }
        __dsDebuggerListDestroy(_pregen_loot_list)
        ds_map_delete(data, "pregenLootList")
        is_open = true
    }
}
else
    event_inherited()
