/*************************************************
	功能: 主要逻辑单位技能处理
*************************************************/

using FrameSyncProtocol;
using PEMath;

public partial class MainLogicUnit {
    protected Skill[] skillArr;

    void InitSkill()
    {
        int len = ud.unitCfg.skillArr.Length;
        skillArr = new Skill[len];
        for (int i = 0; i < len; i++)
        {
            skillArr[i] = new Skill(ud.unitCfg.skillArr[i], this);
        }
    }

    void TickSkill()
    {
        //TODO
    }

    void InputSkillKey(SkillKey key)
    {
        for (int i = 0; i < skillArr.Length; i++)
        {
            if (skillArr[i].skillID == key.skillID)
            {
                PEInt x = PEInt.zero;
                PEInt z = PEInt.zero;
                x.ScaledValue = key.x_value;
                z.ScaledValue = key.z_value;
                PEVector3 skillArgs = new PEVector3(x, 0, z);
                skillArr[i].ReleaseSkill(skillArgs);
                return;
            }
        }
        this.Error($"skillID:{key.skillID} is not exist.");
    }
    #region API Functions
    public Skill GetNormalSkill()
    {
        if (skillArr != null && skillArr[0] != null)
        {
            return skillArr[0];
        }
        return null;
    }
    #endregion
}
