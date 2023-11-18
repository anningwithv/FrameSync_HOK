/*************************************************
	日期: 2021/02/25 16:01
	功能: 服务器启动入口
*************************************************/

using System;
using System.Threading;

namespace HOKServer {
    class ServerStart {
        static void Main(string[] args) {
            ServerRoot.Instance.Init();

            while(true) {
                ServerRoot.Instance.Update();
                Thread.Sleep(10);
            }
        }
    }
}
