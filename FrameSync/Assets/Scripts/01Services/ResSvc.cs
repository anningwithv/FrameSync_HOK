/*************************************************
	功能: 资源服务
*************************************************/

using PEMath;
using PEPhysx;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance;

    public void InitSvc()
    {
        Instance = this;
        this.Log("Init ResSvc Done.");
    }


    private Dictionary<string, AudioClip> adDic = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path, bool cache = false)
    {
        AudioClip au = null;
        if (!adDic.TryGetValue(path, out au))
        {
            au = Resources.Load<AudioClip>(path);
            if (cache)
            {
                adDic.Add(path, au);
            }
        }
        return au;
    }

    private Dictionary<string, Sprite> spDic = new Dictionary<string, Sprite>();
    public Sprite LoadSprite(string path, bool cache = false)
    {
        Sprite sp = null;
        if (!spDic.TryGetValue(path, out sp))
        {
            sp = Resources.Load<Sprite>(path);
            if (cache)
            {
                spDic.Add(path, sp);
            }
        }
        return sp;
    }

    private Action prgCB = null;
    public void AsyncLoadScene(string sceneName, Action<float> loadRate, Action loaded)
    {
        AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);

        prgCB = () =>
        {
            float progress = sceneAsync.progress;
            loadRate?.Invoke(progress);
            if (progress == 1)
            {
                loaded?.Invoke();
                prgCB = null;
                sceneAsync = null;
            }
        };
    }

    private void Update()
    {
        prgCB?.Invoke();
    }

    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();
    public GameObject LoadPrefab(string path, bool cache = false)
    {
        GameObject prefab = null;
        if (!goDic.TryGetValue(path, out prefab))
        {
            prefab = Resources.Load<GameObject>(path);
            if (cache)
            {
                goDic.Add(path, prefab);
            }
        }

        GameObject go = null;
        if (prefab != null)
        {
            go = Instantiate(prefab);
        }
        return go;
    }

    public UnitCfg GetUnitConfigByID(int unitID)
    {
        switch (unitID)
        {
            case 101:
                return new UnitCfg
                {
                    unitID = 101,
                    unitName = "亚瑟",
                    resName = "arthur",

                    hp = 6500,
                    def = 0,
                    moveSpeed = 5,
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.5f
                    },
                    skillArr = new int[] { 1010, 1011, 1012, 1013 }
                };
            case 102:
                return new UnitCfg
                {
                    unitID = 102,
                    unitName = "后羿",
                    resName = "houyi",

                    hp = 3500,
                    def = 10,
                    moveSpeed = 5,
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.5f
                    },
                    skillArr = new int[] { 1020, 1021, 1022, 1023 }
                };
        }
        return null;
    }

    public SkillCfg GetSkillConfigByID(int skillID)
    {
        switch (skillID)
        {
            case 1010:
                return ResSkillConfigs.sk_1010;
            case 1011:
                return ResSkillConfigs.sk_1011;
            case 1012:
                return ResSkillConfigs.sk_1012;
            case 1013:
                return ResSkillConfigs.sk_1013;
            case 1020:
                return ResSkillConfigs.sk_1020;
            case 1021:
                return ResSkillConfigs.sk_1021;
            case 1022:
                return ResSkillConfigs.sk_1022;
            case 1023:
                return ResSkillConfigs.sk_1023;
            default:
                this.Error("Get Skill Config Failed,skillID:" + skillID);
                return null;
        }
    }

    public MapCfg GetMapConfigByID(int mapID)
    {
        switch (mapID)
        {
            case 101:
                return new MapCfg
                {
                    mapID = 101,
                    blueBorn = new PEVector3(-27, 0, 0),
                    redBorn = new PEVector3(27, 0, 0),
                    bornDelay = 15000,
                    bornInterval = 2000,
                    waveInterval = 50000
                };
            case 102:
                return new MapCfg
                {
                    mapID = 102,
                    blueBorn = new PEVector3(-27, 0, 0),
                    redBorn = new PEVector3(27, 0, 0),
                    bornDelay = 15000,
                    bornInterval = 2000,
                    waveInterval = 50000
                };
            default:
                return null;
        }
    }
}
