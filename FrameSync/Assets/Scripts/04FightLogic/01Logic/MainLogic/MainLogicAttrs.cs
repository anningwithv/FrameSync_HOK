/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com	
	功能: 主要逻辑单位属性状态处理

    //=================*=================\\
           教学官网：www.qiqiker.com
           官方微信服务号: qiqikertuts
           Plane老师微信: PlaneZhong
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using PEMath;

public partial class MainLogicUnit {
    #region 属性状态数据
    private PEInt hp;
    public PEInt Hp {
        private set {
            hp = value;
        }
        get {
            return hp;
        }
    }
    private PEInt def;
    public PEInt Def {
        private set {
            def = value;
        }
        get {
            return def;
        }
    }
    #endregion

    void InitProperties() {
        Hp = ud.unitCfg.hp;
        Def = ud.unitCfg.def;
    }
}
