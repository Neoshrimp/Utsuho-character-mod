﻿using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle;
using LBoL.Core;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Cards;
using LBoL.Core.StatusEffects;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Status;
using static Utsuho_character_mod.CardsB.DarkMatterDef;
using Utsuho_character_mod.Util;
using HarmonyLib;
using static Utsuho_character_mod.CardsBR.GammaProtocolDef;

namespace Utsuho_character_mod.CardsBR
{
    public sealed class BetaProtocolDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(BetaProtocol);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(embeddedSource);
            imgs.AutoLoad(this, extension: ".png");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            return UsefulFunctions.LocalizationCard(directorySource);
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: 45,
                Id: "",
                ImageId: "",
                UpgradeImageId: "",
                Order: 10,
                AutoPerform: true,
                Perform: new string[0][],
                GunName: "Simple1",
                GunNameBurst: "Simple1",
                DebugLevel: 0,
                Revealable: false,
                IsPooled: false,
                HideMesuem: true,
                IsUpgradable: true,
                Rarity: Rarity.Rare,
                Type: CardType.Ability,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Black, ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 1, Red = 1, Any = 1 },
                UpgradedCost: new ManaGroup() { Black = 1, Red = 1, Any = 1 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 3,
                UpgradedValue1: 3,
                Value2: null,
                UpgradedValue2: null,
                Mana: null,
                UpgradedMana: null,
                Scry: null,
                UpgradedScry: null,
                ToolPlayableTimes: null,

                Loyalty: null,
                UpgradedLoyalty: null,
                PassiveCost: null,
                UpgradedPassiveCost: null,
                ActiveCost: null,
                UpgradedActiveCost: null,
                UltimateCost: null,
                UpgradedUltimateCost: null,

                Keywords: Keyword.None,
                UpgradedKeywords: Keyword.None,
                EmptyDescription: false,
                RelativeKeyword: Keyword.None,
                UpgradedRelativeKeyword: Keyword.None,

                RelativeEffects: new List<string>() { "GammaStatus" },
                UpgradedRelativeEffects: new List<string>() { "GammaStatus" },
                RelativeCards: new List<string>() { "GammaProtocol" },
                UpgradedRelativeCards: new List<string>() { "GammaProtocol+" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(BetaProtocolDef))]
        public sealed class BetaProtocol : Card
        {


            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                if (!IsUpgraded)
                {
                    Card[] cards = { Library.CreateCard("OmegaProtocol") };
                    yield return new AddCardsToDrawZoneAction(cards, DrawZoneTarget.Random);
                }
                else
                {
                    Card[] cards = { Library.CreateCard("OmegaProtocol+") };
                    yield return new AddCardsToDrawZoneAction(cards, DrawZoneTarget.Random);
                }
                for (int i = 0; i < Value1; i++)
                {
                    Card card = UsefulFunctions.RandomUtsuho(Battle.HandZone);
                    foreach (BattleAction action in UsefulFunctions.RandomCheck(card, Battle)) { yield return action; }
                    yield return new ExileCardAction(card);
                }
                yield break;
            }

        }

    }
}
