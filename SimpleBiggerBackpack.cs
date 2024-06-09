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
    public override string Version => "1.0.0.0";
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
        
    }
}
