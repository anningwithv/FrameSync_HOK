/*************************************************
	功能: 主要逻辑单位移动碰撞处理
*************************************************/

using FrameSyncProtocol;
using PEMath;
using PEPhysx;
using System.Collections.Generic;

public partial class MainLogicUnit
{

    /// <summary>
    /// UI输入方向
    /// </summary>
    private PEVector3 inputDir;
    public PEVector3 InputDir
    {
        private set
        {
            inputDir = value;
        }
        get
        {
            return inputDir;
        }
    }

    /// <summary>
    /// 战斗单位物理碰撞器
    /// </summary>
    public PECylinderCollider collider;
    List<PEColliderBase> envColliLst;
    void InitMove()
    {
        LogicPos = ud.bornPos;
        moveSpeedBase = ud.unitCfg.moveSpeed;
        LogicMoveSpeed = ud.unitCfg.moveSpeed;
        envColliLst = BattleSys.Instance.GetEnvColliders();
        collider = new PECylinderCollider(ud.unitCfg.colliCfg)
        {
            mPos = LogicPos
        };
    }

    void TickMove()
    {
        PEVector3 moveDir = InputDir;
        collider.mPos += moveDir * LogicMoveSpeed * (PEInt)Configs.ClientLogicFrameDeltaSec;
        PEVector3 adj = PEVector3.zero;
        collider.CalcCollidersInteraction(envColliLst, ref moveDir, ref adj);
        if (LogicDir != moveDir)
        {
            LogicDir = moveDir;
        }
        if (LogicDir != PEVector3.zero)
        {
            LogicPos = collider.mPos + adj;
        }
        collider.mPos = LogicPos;
        this.Log($"{unitName} pos:" + collider.mPos.ConvertViewVector3());
    }

    void UnInitMove()
    {
        //TODO
    }

    public void InputMoveKey(PEVector3 dir)
    {
        InputDir = dir;
        //this.Log("InputDir:" + dir.ConvertViewVector3());
    }
}
