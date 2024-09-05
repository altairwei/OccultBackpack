// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib;
using UndertaleModLib.Models;

namespace SimpleBiggerBackpack;
public class SimpleBiggerBackpack : Mod
{
    public override string Author => "Altair Wei";
    public override string Name => "Simple Bigger Backpack";
    public override string Description => "Now the tailor of Osbrook will sell a bigger backpack.";
    public override string Version => "1.0.1";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        // Make the backpack only take up 2x3 space in your inventory
        UndertaleSprite s_backpack = Msl.GetSprite("s_inv_travellersbackpack");
        s_backpack.Width = 54;
        s_backpack.Height = 81;
        s_backpack.MarginLeft = 1;
        s_backpack.MarginRight = 53;
        s_backpack.MarginBottom = 80;
        s_backpack.MarginTop = 1;

        UndertaleTexturePageItem t_backpack = s_backpack.Textures[0].Texture;
        t_backpack.TargetX = 4;
        t_backpack.TargetY = 2;
        t_backpack.BoundingWidth = 54;
        t_backpack.BoundingHeight = 81;

        // Make the Backpack hold more stuff
        UndertaleGameObject o_container_backpack = Msl.GetObject("o_container_backpack");
        o_container_backpack.Sprite = Msl.GetSprite("s_container");
        Msl.LoadGML("gml_Object_o_container_backpack_Other_10")
            .MatchFrom("closeButton = scr_adaptiveCloseButtonCreate(id, (depth - 1), 121, 3)")
            .ReplaceBy("closeButton = scr_adaptiveCloseButtonCreate(id, (depth - 1), 229, 3)")
            .Save();
        Msl.LoadGML("gml_Object_o_container_backpack_Other_10")
            .MatchFrom("getbutton = scr_adaptiveTakeAllButtonCreate(id, (depth - 1), 122, 27)")
            .ReplaceBy("getbutton = scr_adaptiveTakeAllButtonCreate(id, (depth - 1), 230, 27)")
            .Save();
        Msl.LoadGML("gml_Object_o_container_backpack_Other_10")
            .MatchFrom("cellsRowSize = 3")
            .ReplaceBy("cellsRowSize = 7")
            .Save();
        Msl.LoadGML("gml_Object_o_container_backpack_Other_10")
            .MatchFrom("scr_inventory_add_cells(id, cellsContainer, cellsRowSize, 4)")
            .ReplaceBy("scr_inventory_add_cells(id, cellsContainer, cellsRowSize, 5)")
            .Save();

        // Compatibility with UI+
        Msl.LoadGML("gml_Object_o_container_backpack_Other_10")
            .MatchAll()
            .InsertBelow(ModFiles, "compatibility_with_UI_plus.gml")
            .Save();

        // Add backpacks to the goods of the Osbrook tailor
        Msl.LoadGML("gml_Object_o_npc_tailor_Create_0")
            .MatchFrom(@"scr_buying_loot(""alcohol"", ""beverage"", ""food"", ""jewelry"", ""valuable"", ""armor"", ""material"", ""junk"", ""bag"")")
            .InsertAbove("ds_list_add(selling_loot_object, o_inv_backpack, 2.5)")
            .Save();
    }
}
