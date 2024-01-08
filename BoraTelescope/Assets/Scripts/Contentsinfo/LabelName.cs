using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelName : MonoBehaviour
{
    public string Namelist;
    public string TourNamelist;
    public void LabelLanguage(string name)
    {
        if (ContentsInfo.ContentsName == "Aegibong")
        {
            switch (name)
            {
                case "River":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "예성강 하구";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "The mouth of the Yeseong River";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "礼成江 河口";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "イェソン川河口";
                            break;
                    }
                    break;
                case "DogogaeM":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "도고개산";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Dogogae Mountain";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "东姑盖山";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "トゴゲ山";
                            break;
                    }
                    break;
                case "Old":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "구 탈곡장";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Old Threshing Floor";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "前场打谷场";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "旧脱穀場";
                            break;
                    }
                    break;
                case "AmsilV":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "암실마을";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Amsil Village";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "岩实村";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "アムシル村";
                            break;
                    }
                    break;
                case "Quarry":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "채석장";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Quarry";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "采石场";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "採石場";
                            break;
                    }
                    break;
                case "New":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "신 탈곡장";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "New Threshing Floor";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "新打谷场";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "新脱穀場";
                            break;
                    }
                    break;
                case "PromotionV":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "해물선전마을";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Haemul Promotion Village";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "海穆尔广告村";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "ヘムル広告村";
                            break;
                    }
                    break;
                case "Dora":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "파주 도라전망대";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Paju Dora Observation";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "都罗展望台";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "トラ展望台";
                            break;
                    }
                    break;
                case "Gwansanpo":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "관산반도";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "The Gwansan Peninsula";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "关山半岛";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "クァンサン半島";
                            break;
                    }
                    break;
                case "Dolgoji":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "시암리 습지";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Siam-ri Wetland";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "暹罗里湿地";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "シアム里湿地";
                            break;
                    }
                    break;
                case "HanR":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "한강";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Han River";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "汉江";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "漢江";
                            break;
                    }
                    break;
                case "Paju":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "파주";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Paju";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "坡州";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "坡州";
                            break;
                    }
                    break;
            }
        }else if (ContentsInfo.ContentsName == "Typhoon")
        {
            switch (name)
            {
                case "Hinge":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "힌지";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Hinge";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "绞链";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "ヒンジ";
                            break;
                    }
                    break;
                case "Brown":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "갈색";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "BrownKnoll";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "布朗诺尔";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "ブラウン·ノール";
                            break;
                    }
                    break;
                case "Taecy":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "테시";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Tessle";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "泰瑟尔";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "テッスル";
                            break;
                    }
                    break;
                case "Nickey":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "닉키";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Nickie";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "尼基";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "ニッキー";
                            break;
                    }
                    break;
                case "Bubble":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "버블고지";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Bubble Highlands";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "泡泡高地";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "バブル高原";
                            break;
                    }
                    break;
                case "Antia":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "안티아";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Antia";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "安蒂亚";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "アンティア";
                            break;
                    }
                    break;
                case "Park":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "박";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Park";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "Park";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "Park";
                            break;
                    }
                    break;
                case "Queen":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "퀸";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Queen";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "皇后乐队";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "クイーン";
                            break;
                    }
                    break;                
            }
        }
        else if (ContentsInfo.ContentsName == "EndIsland")
        {
            switch (name)
            {
                case "Airport":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "백령공항";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Baengnyeong Airport";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "白翎机场";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "白翎空港";
                            break;
                    }
                    break;
                case "Pyongyang":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "개성직할시 방향";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Direction of Gaeseong City";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "開城直割市方向";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "開城直割市方面";
                            break;
                    }
                    break;
                case "Big":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "대청도 방향";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Direction of Daecheongdo Island";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "大青岛方向";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "大靑島方面";
                            break;
                    }
                    break;
                case "Small":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "소청도 방향";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Socheongdo direction";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "小青岛方向";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "小靑島方面";
                            break;
                    }
                    break;
                case "Simchung":
                    switch (GameManager.currentLang)
                    {
                        case GameManager.Language_enum.Korea:
                            Namelist = "심청각 방향";
                            break;
                        case GameManager.Language_enum.English:
                            Namelist = "Shimcheonggak direction";
                            break;
                        case GameManager.Language_enum.Chinese:
                            Namelist = "沈清阁方向";
                            break;
                        case GameManager.Language_enum.Japanese:
                            Namelist = "沈清閣方面";
                            break;
                    }
                    break;
            }
        }
    }
}
