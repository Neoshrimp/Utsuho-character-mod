﻿using LBoL.Base;
using LBoL.ConfigData;
using LBoL.Core.Battle.BattleActions;
using LBoL.Core.Battle;
using LBoL.Core.Cards;
using LBoL.Core.Units;
using LBoL.Core;
using LBoL.Presentation;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using LBoLEntitySideloader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine;
using static Utsuho_character_mod.BepinexPlugin;
using Utsuho_character_mod.Status;

namespace Utsuho_character_mod
{
    public sealed class UtsuhoUltRDef : UltimateSkillTemplate
    {
        public override IdContainer GetId() => nameof(UtsuhoUlt);

        public override LocalizationOption LoadLocalization()
        {
            var gl = new GlobalLocalization(embeddedSource);
            gl.LocalizationFiles.AddLocaleFile(Locale.En, "UltimateSkillEn");
            return gl;
        }

        public override Sprite LoadSprite()
        {
            //return ResourceLoader.LoadSprite("reimu_fist.png", embeddedSource);
            return ResourceLoader.LoadSprite("Nuclear.png", embeddedSource);
        }

        public override UltimateSkillConfig MakeConfig()
        {
            var config = new UltimateSkillConfig(
                Id: "",
                Order: 10,
                PowerCost: 100,
                PowerPerLevel: 100,
                MaxPowerLevel: 2,
                RepeatableType: UsRepeatableType.OncePerTurn,
                Damage: 30,
                Value1: 30,
                Value2: 0,
                Keywords: Keyword.None,
                RelativeEffects: new List<string>() { "HeatStatus" },
                RelativeCards: new List<string>() { }
                );

            return config;
        }
    }

    [EntityLogic(typeof(UtsuhoUltRDef))]
    public sealed class UtsuhoUlt : UltimateSkill
    {
        public UtsuhoUlt()
        {
            base.TargetType = TargetType.SingleEnemy;
            base.GunName = "Simple1";
        }

        protected override IEnumerable<BattleAction> Actions(UnitSelector selector)
        {
            /*var bgGo = StageTemplate.TryGetEnvObject(NewBackgrounds.ghibliDeez);

            bgGo.SetActive(true);
            GameMaster.Instance.StartCoroutine(DeactivateDeez(bgGo));

            yield return PerformAction.Spell(Owner, new UtsuhoUltRDef().UniqueId);*/

            yield return new DamageAction(base.Owner, selector.GetEnemy(base.Battle), this.Damage, base.GunName, GunType.Single);
            yield return new ApplyStatusEffectAction<HeatStatus>(Battle.Player, Value1, null, null, null, 0f, true);

        }
        IEnumerator DeactivateDeez(GameObject go)
        {
            yield return new WaitForSeconds(5f);
            go.SetActive(false);
        }
    }
}