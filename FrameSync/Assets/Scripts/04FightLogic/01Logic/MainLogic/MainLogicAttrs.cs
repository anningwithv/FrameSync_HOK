/*************************************************
	功能: 主要逻辑单位属性状态处理
*************************************************/

using PEMath;
using System;

public partial class MainLogicUnit {
    public Action<int, JumpUpdateInfo> OnHPChange;
    /// <summary>
    /// 受到伤害回调
    /// </summary>
    public Action OnHurt;
    /// <summary>
    /// 死亡时
    /// </summary>
    public Action<MainLogicUnit> OnDeath;

    #region 属性状态数据
    private PEInt hp;
    public PEInt Hp {
        private set {
            hp = value;
        }
        get {
            return hp;
        }
    }
    private PEInt def;
    public PEInt Def {
        private set {
            def = value;
        }
        get {
            return def;
        }
    }

    public PEInt AttackSpeedRateBase;
    private PEInt attackSpeedRate;
    public PEInt AttackSpeedRate
    {
        private set
        {
            attackSpeedRate = value;

            Skill skill = GetNormalSkill();
            if (skill != null)
            {
                skill.skillTime = skill.cfg.skillTime * AttackSpeedRateBase / attackSpeedRate;
                skill.spellTime = skill.cfg.spellTime * AttackSpeedRateBase / attackSpeedRate;
            }
        }
        get
        {
            return attackSpeedRate;
        }
    }
    #endregion

    void InitProperties() {
        Hp = ud.unitCfg.hp;
        Def = ud.unitCfg.def;
    }

    public void InitAttackSpeedRate(PEInt rate)
    {
        AttackSpeedRateBase = rate;
        attackSpeedRate = rate;//每秒钟进行多少次攻击
    }

    #region API Functions
    public void GetDamageBySkill(PEInt damage, Skill skill)
    {
        OnHurt?.Invoke();//比如挂载Arthur标记buff，此时受伤会有额外伤害
        PEInt hurt = damage - Def;
        if (hurt > 0)
        {
            Hp -= hurt;
            if (Hp <= 0)
            {
                Hp = 0;
                unitState = UnitStateEnum.Dead;//状态切换
                InputFakeMoveKey(PEVector3.zero);
                OnDeath?.Invoke(skill.owner);
                PlayAni("death");
                this.Log($"{unitName} hp=0,Died");
            }
            this.Log($"{unitName} hp={hp.RawInt}");

            JumpUpdateInfo jui = null;
            if (IsPlayerSelf() || skill.owner.IsPlayerSelf())
            {
                jui = new JumpUpdateInfo
                {
                    jumpVal = hurt.RawInt,
                    jumpType = JumpTypeEnum.SkillDamage,
                    jumpAni = JumpAniEnum.LeftCurve
                };
            }
            OnHPChange?.Invoke(Hp.RawInt, jui);
        }
    }
    #endregion

    public bool IsTeam(TeamEnum teamEnum)
    {
        return ud.teamEnum == teamEnum;
    }
}
