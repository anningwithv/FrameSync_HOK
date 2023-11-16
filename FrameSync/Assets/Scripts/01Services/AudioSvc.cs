﻿/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/09 21:21
	功能: 音频服务

    //=================*=================\\
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioSvc : MonoBehaviour {
    public static AudioSvc Instance;
    public bool TurnOnVoice;
    public AudioSource bgAudio;
    public AudioSource uiAudio;

    public void InitSvc() {
        Instance = this;
        this.Log("Init AudioSvc Done.");
    }

    public void PlayBGMusic(string name, bool isLoop = true) {
        if(!TurnOnVoice) {
            return;
        }
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        if(bgAudio.clip == null || bgAudio.clip.name != audio.name) {
            bgAudio.clip = audio;
            bgAudio.loop = isLoop;
            bgAudio.Play();
        }
    }

    public void PlayUIAudio(string name) {
        if(!TurnOnVoice) {
            return;
        }
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        uiAudio.clip = audio;
        uiAudio.Play();
    }
}
