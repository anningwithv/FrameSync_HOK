/*************************************************
	功能: 资源服务
*************************************************/

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

    public UnitCfg GetUnitConfigByID(int unitID)
    {
        switch (unitID)
        {
            case 101:
                return new UnitCfg
                {
                    unitID = 101,
                    unitName = "亚瑟",
                    resName = "arthur"
                };
            case 102:
                return new UnitCfg
                {
                    unitID = 102,
                    unitName = "后羿",
                    resName = "houyi"
                };
        }
        return null;
    }
}
