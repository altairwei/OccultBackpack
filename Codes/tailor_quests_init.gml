scr_quest_init("makeBackpackOrmond", "", ["makeBackpackOrmond_find", 1, "makeBackpackOrmond_desc", [], "makeBackpackOrmond_reward", 1, "makeBackpackOrmond_reward_desc", []])
var _RottenWillow = scr_glmap_getLocation("RottenWillow")
scr_quest_init("makeMagicBackpack", "", ["makeMagicBackpack_rotten_willow", 1, "makeMagicBackpack_rotten_willow_desc", [_RottenWillow.x, _RottenWillow.y]])
