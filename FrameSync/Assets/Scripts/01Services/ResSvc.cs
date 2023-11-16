/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/09 21:22
	功能: 资源服务

    //=================*=================\\
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using System.Collections.Generic;
using UnityEngine;

public class ResSvc : MonoBehaviour {
    public static ResSvc Instance;

    public void InitSvc() {
        Instance = this;
        this.Log("Init ResSvc Done.");
    }


    private Dictionary<string, AudioClip> adDic = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string path, bool cache = false) {
        AudioClip au = null;
        if(!adDic.TryGetValue(path, out au)) {
            au = Resources.Load<AudioClip>(path);
            if(cache) {
                adDic.Add(path, au);
            }
        }
        return au;
    }

}
