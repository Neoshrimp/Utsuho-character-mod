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
using LBoL.Base.Extensions;
using JetBrains.Annotations;
using System.Linq;
using Utsuho_character_mod.Util;

namespace Utsuho_character_mod.CardsBR
{
    public sealed class SunCrowDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(SunCrow);
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
                Index: 55,
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
                IsPooled: true,
                HideMesuem: false,
                IsUpgradable: true,
                Rarity: Rarity.Rare,
                Type: CardType.Ability,
                TargetType: TargetType.Nobody,
                Colors: new List<ManaColor>() { ManaColor.Black, ManaColor.Red },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 2, Red = 2, Any = 1 },
                UpgradedCost: new ManaGroup() { Black = 2, Red = 2, Any = 1 },
                MoneyCost: null,
                Damage: null,
                UpgradedDamage: null,
                Block: null,
                UpgradedBlock: null,
                Shield: null,
                UpgradedShield: null,
                Value1: 12,
                UpgradedValue1: 16,
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

                RelativeEffects: new List<string>() { "HeatStatus" },
                UpgradedRelativeEffects: new List<string>() { "HeatStatus" },
                RelativeCards: new List<string>() { "DarkMatter" },
                UpgradedRelativeCards: new List<string>() { "DarkMatter" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "Flippin'Loser",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(SunCrowDef))]
        public sealed class SunCrow : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                List<Card> cards = base.Battle.EnumerateAllCards().Where((Card card) => (card != this) && (card.Id == "DarkMatter")).ToList<Card>();
                yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, new int?(base.Value1) * cards.Count, null, null, null, 0f, true);
                foreach (Card card in cards)
                {
                    yield return new RemoveCardAction(card);
                }
                yield break;
            }
        }
    }
}
