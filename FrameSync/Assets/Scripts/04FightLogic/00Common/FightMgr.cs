using FrameSyncProtocol;
using UnityEngine;
using System.Collections.Generic;
using PEPhysx;
using PEMath;

public class FightMgr : MonoBehaviour
{
    MapRoot mapRoot;
    EnvColliders logicEnv;

    Transform transFollow;
    //英雄角色集合
    List<Hero> heroLst;
    public void Init(List<BattleHeroData> battleHeroLst, MapCfg mapCfg)
    {
        heroLst = new List<Hero>();
        //初始化碰撞环境
        InitEnv();
        //防御塔
        //英雄
        InitHero(battleHeroLst, mapCfg);
        //小兵

        //delay 以后 出生小兵。。。
    }

    void InitHero(List<BattleHeroData> battleHeroLst, MapCfg mapCfg)
    {
        int sep = battleHeroLst.Count / 2;
        for (int i = 0; i < battleHeroLst.Count; i++)
        {
            HeroData hd = new HeroData
            {
                heroID = battleHeroLst[i].heroID,
                posIndex = i,
                userName = battleHeroLst[i].userName,
                unitCfg = ResSvc.Instance.GetUnitConfigByID(battleHeroLst[i].heroID)
            };

            Hero hero;
            if (i < sep)
            {
                hd.teamEnum = TeamEnum.Blue;
                hd.bornPos = mapCfg.blueBorn;
                hero = new Hero(hd);
            }
            else
            {
                hd.teamEnum = TeamEnum.Red;
                hd.bornPos = mapCfg.redBorn;
                hero = new Hero(hd);
            }
            hero.LogicInit();
            heroLst.Add(hero);
        }
    }

    void InitEnv()
    {
        Transform transMapRoot = GameObject.FindGameObjectWithTag("MapRoot").transform;
        mapRoot = transMapRoot.GetComponent<MapRoot>();
        List<ColliderConfig> envColliCfgLst = GenerateEnvColliCfgs(mapRoot.transEnvCollider);
        logicEnv = new EnvColliders
        {
            envColliCfgLst = envColliCfgLst
        };
        logicEnv.Init();
    }

    public void InitCamFollowTrans(int posIndex)
    {
        transFollow = heroLst[posIndex].mainViewUnit.transform;
    }

    private void Update()
    {
        if (transFollow != null)
        {
            mapRoot.transCameraRoot.position = transFollow.position;
        }
    }

    public void Tick()
    {

        //hero tick
        for (int i = 0; i < heroLst.Count; i++)
        {
            heroLst[i].LogicTick();
        }
    }

    public void UnInit()
    {
        heroLst.Clear();
    }

    List<ColliderConfig> GenerateEnvColliCfgs(Transform transEnvRoot)
    {
        List<ColliderConfig> envColliCfgLst = new List<ColliderConfig>();
        BoxCollider[] boxArr = transEnvRoot.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < boxArr.Length; i++)
        {
            Transform trans = boxArr[i].transform;
            var cfg = new ColliderConfig
            {
                mPos = new PEVector3(trans.position)
            };
            cfg.mSize = new PEVector3(trans.localScale / 2);
            cfg.mType = ColliderType.Box;
            cfg.mAxis = new PEVector3[3];
            cfg.mAxis[0] = new PEVector3(trans.right);
            cfg.mAxis[1] = new PEVector3(trans.up);
            cfg.mAxis[2] = new PEVector3(trans.forward);

            envColliCfgLst.Add(cfg);
        }

        CapsuleCollider[] cylindderArr = transEnvRoot.GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < cylindderArr.Length; i++)
        {
            Transform trans = cylindderArr[i].transform;
            var cfg = new ColliderConfig
            {
                mPos = new PEVector3(trans.position)
            };
            cfg.mType = ColliderType.Cylinder;
            cfg.mRadius = (PEInt)(trans.localScale.x / 2);

            envColliCfgLst.Add(cfg);
        }
        return envColliCfgLst;
    }

    public void InputKey(List<OpKey> keyLst)
    {
        for (int i = 0; i < keyLst.Count; i++)
        {
            OpKey key = keyLst[i];
            MainLogicUnit hero = heroLst[key.opIndex];
            hero.InputKey(key);
        }
    }

    public List<PEColliderBase> GetEnvColliders()
    {
        return logicEnv.GetEnvColliders();
    }

    public MainLogicUnit GetSelfHero(int posIndex)
    {
        return heroLst[posIndex];
    }
}
