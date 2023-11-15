/*************************************************
	作者: Plane
	邮箱: 1785275942@qq.com
	日期: 2021/02/09 22:05
	功能: 登录窗口

    //=================*=================\\
           关注微信公众号: PlaneZhong
           关注微信服务号: qiqikertuts
               ~~获取更多教学资讯~~
    \\=================*=================//
*************************************************/

using UnityEngine.UI;

public class LoginWnd : WindowRoot {
    public InputField iptAcct;
    public InputField iptPass;
    public Toggle togSrv;

    protected override void InitWnd() {
        base.InitWnd();

        System.Random rd = new System.Random();
        iptAcct.text = rd.Next(100, 999).ToString();
        iptPass.text = rd.Next(100, 999).ToString();
    }

    public void ClickLoginBtn() {
        audioSvc.PlayUIAudio("loginBtnClick");
        if(iptAcct.text.Length >= 3 && iptPass.text.Length >= 3) {
            //TODO 发送网络消息，请求登录服务器
            root.AddTips("请求登录");
        }
        else {
            //POP Tips
            root.AddTips("账号或密码为空");
        }
    }
}
