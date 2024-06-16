event_inherited()
inv_object = o_inv_masterpiecebackpack
number = 0
is_container = true
container_type = o_container_masterpiecebackpack
contentType = o_inv_slot
if __is_undefined(ds_map_find_value(data, "pregenLootList"))
    ds_map_add_list(data, "pregenLootList", __dsDebuggerListCreate())
