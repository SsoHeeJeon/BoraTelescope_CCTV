//using PanTiltControl_v2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinimapCustom : MonoBehaviour
{
    public GameManager gamemanager;

    public List<string> Hotspot = new List<string>();
    public GameObject Hotspot_Obj;
    public GameObject Hotspot_parent;
    public Sprite[] hotspot_img;

    public static bool Hotspotclick = false;

    public void SettingHotspot()
    {
        if (Hotspot_parent.gameObject.transform.childCount > 0)
        {
            for (int index = Hotspot_parent.gameObject.transform.childCount - 1; index >= 0; index--)
            {
                Destroy(Hotspot_parent.gameObject.transform.GetChild(index).gameObject);
            }
        }

        // 미니맵 위에 주요지점 버튼 생성
        for (int index = 0; index < Hotspot.Count; index++)
        {
            GameObject obj = Instantiate(Hotspot_Obj);
            obj.transform.SetParent(Hotspot_parent.transform);
            obj.name = Hotspot[index];
            obj.transform.GetChild(0).GetComponent<Image>().sprite = hotspot_img[index];
            obj.transform.GetChild(0).GetComponent<Image>().SetNativeSize();

            if (SceneManager.GetActiveScene().name.Contains("XRMode"))
            {
                for (int Sindex = 0; Sindex < gamemanager.xrmode.AllMapLabels.transform.childCount; Sindex++)
                {
                    if (obj.name == gamemanager.xrmode.AllMapLabels.transform.GetChild(Sindex).gameObject.name)
                    {
                        if (gamemanager.xrmode.AllMapLabels.transform.GetChild(Sindex).gameObject.transform.position.x / XRMode_Manager.TotalPan <= XRMode_Manager.MaxPan && gamemanager.xrmode.AllMapLabels.transform.GetChild(Sindex).gameObject.transform.position.x / XRMode_Manager.TotalPan >= XRMode_Manager.MinPan)
                        {
                            float totalminimap = (gamemanager.minimap.mapX.rect.width - gamemanager.minimap.mapCamX.rect.width);
                            //float objx = (gamemanager.xrmode.AllMapLabels.transform.GetChild(Sindex).gameObject.transform.position.x) * totalminimap / (XRMode_Manager.MaxPan*XRMode_Manager.TotalPan - XRMode_Manager.MinPan * XRMode_Manager.TotalPan * XRMode_Manager.TotalPan);
                            float objx = (gamemanager.xrmode.AllMapLabels.transform.GetChild(Sindex).gameObject.transform.position.x / XRMode_Manager.TotalPan - XRMode_Manager.MinPan) / (XRMode_Manager.MaxPan - XRMode_Manager.MinPan) * (totalminimap) - gamemanager.minimap.mapX.rect.width / 2 + gamemanager.minimap.mapCamX.rect.width / 2;
                            obj.transform.localPosition = new Vector3(objx, 0);
                        } else
                        {
                            obj.GetComponent<Button>().enabled = false;
                            obj.SetActive(false);
                        }
                    }
                }
            } else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                for (int Sindex = 0; Sindex < gamemanager.clearmode.AllMapLabels.transform.childCount; Sindex++)
                {
                    if (obj.name == gamemanager.clearmode.AllMapLabels.transform.GetChild(Sindex).gameObject.name)
                    {
                        float totalminimap = (gamemanager.minimap.mapX.rect.width - gamemanager.minimap.mapCamX.rect.width);
                        //float objx = (gamemanager.clearmode.AllMapLabels.transform.GetChild(Sindex).gameObject.transform.position.x)*totalminimap / (gamemanager.clearmode.max_x - gamemanager.clearmode.min_x);
                        float objx = (gamemanager.clearmode.AllMapLabels.transform.GetChild(Sindex).gameObject.transform.position.x - gamemanager.clearmode.min_x) / (gamemanager.clearmode.max_x - gamemanager.clearmode.min_x) * (totalminimap) - gamemanager.minimap.mapX.rect.width / 2 + gamemanager.minimap.mapCamX.rect.width / 2;
                        obj.transform.localPosition = new Vector3(objx, 0);
                    }
                }
            }
        }
    }

    public void SelectHotspot(GameObject btn)
    {
        //PanTiltControl.Stop();
        Hotspotclick = true;
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            StartCoroutine(MoveHotspot_XR(btn));
            gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Hotspot, "XR_Hotspot : " + btn.name, GetType().ToString());
        } else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            MoveHotspot_Clear(btn);
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Hotspot, "Clear_Hotspot : " + btn.name, GetType().ToString());
        }
    }

    IEnumerator MoveHotspot_XR(GameObject btn)
    {
        //PanTiltControl.Stop();
        yield return new WaitForSeconds(0.1f);
        //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Fast);
        //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Fast);
        gamemanager.speed_enum = GameManager.Speed_enum.fast;

        for (int index = 0; index < gamemanager.xrmode.AllMapLabels.transform.childCount; index++)
        {
            if (btn.name == gamemanager.xrmode.AllMapLabels.transform.GetChild(index).gameObject.name)
            {
                if (gamemanager.WantNoLabel == false)
                {
                    gamemanager.xrmode.SelectMapLabel(gamemanager.xrmode.AllMapLabels.transform.GetChild(index).gameObject);
                }
                else if (gamemanager.WantNoLabel == true)
                {
                    gamemanager.xrmode.xpulse = gamemanager.label.Label_Position[index].x / XRMode.ValueX;
                    gamemanager.xrmode.ypulse = gamemanager.label.Label_Position[index].y / XRMode.ValueY;
                    gamemanager.xrmode.MoveCamera_Navi();
                }
            }
        }
        Hotspotclick = false;
        gamemanager.MiniMap_CameraGuide.SetActive(false);
    }

    public void MoveHotspot_Clear(GameObject btn)
    {
        for (int index = 0; index < gamemanager.clearmode.AllMapLabels.transform.childCount; index++)
        {
            if (btn.name == gamemanager.clearmode.AllMapLabels.transform.GetChild(index).gameObject.name)
            {
                gamemanager.clearmode.SelectMapLabel(gamemanager.clearmode.AllMapLabels.transform.GetChild(index).gameObject);
            }
        }
        Hotspotclick = false;
        gamemanager.MiniMap_CameraGuide.SetActive(false);
    }
}
