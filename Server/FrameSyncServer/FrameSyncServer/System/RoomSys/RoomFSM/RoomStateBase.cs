/*************************************************
	功能: 房间状态抽象基类
*************************************************/

namespace HOKServer {
    public interface IRoomState {
        void Enter();
        void Update();
        void Exit();
    }

    public abstract class RoomStateBase : IRoomState {
        public PVPRoom room;
        public RoomStateBase(PVPRoom room) {
            this.room = room;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Update();
    }

    public enum RoomStateEnum {
        None = 0,
        Confirm,    //确认
        Select,     //选择
        Load,       //加载
        Fight,      //战斗
        End,        //完成
    }
}
