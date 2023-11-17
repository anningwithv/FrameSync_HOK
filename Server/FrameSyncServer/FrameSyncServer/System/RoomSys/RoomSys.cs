/*************************************************
	功能: 房间系统 
*************************************************/

using FrameSyncProtocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace HOKServer {
    public class RoomSys : SystemRoot<RoomSys> 
    {
        List<PVPRoom> pvpRoomLst = null;
        Dictionary<uint, PVPRoom> pvpRoomDic = null;

        public override void Init() 
        {
            base.Init();

            pvpRoomLst = new List<PVPRoom>();
            pvpRoomDic = new Dictionary<uint, PVPRoom>();

            this.Log("RoomSys Init Done.");
        }

        public override void Update() {
            base.Update();
        }

        public void AddPVPRoom(ServerSession[] sessionArr, PVPEnum pvp)
        {
            uint roomID = GetUniqueRoomID();
            PVPRoom room = new PVPRoom(roomID, pvp, sessionArr);
            pvpRoomLst.Add(room);
            pvpRoomDic.Add(roomID, room);
        }

        uint roomID = 0;
        public uint GetUniqueRoomID()
        {
            roomID += 1;
            return roomID;
        }
    }
}
