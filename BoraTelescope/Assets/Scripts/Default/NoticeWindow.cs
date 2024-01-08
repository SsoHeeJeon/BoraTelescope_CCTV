using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeWindow : MonoBehaviour
{
    public static GameManager gamemanager;
    public static NoticeWindow noticewindow;
    public static string NoticeState;
    public static Image NoticeIcon;
    public static Text NoticeText;
    public static Font KEfont;
    public static Font CJfont;

    public static GameObject ButType_1;
    public static GameObject ButType_2;

    public Sprite ErrorIcon;
    public Sprite CheckIcon;

    private void ReadytoStart()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        noticewindow = gamemanager.ErrorMessage.transform.parent.GetComponent<NoticeWindow>();
        KEfont = gamemanager.label.Titlefont_KE;
        CJfont = gamemanager.label.Titlefont_CJ;
    }

    public static void NoticeWindowOpen(string Window)
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        noticewindow = gamemanager.ErrorMessage.transform.parent.GetComponent<NoticeWindow>();
        if (!gamemanager.ErrorMessage.activeSelf)
        {
            gamemanager.ErrorMessage.SetActive(true);
        }

        NoticeIcon = gamemanager.ErrorMessage.transform.GetChild(0).gameObject.GetComponent<Image>();
        NoticeText = gamemanager.ErrorMessage.transform.GetChild(1).gameObject.GetComponent<Text>();
        ButType_1 = gamemanager.ErrorMessage.transform.GetChild(2).gameObject;
        ButType_2 = gamemanager.ErrorMessage.transform.GetChild(3).gameObject;

        NoticeState = Window;
        
        //switch (GameManager.currentLang)
        //{
        //    case GameManager.Language_enum.Korea:
        //        NoticeText.font = KEfont;
        //        break;
        //    case GameManager.Language_enum.English:
        //        NoticeText.font = KEfont;
        //        break;
        //    case GameManager.Language_enum.Chinese:
        //        NoticeText.font = CJfont;
        //        break;
        //    case GameManager.Language_enum.Japanese:
        //        NoticeText.font = CJfont;
        //        break;
        //}

        noticewindow.Setanything();
        Debug.Log(ButType_1 + " / " + ButType_2);
    }

    public void Setanything()
    {
        switch (NoticeState)
        {
            case "ErrorMessage":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "죄송합니다.\r\n현재 해당 모드를 이용하실 수 없습니다.";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "I'm sorry.\r\nYou can't use mode.";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "绐您添麻烦子, 真抱歉。 目前不能使用。";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "すみません。 現在のモードは利用できません。";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                }
                ButType_1.SetActive(true);
                ButType_2.SetActive(false);
                NoticeIcon.sprite = ErrorIcon;
                break;
            case "ErrorInternet":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "죄송합니다.\r\n현재 인터넷이 불안정하여 QR코드 제공이 어렵습니다.";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "I'm sorry.\r\nYou can't use mode.";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "绐您添麻烦子, 真抱歉。 目前不能使用。";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "すみません。 現在のモードは利用できません。";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                }
                ButType_1.SetActive(true);
                ButType_2.SetActive(false);
                NoticeIcon.sprite = ErrorIcon;
                gamemanager.WriteErrorLog(LogSendServer.ErrorLogCode.Fail_InternetConnect, "InternetConnect Fail", GetType().ToString());
                break;
            case "SeasonWaiting":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "죄송합니다.\r\n현재 인터넷이 불안정하여 QR코드 제공이 어렵습니다.";
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "I'm sorry.\r\nYou can't use mode.";
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "绐您添麻烦子, 真抱歉。 目前不能使用。";
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "すみません。 現在のモードは利用できません。";
                        break;
                }
                ButType_1.SetActive(true);
                ButType_2.SetActive(false);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "See360View":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "화면을 터치하여 360 View를\r\n관람해 주세요.";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "Touch the screen to view 360 photo.";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "请按屏幕观看 360 view。";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "画面をタッチして360 Viewをご覧ください。";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                }
                ButType_1.SetActive(true);
                ButType_2.SetActive(false);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "ChangeOperation":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "수동조작 모드로 변경하시겠습니까?";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "Do you want to change to manual operation mode?";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "确定要更改为手动操作模式吗？";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "手動操作モードに変更しますか？";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                }
                ButType_1.SetActive(false);
                ButType_2.SetActive(true);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "VisitClickCap":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "사진촬영 기능이 제공되지 않는 모드입니다.\r\n(실시간, XR, 맑은날모드 내에서 사용가능합니다.)";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "Mode Not Supported\r\n(Use only for Live, XR and Clear Mode)";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "不支持模式\r\n(仅用于实时、XR和清除模式)";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "モードがサポートされていない\r\n（ライブ、XR、およびクリア モードでのみ使用";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                }
                ButType_1.SetActive(true);
                ButType_2.SetActive(false);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "VisitCancel":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "작성을 취소하시겠습니까?\r\n작성중인 내용은 삭제됩니다.";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "Are you sure you want to cancel?\r\nType will be deleted.";
                        NoticeText.font = gamemanager.label.Titlefont_KE;
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "确定要取消吗？\r\n类型将被删除";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "キャンセルしてよろしいですか？\r\nタイプが削除されます";
                        NoticeText.font = gamemanager.label.Titlefont_CJ;
                        break;
                }
                ButType_1.SetActive(false);
                ButType_2.SetActive(true);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "StopSelfi":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "사진찍기를 그만하시겠습니까?";
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "";
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "";
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "";
                        break;
                }
                ButType_1.SetActive(false);
                ButType_2.SetActive(true);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "StopSelfiCustom":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "사진꾸미기를 그만하시겠습니까?\r\n해당 사진은 삭제됩니다.";
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "";
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "";
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "";
                        break;
                }
                ButType_1.SetActive(false);
                ButType_2.SetActive(true);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "DontSaveSelfi":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "셀피모드에서 나가시겠습니까?\r\n해당 사진은 삭제됩니다.";
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "";
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "";
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "";
                        break;
                }
                ButType_1.SetActive(false);
                ButType_2.SetActive(true);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "HideArea":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "비공개 영역입니다. 이동해주세요.";
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "";
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "";
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "";
                        break;
                }
                ButType_1.SetActive(false);
                ButType_2.SetActive(true);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "ResetNotice":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = ((int)(60 - GameManager.touchCount)).ToString() + "초 후 초기화면으로 돌아갑니다.";
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "Return to the initial screen after "+ ((int)(60 - GameManager.touchCount)).ToString() + " seconds.";
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = ((int)(60 - GameManager.touchCount)).ToString() + "秒后回到初始画面";
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = ((int)(60 - GameManager.touchCount)).ToString() + "秒後に初期画面に戻ります.";
                        break;
                }
                ButType_1.SetActive(true);
                ButType_2.SetActive(false);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "SeeFourSeason":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "사계절 버튼을 선택해 다른 계절의 맑은날 풍경도 감상해보세요!";
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "Select the four seasons button to enjoy the scenery on a Clear day in other seasons!";
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "请选择四季按钮，欣赏其他季节的晴天风景！";
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "四季のボタンを選んで他の季節の晴れた日の風景も鑑賞してみてください！";
                        break;
                }
                ButType_1.SetActive(false);
                ButType_2.SetActive(true);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "PantiltOrigin":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "안내가 정확하지 않다면 잠시 정비할까요?";
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "If the instructions are not correct, should we fix it for a while?";
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "如果介绍不准确的话，要暂时整顿一下吗？";
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "案内が正確でなければ、しばらく整備しましょうか？";
                        break;
                }
                ButType_1.SetActive(false);
                ButType_2.SetActive(true);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "GoXRMode":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "시야가 좋아서 잘보이네요! XR모드를 이용하여 실시간 풍경을 감상해보세요.";
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "This place has a good view! Enjoy real-time scenery using XR mode.";
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "视野很好，看得清楚！ 请使用XR模式实时欣赏风景。";
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "視野が良くてよく見えますね！ XRモードを利用してリアルタイムの風景を鑑賞してみてください。";
                        break;
                }
                ButType_1.SetActive(false);
                ButType_2.SetActive(true);
                NoticeIcon.sprite = CheckIcon;
                break;
            case "GoClearMode":
                switch (GameManager.currentLang)
                {
                    case GameManager.Language_enum.Korea:
                        NoticeText.text = "시야가 좋지 않아요. 맑은날모드를 이용하여 맑은날 풍경을 감상해보세요.";
                        break;
                    case GameManager.Language_enum.English:
                        NoticeText.text = "The view isn't good here. Use ClearMode to enjoy the scenery on a clear day.";
                        break;
                    case GameManager.Language_enum.Chinese:
                        NoticeText.text = "这里的视野不是很好。 利用晴天模式欣赏晴天的风景。";
                        break;
                    case GameManager.Language_enum.Japanese:
                        NoticeText.text = "ここは視界がよくありません。 晴れた日モードで晴れた日の風景を鑑賞してみてください。";
                        break;
                }
                ButType_1.SetActive(false);
                ButType_2.SetActive(true);
                NoticeIcon.sprite = CheckIcon;
                break;
        }
    }

    public static void NoticeYes()
    {
        switch (NoticeState)
        {
            case "ErrorMessage":
                break;
            case "ErrorInternet":
                FunctionCustom.functionorigin.capturemode.CaptureEndCamera();
                break;
            case "SeasonWaiting":
                break;
            case "See360View":
                break;
            case "ChangeOperation":

                break;
            case "VisitClickCap":
                
                break;
            case "VisitCancel":
                gamemanager.visitmanager.RealOut();
                gamemanager.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case "StopSelfi":
                gamemanager.selfifunction.FinishSelfi();
                gamemanager.CaptureBtn.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case "StopSelfiCustom":
                gamemanager.selfifunction.FinishSelfi();
                gamemanager.CaptureBtn.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case "DontSaveSelfi":
                gamemanager.selfifunction.FinishSelfi();
                gamemanager.CaptureBtn.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case "SeeFourSeason":
                FunctionCustom.functionorigin.SeasonPano.SeasonChange();
                break;
            case "PantiltOrigin":
                //gamemanager.pantiltorigin.StartOrigin = false;
                //PantiltOrigin.State = PantiltOrigin.OriginState.SetOrigin;
                gamemanager.xrmode.playtime = 0;
                break;
            case "GoXRMode":
                gamemanager.Menu(gamemanager.MenuBar.transform.GetChild(0).GetChild(1).gameObject);
                break;
            case "GoClearMode":
                gamemanager.Menu(gamemanager.MenuBar.transform.GetChild(0).GetChild(2).gameObject);
                break;
        }
        gamemanager.ErrorMessage.SetActive(false);
        gamemanager.ButtonClickSound();
    }

    public static void NoticeNo()
    {
        gamemanager.ErrorMessage.SetActive(false);
        gamemanager.ButtonClickSound();
    }
}
