
using ModShardLauncher;
using ModShardLauncher.Mods;

namespace OccultBackpack;

public class Localization
{
    public static void ItemsPatching()
    {
        Msl.InjectTableItemsLocalization(
            new LocalizationItem(
                id: "masterpiecebackpack",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Tailor's Backpack"},
                    {ModLanguage.Chinese, "裁缝的背包"}
                },
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "A exquisite backpack that's smaller and holds more stuff."},
                    {ModLanguage.Chinese, "一个更加小巧，但能装更多东西的背包。"}
                },
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "The masterpiece of the Osbrook tailor. While the style is similar to a traditional backpack, this one is more compact and can hold more."},
                    {ModLanguage.Chinese, "奥斯布鲁克裁缝的呕心沥血之作。虽然款式与传统背包差不多，但这个背包更加小巧，能装更多的东西。"}
                }
            ),
            new LocalizationItem(
                id: "magicbackpack",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Occult Backpack"},
                    {ModLanguage.Chinese, "玄秘背包"}
                },
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "A backpack that connects to a specific space."},
                    {ModLanguage.Chinese, "一个能联通特定空间的背包。"}
                },
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "l'Owcrey fixes occult powers on the backpack and links it to your caravan stash as you requested.##Opening the Occult Backpack is equivalent to opening the caravan stash."},
                    {ModLanguage.Chinese, "埃欧科里按照你的要求在背包上将玄秘力量固定下来，并与你的大篷车货堆联通。##打开魔法背包等同于打开大篷车货堆。"}
                }
            )
        );
    }

    public static void DialogLinesPatching()
    {
        Msl.InjectTableDialogLocalization(
            new LocalizationSentence(
                "tailor_backpack_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "How come you don't sell backpacks here?"},
                    {ModLanguage.Chinese, "你这里怎么没有卖背包？"}
                }
            ),
            new LocalizationSentence(
                "tailor_backpack_inquiry",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, string.Join("#", new string[] {
                        "A backpack? Ha! Who needs a backpack in a place as small as Osbrook?",
                        "With the war raging, who even dares to travel far, let alone buy one? ",
                        "Aldor's traditional travel backpacks are big and clunky, utterly impractical. Only greenhorns buy those. I certainly wouldn't waste my skills making such rubbish."
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "背包？哈！奥斯布鲁克就这么个巴掌大的地方，谁会买背包？现在战乱成这样，谁还敢走远路？更没人会买了。",
                        "奥尔多传统的旅行背包又大又笨重，一点不实用，也就那些没经验的菜鸟才会买。我可不屑做这种垃圾。"
                    })}
                }
            ),
            new LocalizationSentence(
                "tailor_backpack_inquiry_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Does that mean you can make a small and functional backpack?"},
                    {ModLanguage.Chinese, "那意思是你能制作一个小巧又实用的背包？"}
                }
            ),
            new LocalizationSentence(
                "tailor_backpack_quest",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, string.Join("#", new string[] {
                        "Since you asked, I guess I have to show off my ancestral craft. But right now, I'm short on the right materials.",
                        "Here's the deal: bring me a good pelt, a bolt of cloth, and a spool of thread. I'll charge you just 50 crowns to make you a fine backpack.",
                        "And remember, don't try my patience with useless pelts like dog, horse, or rabbit skins."
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "既然你都开口了，那我也只能展示一下祖传的手艺了。不过眼下我手头缺合适的材料。",
                        "这样吧，你去找一张好的兽皮，一卷布料，再带一轴毛线。我只收你 50 冠手工费，给你做个精致的背包。",
                        "记住，别拿狗皮、马皮、兔皮这些垃圾来糊弄我。"
                    })}
                }
            ),
            new LocalizationSentence(
                "backpack_materials_collected",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I've collected all the materials for making a backpack."},
                    {ModLanguage.Chinese, "制作背包的材料我都收集好了。"}
                }
            ),
            new LocalizationSentence(
                "tailor_making_backpack",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Let's see what you've got.#Pelt... Cloth... A spool of thread... Looks like everything's here!#Alright, but making this backpack will take some time. Come back tomorrow."},
                    {ModLanguage.Chinese, "拿出来让我瞧瞧。#皮毛……布料……还有一轴毛线……材料齐全了！#好吧，不过做这个背包得费点功夫，明天再来取吧。"}
                }
            ),
            new LocalizationSentence(
                "backpack_claim",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Is my backpack done?"},
                    {ModLanguage.Chinese, "我的背包做好了吗？"}
                }
            ),
            new LocalizationSentence(
                "tailor_backpack_reward",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "All done! Here you go."},
                    {ModLanguage.Chinese, "做好了！给你。"}
                }
            ),
            // TODO: Refine me
            new LocalizationSentence(
                "masterpiecebackpack_is_good_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "What a great backpack you have made!"},
                    {ModLanguage.Chinese, "你制作的背包真是太好用了！"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_rumor",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, string.Join("#", new string[] {
                        "Oh really? You lot only believe what you can see with your own eyes.",
                        "My backpack is nothing special. Once, I heard a hunter selling pelts talk about some kind of contraption.",
                        "He was hunting in the woods when he spotted a man with a painted face pulling a whole bunch of stuff out of a tiny travel case.",
                        "Pots, firewood, tents, even bunk beds—it all came out, and he set up camp in no time. He suspected the case was enchanted.",
                        "If you had something like that, you mercenaries wouldn't need to worry about hauling your loot around."
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "是吗？你们这些人非得看见点真东西才信服。",
                        "但我这背包不算什么。有次我听卖皮的猎人说起过一个神奇物件。",
                        "他在林子里打猎时，远远看见一个满脸花纹的人从一个小小的手提箱里拿出了一大堆东西。",
                        "锅碗、柴火、帐篷、铺盖卷，一下子就搭起了营地。他猜那箱子肯定是被施了魔法。",
                        "要是你们雇佣兵有了那东西，就不愁战利品带不走了。"
                    })}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_rumor_accept",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Holy shit! There's such a thing as magic! Just tell me where to find someone to cast a spell on my backpack!"},
                    {ModLanguage.Chinese, "我靠！还有这种魔法！你快告诉我去哪找谁给我的背包施施法！"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_rumor_reject",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I don't believe any of this bullshit from you at all."},
                    {ModLanguage.Chinese, "我根本不相信你的这些鬼扯。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_where_to_find",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, string.Join("#", new string[] {
                        "I have no idea. I just heard the story.",
                        "But you might try asking around at the Rotten Willow Tavern.",
                        "That place is crawling with all kinds, so you might pick up some useful information."
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "我怎么会知道，我也只是听说罢了。",
                        "不过你可以去烂柳旅店打听。",
                        "那里三教九流的人都有，兴许能问出点消息。"
                    })}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_where_is_rottenwillow_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "The Rotten Willow Tavern? What kind of a shithole is that?"},
                    {ModLanguage.Chinese, "烂柳旅店？那是一个什么破地方？"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_where_is_rottenwillow",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "It's a roadside tavern north of Mannshire. Bring your map over, and I'll mark the general location for you."},
                    {ModLanguage.Chinese, "那是一家位于曼郡北边的路边旅店。把地图拿来，我给你标一下大概的位置。"}
                }
            ),

            // Inquire l'Owcrey about Occult Backpack
            new LocalizationSentence(
                "magicbackpack_inquire_lowcrey_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I would like to ask you something."},
                    {ModLanguage.Chinese, "我想跟你打听个事儿。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_inquire_lowcrey",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "What's up?"},
                    {ModLanguage.Chinese, "敢问有何贵干？"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_inquire_lowcrey_pc_2a",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "*You told him the same rumours the tailor had mentioned.*"},
                    {ModLanguage.Chinese, "*你向他复述了裁缝讲给你的传闻*"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_inquire_lowcrey_pc_2b",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Do you remember the contraption I mentioned before? Have you heard anything new about it recently?"},
                    {ModLanguage.Chinese, "还记得我之前提到的那个神奇物件吗？不知道你最近有没有听到什么新的消息？"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_inquire_lowcrey_2a",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "*l'Owcrey glances you over, his eyes narrowing slightly*#Never heard of it, I don't know."},
                    {ModLanguage.Chinese, "*埃欧科里上下打量了你一眼，眼神略显犀利*#没听说过，我不知道。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_inquire_lowcrey_2b",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Hmmm... Jonna... I'm sorry, I hadn't heard anyhting about this. #*He frowns slightly, an unusual hint of embarrassment crossing his face*"},
                    {ModLanguage.Chinese, "嗯...约娜...不好意思，我没有听说过这事。#*他皱了皱眉，脸上闪过一丝少见的尴尬*"}
                }
            ),

            // Materials to repair Occult Backpack
            new LocalizationSentence(
                "magicbackpack_materials_to_repair_lowcrey",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I see you've been sniffing around. Well, let me put an end to the mystery: the main character in that rumor? That's me."},
                    {ModLanguage.Chinese, "我看你一直在打听这事儿。好吧，我就给你个实话：传闻中的主角，正是在下。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_materials_to_repair_lowcrey_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I didn't realise it was you!"},
                    {ModLanguage.Chinese, "没想到竟然是你！"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_materials_to_repair_lowcrey_2",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "That was a long time ago, and let's just say, that little contraption of mine didn't survive the journey."},
                    {ModLanguage.Chinese, "不过这事儿已经过去很久了，当年我那玩意儿也没能完好无损地熬过那次旅程。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_materials_to_repair_lowcrey_pc_2",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Oh no! I can't believe it's broken...... Is there a way to fix it?"},
                    {ModLanguage.Chinese, "啊！竟然坏了...... 有办法修好吗？"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_materials_to_repair_lowcrey_3",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "So, you want it that badly, huh? But why on earth should I give it to you?"},
                    {ModLanguage.Chinese, "哦？你就这么想要？不过我凭什么给你呢？"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_materials_to_repair_lowcrey_pc_3",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "If you have any requests, kill someone, fight a monster, find something, whatever, just mention it to me! I owe you a favour."},
                    {ModLanguage.Chinese, "你有什么要求，杀个人、打个怪、找个东西什么的，尽管跟我提！算我欠你一个人情。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_materials_to_repair_lowcrey_4",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, string.Join("#", new string[] {
                        "*He pauses, his gaze drifting off as if contemplating something*",
                        "Perhaps I might have some use for you yet.",
                        "Theoretically, I could repair it, but it's a hassle, and I'm short on materials.",
                        "*He sighs with feigned helplessness*",
                        "It requires a ruby, an emerald, and 1000 crowns for ritual supplies."
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "*埃欧科里沉默片刻，目光游离，仿佛在思索什么*",
                        "说不定以后还能用得上你。",
                        "那玩意儿理论上可以修好，不过麻烦得很，而且材料不足。",
                        "*他故作无奈地叹了口气*",
                        "需要一颗红宝石、一块祖母绿，还有1000冠用来买仪式材料。"
                    })}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_materials_to_repair_lowcrey_pc_4",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Good! I'll make sure to get all the materials."},
                    {ModLanguage.Chinese, "好！我一定把材料找齐。"}
                }
            ),

            // Ready to make Occult Backpack
            new LocalizationSentence(
                "magicbackpack_ready_to_make_pc",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I have all the materials to fix this contraption!"},
                    {ModLanguage.Chinese, "我把修理那神奇玩意儿的材料找齐了！"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_ready_to_make_lowcrey",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, string.Join("#", new string[] {
                        "I didn't expect you to actually get everything.",
                        "I used to attach this arcane enchantment to a suitcase.",
                        "What kind of container suits your taste?"
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "没想到你还真把东西凑齐了。",
                        "我以前是把这玄秘法术固定在一个手提箱上。",
                        "你喜欢用什么容器？"
                    })}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_ready_to_make_pc_2a",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Do you think my backpack will fit?"},
                    {ModLanguage.Chinese, "你看我这个背包合用吗？"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_ready_to_make_pc_2b",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Just use the backpack, I'll clean out the contents first."},
                    {ModLanguage.Chinese, "就背包吧，等我先把里面的东西清理出来。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_ready_to_make_lowcrey_2",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, string.Join("#", new string[] {
                        "A traveling backpack, eh? Quite a refined choice, I'd say.",
                        "But tell me, have you figured out where you want this backpack connected to?"
                    })},
                    {ModLanguage.Chinese, string.Join("#", new string[] {
                        "旅行背包？倒也别致，我想没什么问题。",
                        "不过你可想好了，这背包要连通到哪个空间？"
                    })}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_ready_to_make_pc_3",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I don't have a place to stay yet, so I haven't thought about it yet."},
                    {ModLanguage.Chinese, "我现在居无定所，还没想好。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_ready_to_make_pc_4",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "I think the Caravan's storage is a great option."},
                    {ModLanguage.Chinese, "我觉得大篷车的货仓是个不错的选择。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_ready_to_make_lowcrey_4",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Alright, I've got it. Come back in two days."},
                    {ModLanguage.Chinese, "行吧，我知道了，两天后再来找我。"}
                }
            ),

            new LocalizationSentence(
                "magicbackpack_ready_to_make_pc_5",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "Is the Occult Backpack ready?"},
                    {ModLanguage.Chinese, "玄秘背包制作好了吗？"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_ready_to_make_lowcrey_5",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "It's done. Here you go."},
                    {ModLanguage.Chinese, "搞定了，拿去吧。"}
                }
            ),
            new LocalizationSentence(
                "magicbackpack_ready_to_make_pc_6",
                new Dictionary<ModLanguage, string>() {
                    {ModLanguage.English, "That's fantastic!"},
                    {ModLanguage.Chinese, "真是太棒了！"}
                }
            )
        );
    }

    public static void QeustsPatching()
    {
        List<string> stringList = new List<string>();

        string id = "makeBackpackOrmond";
        string text_en = @"Hold the Tailor's Backpack Making";
        string text_zh = @"裁缝霍特背包制作";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeBackpackOrmond_find";
        text_en = @"Find a Pelt, a Bolt of Cloth, and a Spool of Thread";
        text_zh = @"寻找毛皮、亚麻布和毛线";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeBackpackOrmond_desc";
        text_en = @"Hold the Tailor from Osbrook doesn't sell backpacks, but is willing to help me make a new one. He asked me to get him a pelt, a bolt of cloth, and a spool of thread, plus fifty crowns for the craft.";
        text_zh = @"奥斯布鲁克的裁缝霍特不卖背包，但愿意帮我制作一个新的。他让我给他弄一张毛皮、一卷亚麻布和一轴毛线，再加上五十冠的手工费。";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeBackpackOrmond_reward";
        text_en = "Claim Your Backpack";
        text_zh = "领取背包";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeBackpackOrmond_reward_desc";
        text_en = "Hold, the tailor, says it takes quite a bit of work to make a fine backpack, and he's asked me to come back this time tomorrow to pick it up.";
        text_zh = "裁缝霍特说制作一个精良的背包需要花费不少功夫，他让我明天再来取。";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeMagicBackpack";
        text_en = "Legendary Backpack";
        text_zh = "传说中的背包";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeMagicBackpack_rotten_willow";
        text_en = "Go to the Rotten Willow Tavern and look for clues.";
        text_zh = "去烂柳旅店寻找线索";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeMagicBackpack_rotten_willow_desc";
        text_en = "Hold, the tailor, tells you of a rumour that someone has seen a magical suitcase that is directly linked to a room, allowing items to be picked up and dropped off at any time from thousands of miles away. He tells you to poke around the Rotten Willow Tavern if interested.";
        text_zh = "裁缝告诉你一个传闻，有人曾见过一种神奇手提箱，直接联通了固定的房间，可以在千里之外随时取放物品。他告诉你，如果感兴趣的话，可以去烂柳旅店打探下消息。";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeMagicBackpack_find_materials";
        text_en = "Find a Ruby and an Emerald";
        text_zh = "寻找红宝石与祖母绿";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeMagicBackpack_find_materials_desc";
        text_en = "l'Owcrey ignores you at first, but as your reputation in the Rotten Willow grows, he reveals to you that the rumoured magical suitcase is none other than his, but it is broken. You promise him that if he can repair the magical suitcase and let you use it, you will owe him a great favour. l'Owcrey agrees, but asks that you provide the materials needed to repair the suitcase at your own expense, including a ruby, an emerald, and the cost of 1000 crowns used to purchase the materials for the ritual.";
        text_zh = "埃欧科里起初并不理会你，但随着你在烂柳的声誉逐渐提升，他向你透露，那传闻中的神奇手提箱正是他的，不过已经损坏。你向他承诺，如果他能修好这神奇手提箱并让你使用，你将欠他一个大大的人情。埃欧科里同意了，但要求你自费提供修复手提箱所需的材料，包括一颗红宝石、一块祖母绿，以及用于购买仪式材料的1000冠费用。";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeMagicBackpack_reward";
        text_en = "Claim Your Occult Backpack";
        text_zh = "领取玄秘背包";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        id = "makeMagicBackpack_reward_desc";
        text_en = "After much hardship, you finally managed to find a ruby and an emerald, scrimping and saving 1000 crowns from your contract rewards, and even offering the specially crafted backpack made by Osbrook’s tailor, Hold. l'Owcrey will need two days to transfer the occult rituals from the suitcase to your backpack, so now all you can do is wait patiently.";
        text_zh = "你历经艰辛，终于找到了红宝石和祖母绿，并从契据任务的报酬中省出1000冠，还搭上了奥斯布鲁克裁缝霍特为你精心制作的背包。埃欧科里需要两天时间将玄秘仪轨从手提箱转移到你的背包上，现在你只能耐心等待。";
        stringList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        string questend = ";" + string.Concat(Enumerable.Repeat("text_end;", 12));

        List<string> quest_table = ModLoader.GetTable("gml_GlobalScript_table_Quests_text");
        quest_table.InsertRange(quest_table.IndexOf(questend), stringList);
        ModLoader.SetTable(quest_table, "gml_GlobalScript_table_Quests_text");
    }

    public static void ActionLogsPatching()
    {
        List<string> logList = new List<string>();
        string id = "openMagicBackpackInvalid";
        string text_en = @"~w~$~/~ try to open $, but find that it doesn't work.";
        string text_zh = @"~w~$~/~尝试打开$，但发现不行。";
        logList.Add($"{id};{text_en};{text_en};{text_zh};" + string.Concat(Enumerable.Repeat($"{text_en};", 9)));

        logList.Add("searchBackpack;~w~$~/~ обыскивает контейнер ($).;~w~$~/~ searches the $.;~w~$~/~翻了翻$。;~w~$~/~ durchsucht $.;~w~$~/~ revisa: $.;~w~$~/~ fouille $.;~w~$~/~ perquisisce $.;~w~$~/~ vasculha $.;~w~$~/~ przeszukuje $.;~w~$~/~ araştırdı: $.;~w~$~/~ は $ を調べた;~w~$~/~이(가) $을(를) 뒤졌다.;");

        string logtextend = ";" + string.Concat(Enumerable.Repeat("text_end;", 12));

        List<string> log_table = ModLoader.GetTable("gml_GlobalScript_table_log_text");
        log_table.InsertRange(log_table.IndexOf(logtextend), logList);
        ModLoader.SetTable(log_table, "gml_GlobalScript_table_log_text");
    }
}