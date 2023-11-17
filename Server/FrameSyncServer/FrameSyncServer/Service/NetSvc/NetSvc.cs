/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/25 16:03
	功能: 网络服务

    //=================*=================\\
           教学官网：www.qiqiker.com
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts           
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using PEUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer {
    public class NetSvc : Singleton<NetSvc> {
        public override void Init() {
            base.Init();

            this.Log("NetSvc Init Done.");
        }

        public override void Update() {
            base.Update();
        }
    }
}
