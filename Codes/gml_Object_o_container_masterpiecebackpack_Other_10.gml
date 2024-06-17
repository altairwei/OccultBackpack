var _bottomMenuHeight = sprite_get_height(s_bottompanel)
var _offsetLeft = 0
var _offsetTop = global.cameraHeight - _bottomMenuHeight - sprite_height - 10
if global.resolutionHigh
    _offsetLeft = (global.cameraWidth - sprite_width) / 2
else
{
    scr_characterMenuClose(true)
    var _rightMenuWidth = 0
    with (o_inventory)
        _rightMenuWidth = sprite_get_width(sprite_index)
    _offsetLeft = (global.cameraWidth - _rightMenuWidth - sprite_width) / 2
}
scr_guiLayoutOffsetUpdate(id, _offsetLeft, _offsetTop)

closeButton = scr_adaptiveCloseButtonCreate(id, (depth - 1), 229, 3)
with (closeButton)
    drawHover = 1
getbutton = scr_adaptiveTakeAllButtonCreate(id, (depth - 1), 230, 27)
with (getbutton)
    owner = other.id
cellsContainer = scr_guiCreateContainer(id, o_guiContainerEmpty, depth, adaptiveOffsetX, adaptiveOffsetY)
cellsRowSize = 7
scr_inventory_add_cells(id, cellsContainer, cellsRowSize, 6)
