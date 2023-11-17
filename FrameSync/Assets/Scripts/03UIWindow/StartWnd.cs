/*************************************************
	功能: 开始界面
*************************************************/

using System.Collections;
using System.Collections.Generic;
using FrameSyncProtocol;
using UnityEngine;
using UnityEngine.UI;

public class StartWnd : WindowRoot
{
    public Text txtName;

    private UserData ud = null;
    protected override void InitWnd()
    {
        base.InitWnd();
        ud = root.UserData;
        txtName.text = ud.name;
    }

    public void ClickStartBtn()
    {
        audioSvc.PlayUIAudio("com_click1");
        LoginSys.Instance.EnterLobby();
    }

    public void ClickAreaBtn()
    {
        root.ShowTips("正在开发中...");
    }

    public void ClickExitBtn()
    {
        root.ShowTips("正在开发中...");
    }
}
