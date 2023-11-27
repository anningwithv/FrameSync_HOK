/*************************************************
	功能: 战斗界面
*************************************************/

using PEMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWnd : WindowRoot
{
    protected override void InitWnd()
    {
        base.InitWnd();
    }

    protected override void UnInitWnd()
    {
        base.UnInitWnd();
    }

    private Vector2 lastKeyDir = Vector2.zero;
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 keyDir = new Vector2(h, v);
        if (keyDir != lastKeyDir)
        {
            if (h != 0 || v != 0)
            {
                keyDir = keyDir.normalized;
            }
            InputMoveKey(keyDir);
            lastKeyDir = keyDir;
        }
    }

    private Vector2 lastStickDir = Vector2.zero;
    private void InputMoveKey(Vector2 dir)
    {
        if (!dir.Equals(lastStickDir))
        {
            Vector3 dirVector3 = new Vector3(dir.x, 0, dir.y);
            dirVector3 = Quaternion.Euler(0, 45, 0) * dirVector3;
            PEVector3 logicDir = PEVector3.zero;
            if (dir != Vector2.zero)
            {
                logicDir.x = (PEInt)dirVector3.x;
                logicDir.y = (PEInt)dirVector3.y;
                logicDir.z = (PEInt)dirVector3.z;
            }

            bool isSend = BattleSys.Instance.SendMoveKey(logicDir);
            if (isSend)
            {
                lastStickDir = dir;
            }
        }
    }
}
