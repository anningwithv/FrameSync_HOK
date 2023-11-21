using System;

namespace FrameSyncProtocol
{
    public class ServerConfig
    {
        public const string LocalDevInnerIP = "192.168.26.173";
        public const int UdpPort = 17666;

        //确认匹配倒计时：15秒
        public const int ConfirmCountDown = 15;
        //选择英雄倒计时：15秒
        public const int SelectCountDown = 15;
    }
}
