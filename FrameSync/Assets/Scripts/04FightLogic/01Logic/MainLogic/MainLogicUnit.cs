/*************************************************
	功能: 主要逻辑单位（英雄，小兵，塔）
*************************************************/

using FrameSyncProtocol;
using PEMath;

public enum UnitStateEnum
{
    Alive,
    Dead
}

public enum UnitTypeEnum
{
    Hero,
    Soldier,
    Tower,
}

public enum TeamEnum
{
    None,
    Blue,
    Red,
    Neutal//中立，暂时用不上
}

public abstract partial class MainLogicUnit : LogicUnit
{
    public LogicUnitData ud;
    public UnitStateEnum unitState;
    public UnitTypeEnum unitType;

    public MainLogicUnit(LogicUnitData ud)
    {
        this.ud = ud;
        unitName = ud.unitCfg.unitName;
    }


    public override void LogicInit()
    {
        //初始化属性
        InitProperties();
        //初始化技能
        InitSkill();
        //初始化移动
        InitMove();

        unitState = UnitStateEnum.Alive;
    }

    public override void LogicTick()
    {
        TickSkill();
        TickMove();
    }

    public override void LogicUnInit()
    {
        UnInitSkill();
        UnInitMove();
    }

    public void InputKey(OpKey key)
    {
        switch (key.keyType)
        {
            case KeyType.Skill:
                //TODO
                break;
            case KeyType.Move:
                PEInt x = PEInt.zero;
                x.ScaledValue = key.moveKey.x;
                PEInt z = PEInt.zero;
                z.ScaledValue = key.moveKey.z;
                InputMoveKey(new PEVector3(x, 0, z));
                break;
            case KeyType.None:
            default:
                this.Error("KEY is not exist");
                break;
        }
    }
}
