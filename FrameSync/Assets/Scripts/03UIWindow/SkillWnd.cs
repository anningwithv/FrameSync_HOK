using FrameSyncProtocol;
using UnityEngine;

public partial class PlayWnd 
{
    public SkillItem skaItem;
    public SkillItem sk1Item;
    public SkillItem sk2Item;
    public SkillItem sk3Item;

    public Transform imgInfoRoot;

    public void InitSkillInfo() {
        BattleHeroData self = root.HeroLst[root.SelfIndex];
        UnitCfg heroCfg = resSvc.GetUnitConfigByID(self.heroID);
        int[] skillArr = heroCfg.skillArr;

        skaItem.InitSkillItem(resSvc.GetSkillConfigByID(skillArr[0]), 0);
        sk1Item.InitSkillItem(resSvc.GetSkillConfigByID(skillArr[1]), 1);
        sk2Item.InitSkillItem(resSvc.GetSkillConfigByID(skillArr[2]), 2);
        sk3Item.InitSkillItem(resSvc.GetSkillConfigByID(skillArr[3]), 3);

        SetForbidState(false);
        SetActive(imgInfoRoot, false);
    }

    void SetForbidState(bool state) {
        sk1Item.SetForbidState(state);
        sk2Item.SetForbidState(state);
        sk3Item.SetForbidState(state);
    }
}
