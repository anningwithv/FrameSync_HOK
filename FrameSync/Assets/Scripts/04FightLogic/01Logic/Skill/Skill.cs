/*************************************************
	功能: 技能
*************************************************/

using PEMath;
using System;

public enum SkillState {
    None,
    SpellStart,
    SpellAfter,
}

public class Skill {
    public int skillID;
    public SkillCfg cfg;
    public PEVector3 skillArgs;
    public MainLogicUnit lockTarget;
    public SkillState skillState = SkillState.None;

    public PEInt spellTime;//施法时间
    public PEInt skillTime;//技能总时间

    public MainLogicUnit owner;

    public Action FreeAniCallback;

    public Skill(int skillID, MainLogicUnit owner) {
        this.skillID = skillID;
        cfg = ResSvc.Instance.GetSkillConfigByID(this.skillID);
        spellTime = cfg.spellTime;
        skillTime = cfg.skillTime;

        if(cfg.isNormalAttack) {
            owner.InitAttackSpeedRate(1000 / skillTime);
        }

        this.owner = owner;
    }

    void HitTarget(MainLogicUnit target, object[] args = null) {
        //音效表现
        if(cfg.audio_hit != null) {
            target.PlayAudio(cfg.audio_hit);
        }
        //可能全为buff伤害，这里为0
        if(cfg.damage != 0) {
            PEInt damage = cfg.damage;
            target.GetDamageBySkill(damage, this);
        }
        //附加buff到目标
        if(cfg.buffIDArr == null) {
            return;
        }

        //TODO
    }

    /// <summary>
    /// 技能生效
    /// </summary>
    /// <param name="lockTarget"></param>
    void CalcSkillAttack(MainLogicUnit lockTarget) {
        if(cfg.bulletCfg != null) {
            //TODO
        }
        else {
            HitTarget(lockTarget);
        }
    }

    /// <summary>
    /// 施法前摇开始，瞬时技能这个时间阶段为0
    /// </summary>
    /// <param name="spellDir"></param>
    void SkillSpellStart(PEVector3 spellDir) {
        skillState = SkillState.SpellStart;
        if(cfg.audio_start != null) {
            owner.PlayAudio(cfg.audio_start);
        }
        if(spellDir != PEVector3.zero) {
            owner.mainViewUnit.UpdateSkillRotation(spellDir);
        }
        if(cfg.aniName != null) {
            owner.InputFakeMoveKey(PEVector3.zero);
            owner.PlayAni(cfg.aniName);
            //技能被中断或后摇被移动取消需要调用动画重置
            FreeAniCallback = () => {
                owner.PlayAni("free");
            };
        }
    }

    /// <summary>
    /// 施法后摇动作完成,角色切换到idle状态
    /// </summary>
    void SkillEnd() {
        if(FreeAniCallback != null) {
            FreeAniCallback();
            FreeAniCallback = null;
        }
        skillState = SkillState.None;
        lockTarget = null;
    }

    /// <summary>
    /// 施放技能
    /// </summary>
    public void ReleaseSkill(PEVector3 skillArgs) {
        this.skillArgs = skillArgs;
        //目标技能，必须存在施法目标，且目标队伍类型不能为动态类型
        if(cfg.targetCfg != null && cfg.targetCfg.targetTeam != TargetTeamEnum.Dynamic) {
            lockTarget = CalcRule.FindSingleTargetByRule(owner, cfg.targetCfg, skillArgs);
            if(lockTarget != null) {
                PEVector3 spellDir = lockTarget.LogicPos - owner.LogicPos;
                SkillSpellStart(spellDir);

                if(spellTime == 0) {
                    this.Log("瞬发技能，立即生效");
                    CalcSkillAttack(lockTarget);
                    //附着buff
                }
                else {
                    //定时处理TODO
                }

            }
            else {
                this.Warn("没有符合条件的技能目标");
                SkillEnd();
            }
        }
        //非目标技能
    }
}
