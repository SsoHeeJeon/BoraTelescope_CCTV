using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BroadcastCam : MonoBehaviour
{
    public GameManager gamemanager;
    public GameObject RecordingCam;
    public SunAPITest.CCTVControl cctvcontrol;
    //public AllTimeRecord alltimerecord;
    public static bool firstdontdestroy = false;

    private Vector3 recordcamPos_XR;
    private Vector3 recordcamPos_Clear = new Vector3(0, 20000, 0);

    // Start is called before the first frame update
    public void ReadytoStart()
    {
        if (firstdontdestroy == false)
        {
            if (SceneManager.GetActiveScene().name.Contains("XRMode"))
            {
                RecordingCam = gamemanager.xrmode.CameraWindow;
                cctvcontrol = gamemanager.xrmode.cctvcontrol;
            }
            else
            {
                RecordingCam = gamemanager.clearmode.CameraWindow;
            }
            recordcamPos_XR = RecordingCam.transform.position;
            DontDestroyOnLoad(RecordingCam);
            firstdontdestroy = true;
        }
        else if (firstdontdestroy == true)
        {
            if (RecordingCam == null)
            {
                if (SceneManager.GetActiveScene().name.Contains("XRMode"))
                {
                    RecordingCam = gamemanager.xrmode.CameraWindow;
                    cctvcontrol = gamemanager.xrmode.cctvcontrol;
                }
                else
                {
                    RecordingCam = gamemanager.clearmode.CameraWindow;
                }
                DontDestroyOnLoad(RecordingCam);
            }
            else if (RecordingCam != null)
            {
                if (SceneManager.GetActiveScene().name.Contains("XRMode"))
                {
                    gamemanager.xrmode.CameraWindow.gameObject.SetActive(false);
                    gamemanager.xrmode.CameraWindow = RecordingCam;
                    gamemanager.xrmode.CameraWindow.transform.position = recordcamPos_XR;
                    GameObject.Find("Canvas_Label").GetComponent<Canvas>().worldCamera = RecordingCam.transform.GetChild(1).gameObject.GetComponent<Camera>();  // Label Canvas EventCamera setting
                    gamemanager.xrmode.UICam = RecordingCam.transform.GetChild(1).gameObject.GetComponent<Camera>();
                    gamemanager.xrmode.cctvcontrol = cctvcontrol;
                }
                else
                {
                    gamemanager.clearmode.CameraWindow.gameObject.SetActive(false);
                    gamemanager.clearmode.CameraWindow = RecordingCam;
                    gamemanager.clearmode.CameraWindow.transform.position = recordcamPos_XR;
                }
            }
        }
    }

    public void ChangeRecordCamPos()
    {
        RecordingCam.transform.position = recordcamPos_Clear;
    }
}
