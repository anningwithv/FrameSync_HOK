/*************************************************
	功能: 主要表现控制
*************************************************/

using PEMath;
using UnityEngine;
using UnityEngine.XR;
/// <summary>
/// 攻速/移速动画变化 
/// 技能动画播放
/// 血条信息显示
/// 小地图显示 
/// </summary>
public abstract class MainViewUnit : ViewUnit {
    public Transform skillRange;
    public float fade;
    //血条定位transform
    public Transform hpRoot;
    public Animation ani;

    float aniMoveSpeedBase;
    float aniAttackSpeedBase;

    HPWnd hpWnd;
    PlayWnd playWnd;

    MainLogicUnit mainLogicUnit = null;
    public override void Init(LogicUnit logicUnit) {
        base.Init(logicUnit);
        mainLogicUnit = logicUnit as MainLogicUnit;

        //移速
        aniMoveSpeedBase = mainLogicUnit.LogicMoveSpeed.RawFloat;
        aniAttackSpeedBase = mainLogicUnit.AttackSpeedRate.RawFloat;

        //血条显示
        hpWnd = BattleSys.Instance.hpWnd;
        hpWnd.AddHPItemInfo(mainLogicUnit, hpRoot, mainLogicUnit.Hp.RawInt);

        playWnd = BattleSys.Instance.playWnd;

        mainLogicUnit.OnHPChange += UpdateHP;
    }

    protected override void Update() {
        if(mainLogicUnit.isDirChanged) {
            if(mainLogicUnit.LogicDir.ConvertViewVector3().Equals(Vector3.zero)) {
                PlayAni("free");
            }
            else {
                PlayAni("walk");
            }
        }

        base.Update();
    }

    private void OnDestroy()
    {
        mainLogicUnit.OnHPChange -= UpdateHP;
    }

    public override void PlayAni(string aniName) {
        if (aniName == "atk")
        {
            aniName = "atk" + Random.Range(1, 3);
        }

        if (aniName.Contains("walk"))
        {
            float moveRate = mainLogicUnit.LogicMoveSpeed.RawFloat / aniMoveSpeedBase;
            ani[aniName].speed = moveRate;
            ani.CrossFade(aniName, fade / moveRate);
        }
        else if (aniName.Contains("atk"))
        {
            if (ani.IsPlaying(aniName))
            {
                ani.Stop();
            }
            float attackRate = mainLogicUnit.AttackSpeedRate.RawFloat / aniAttackSpeedBase;
            ani[aniName].speed = attackRate;
            ani.CrossFade(aniName, fade / attackRate);
        }
        else
        {
            if (ani == null)
            {
                this.Log("ani is null");
            }
            ani.CrossFade(aniName, fade);
        }
    }

    void UpdateHP(int hp, JumpUpdateInfo jui)
    {
        if (jui != null)
        {
            float scaleRate = 1.0f * ClientConfig.ScreenStandardHeight / Screen.height;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
            jui.pos = screenPos * scaleRate;
        }
        hpWnd.SetHPVal(mainLogicUnit, hp, jui);
    }

    public void UpdateSkillRotation(PEVector3 skillRotation)
    {
        viewTargetDir = skillRotation.ConvertViewVector3();
    }

    public void SetAtkSkillRange(bool state, float range = 2.5f) {
        if(skillRange != null) {
            range += mainLogicUnit.ud.unitCfg.colliCfg.mRadius.RawFloat;
            skillRange.localScale = new Vector3(range / 2.5f, range / 2.5f, 1);
            skillRange.gameObject.SetActive(state);
        }
    }
}
