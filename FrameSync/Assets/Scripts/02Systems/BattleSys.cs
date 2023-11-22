using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameSyncProtocol;

public class BattleSys : SysRoot
{
    public static BattleSys Instance;

    public LoadWnd loadWnd;
    public PlayWnd playWnd;

    private int mapID;
    public override void InitSys()
    {
        base.InitSys();

        Instance = this;
        this.Log("Init BattleSys Done.");
    }

    public void EnterBattle()
    {
        audioSvc.StopBGMusic();
        loadWnd.SetWndState();

        mapID = root.MapID;

        resSvc.AsyncLoadScene("map_" + mapID, SceneLoadProgress, SceneLoadDone);
    }

    int lastPercent = 0;
    void SceneLoadProgress(float val)
    {
        int percent = (int)(val * 100);
        if (lastPercent != percent)
        {
            HOKMsg msg = new HOKMsg
            {
                cmd = CMD.SndLoadPrg,
                sndLoadPrg = new SndLoadPrg
                {
                    roomID = root.RoomID,
                    percent = percent
                }
            };
            netSvc.SendMsg(msg);
            lastPercent = percent;
        }
    }

    void SceneLoadDone()
    {
        //TODO
        //初始化UI
        playWnd.SetWndState();
        //加载角色及资源
        //初始化战斗

        HOKMsg msg = new HOKMsg
        {
            cmd = CMD.ReqBattleStart,
            reqBattleStart = new ReqBattleStart
            {
                roomID = root.RoomID
            }
        };
        netSvc.SendMsg(msg);
    }

    public void NtfLoadPrg(HOKMsg msg)
    {
        loadWnd.RefreshPrgData(msg.ntfLoadPrg.percentLst);
    }

    public void RspBattleStart(HOKMsg msg)
    {
        loadWnd.SetWndState(false);

        audioSvc.PlayBGMusic(NameDefine.BattleBGMusic);
    }
}
