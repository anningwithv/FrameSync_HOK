﻿using System.Collections.Generic;
using FrameSyncProtocol;
using PEUtils;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance;
    public Transform uiRoot;
    public TipsWnd tipsWnd;

    void Start()
    {
        Instance = this;

        LogConfig cfg = new LogConfig
        {
            enableLog = true,
            logPrefix = "",
            enableTime = true,
            logSeparate = ">",
            enableThreadID = true,
            enableTrace = true,
            enableSave = true,
            enableCover = true,
            saveName = "HOKClientPELog.txt",
            loggerType = LoggerType.Unity
        };
        PELog.InitSettings(cfg);
        PELog.ColorLog(LogColor.Green, "InitLogger.");
        DontDestroyOnLoad(this);
        InitRoot();
        PELog.Log("Init Root.");
        Init();
        PELog.Log("Init Done.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitRoot()
    {
        for (int i = 0; i < uiRoot.childCount; i++)
        {
            Transform trans = uiRoot.GetChild(i);
            trans.gameObject.SetActive(false);
        }
        tipsWnd.SetWndState();
    }

    private NetSvc netSvc;
    private ResSvc resSvc;
    private AudioSvc audioSvc;

    void Init()
    {
        netSvc = GetComponent<NetSvc>();
        netSvc.InitSvc();
        resSvc = GetComponent<ResSvc>();
        resSvc.InitSvc();
        audioSvc = GetComponent<AudioSvc>();
        audioSvc.InitSvc();

        LoginSys login = GetComponent<LoginSys>();
        login.InitSys();
        LobbySys lobby = GetComponent<LobbySys>();
        lobby.InitSys();
        BattleSys battle = GetComponent<BattleSys>();
        battle.InitSys();
        GMSystem gm = GetComponent<GMSystem>();
        gm.InitSys();

        //login
        PELog.Log("EnterLogin.");
        login.EnterLogin();
    }

    public void ShowTips(string tips)
    {
        tipsWnd.AddTips(tips);
    }

    #region
    UserData userData;
    public UserData UserData
    {
        set { userData = value; }
        get { return userData; }
    }

    private uint roomID;
    public uint RoomID
    {
        set { roomID = value; }
        get { return roomID; }
    }
    private int mapID;
    public int MapID
    {
        set { mapID = value; }
        get { return mapID; }
    }

    private List<BattleHeroData> heroLst;
    public List<BattleHeroData> HeroLst
    {
        set { heroLst = value; }
        get { return heroLst; }
    }

    private int selfIndex;
    public int SelfIndex
    {
        set { selfIndex = value; }
        get { return selfIndex; }
    }
    #endregion
}
