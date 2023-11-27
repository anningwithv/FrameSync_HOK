/*************************************************
	功能: 主要表现控制
*************************************************/

using UnityEngine;
/// <summary>
/// 攻速/移速动画变化 
/// 技能动画播放
/// 血条信息显示
/// 小地图显示 
/// </summary>
public abstract class MainViewUnit : ViewUnit {
    public Transform skillRange;
    public float fade;

    public Animation ani;

    float aniMoveSpeedBase;

    MainLogicUnit mainLogicUnit = null;
    public override void Init(LogicUnit logicUnit) {
        base.Init(logicUnit);
        mainLogicUnit = logicUnit as MainLogicUnit;

        //移速
        aniMoveSpeedBase = mainLogicUnit.LogicMoveSpeed.RawFloat;
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

    public override void PlayAni(string aniName) {
        if(aniName.Contains("walk")) {
            float moveRate = mainLogicUnit.LogicMoveSpeed.RawFloat / aniMoveSpeedBase;
            ani[aniName].speed = moveRate;
            ani.CrossFade(aniName, fade / moveRate);
        }
        else {
            if(ani == null) {
                this.Log("ani is null");
            }
            ani.CrossFade(aniName, fade);
        }
    }

    public void SetAtkSkillRange(bool state, float range = 2.5f) {
        if(skillRange != null) {
            range += mainLogicUnit.ud.unitCfg.colliCfg.mRadius.RawFloat;
            skillRange.localScale = new Vector3(range / 2.5f, range / 2.5f, 1);
            skillRange.gameObject.SetActive(state);
        }
    }
}
