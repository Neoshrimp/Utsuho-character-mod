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

namespace Utsuho_character_mod.CardsR
{
    public sealed class PlanetaryOrbitDef : CardTemplate
    {
        public override IdContainer GetId()
        {
            return nameof(PlanetaryOrbit);
        }

        public override CardImages LoadCardImages()
        {
            var imgs = new CardImages(embeddedSource);
            imgs.AutoLoad(this, extension: ".png");
            return imgs;
        }

        public override LocalizationOption LoadLocalization()
        {
            var loc = new GlobalLocalization(embeddedSource);
            loc.LocalizationFiles.AddLocaleFile(Locale.En, "CardsEn.yaml");
            return loc;
        }

        public override CardConfig MakeConfig()
        {
            var cardConfig = new CardConfig(
                Index: sequenceTable.Next(typeof(CardConfig)),
                Id: "",
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
                Type: CardType.Attack,
                TargetType: TargetType.AllEnemies,
                Colors: new List<ManaColor>() { ManaColor.Black },
                IsXCost: false,
                Cost: new ManaGroup() { Black = 3 },
                UpgradedCost: new ManaGroup() { Black = 3 },
                MoneyCost: null,
                Damage: 5,
                UpgradedDamage: 7,
                Block: 5,
                UpgradedBlock: 7,
                Shield: null,
                UpgradedShield: null,
                Value1: null,
                UpgradedValue1: null,
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

                RelativeEffects: new List<string>() { },
                UpgradedRelativeEffects: new List<string>() { },
                //RelativeCards: new List<string>() { "AyaNews" },
                RelativeCards: new List<string>() { "DarkMatter" },
                UpgradedRelativeCards: new List<string>() { "DarkMatter" },
                Owner: "Utsuho",
                Unfinished: false,
                Illustrator: "",
                SubIllustrator: new List<string>() { }
             );

            return cardConfig;
        }

        [EntityLogic(typeof(PlanetaryOrbitDef))]
        public sealed class PlanetaryOrbit : Card
        {
            protected override IEnumerable<BattleAction> Actions(UnitSelector selector, ManaGroup consumingMana, Interaction precondition)
            {
                Card[] array = base.Battle.DrawZone.SampleManyOrAll(999, base.GameRun.BattleRng);
                Card[] array2 = base.Battle.DiscardZone.SampleManyOrAll(999, base.GameRun.BattleRng);
                if (array.Length != 0)
                {
                    foreach (Card card in array)
                    {
                        if (card.BaseName == "Dark Matter")
                        {
                            yield return new DiscardAction(card);
                        }
                    }
                }
                if (array2.Length != 0)
                {
                    foreach (Card card in array2)
                    {
                        if (card.BaseName == "Dark Matter")
                        {
                            yield return new MoveCardToDrawZoneAction(card, 0);
                        }
                    }
                    yield return new ReshuffleAction();
                }
                for (int i = 0; i < array.Length; i++)
                {
                    yield return AttackAction(selector);
                }
                for (int i = 0; i < array.Length; i++)
                {
                    yield return DefenseAction();
                }

                yield break;
            }

        }

    }
}