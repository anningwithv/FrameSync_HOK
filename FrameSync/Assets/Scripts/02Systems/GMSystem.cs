using FrameSyncProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMSystem : SysRoot {
    public static GMSystem Instance;
    public bool isActive = false;

    private uint frameID = 0;
    private List<OpKey> opkeyLst = new List<OpKey>();

    public override void InitSys() {
        base.InitSys();

        Instance = this;
        this.Log("Init GMSystem Done.");
    }

    public void StartSimulate() {
        isActive = true;
        StartCoroutine(BattleSimulate());
    }

    public IEnumerator BattleSimulate() {
        SimulateLoadRes();
        yield return new WaitForSeconds(0.5f);
        SimulateBattleStart();
    }

    void SimulateLoadRes() {
        HOKMsg msg = new HOKMsg {
            cmd = CMD.NtfLoadRes,
            ntfLoadRes = new NtfLoadRes {
                mapID = 102,
                heroList = new List<BattleHeroData> {
                    new BattleHeroData{ heroID = 101,userName = "Plane"},
                    new BattleHeroData{ heroID = 102,userName = "Frank"},
                },
                posIndex = 0
            }
        };
        LobbySys.Instance.NtfLoadRes(msg);
    }

    void SimulateBattleStart() {
        HOKMsg msg = new HOKMsg {
            cmd = CMD.RspBattleStart
        };
        BattleSys.Instance.RspBattleStart(msg);
    }

    public void SimulateServerRcvMsg(HOKMsg msg) {
        switch(msg.cmd) {
            case CMD.SndOpKey:
                UpdateOpeKey(msg.sndOpKey.opKey);
                break;
            default:
                break;
        }
    }

    void FixedUpdate() {
        ++frameID;
        HOKMsg msg = new HOKMsg {
            cmd = CMD.NtfOpKey,
            ntfOpKey = new NtfOpKey {
                frameID = frameID,
                keyList = new List<OpKey>()
            }
        };

        int count = opkeyLst.Count;
        if(count > 0) {
            for(int i = 0; i < opkeyLst.Count; i++) {
                OpKey key = opkeyLst[i];
                msg.ntfOpKey.keyList.Add(key);
            }
        }
        opkeyLst.Clear();
        netSvc.AddMsgQue(msg);
    }


    void UpdateOpeKey(OpKey key) {
        opkeyLst.Add(key);
    }
}
