//using PanTiltControl_v2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class VariableJoystick : Joystick
{
    public BoraJoyStick joystick;
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;
    [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

    private Vector2 fixedPosition = Vector2.zero;


    public void SetMode(JoystickType joystickType)
    {
        this.joystickType = joystickType;
        if(joystickType == JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition;
            background.gameObject.SetActive(true);
        }
        else
            background.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition;
        SetMode(joystickType);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (joystick.GM.ZoomBar.transform.localPosition.y == 30.5f)
        {
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Slow);
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Slow);
            joystick.GM.speed_enum = GameManager.Speed_enum.slow;
        }
        else
        {
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Pan, PanTiltControl.Speed.Middle);
            //PanTiltControl.SetFreq(PanTiltControl.Motor.Tilt, PanTiltControl.Speed.Middle);
            joystick.GM.speed_enum = GameManager.Speed_enum.middle;
        }
        if (joystick.GM.MiniMap_CameraGuide.activeSelf)
        {
            joystick.GM.MiniMap_CameraGuide.gameObject.SetActive(false);
        }
        for (int index = 1; index < joystick.GM.Arrow.transform.childCount; index++)
        {
            joystick.GM.Arrow.transform.GetChild(index).gameObject.SetActive(false);
        }
        joystick.enabled = true;
        if(joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
        }
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            if (joystick.alreadyjoystick == true)
            {
                joystick.GM.WriteLog(LogSendServer.NormalLogCode.AR_Joystick, "XR_Joystick : Finish", GetType().ToString());
                joystick.alreadyjoystick = false;
            }
            joystick.GM.xrmode.Resetothers();
        } else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            if (joystick.alreadyjoystick == true)
            {
                joystick.GM.WriteLog(LogSendServer.NormalLogCode.Clear_Joystick, "Clear_Joystick : Finish", GetType().ToString());
                joystick.alreadyjoystick = false;
            }
            joystick.GM.clearmode.Resetothers();
        }
        //PanTiltControl.Stop();
        joystick.GM.xrmode.cctvcontrol.StopControl();
        for (int index = 1; index < joystick.GM.Arrow.transform.childCount; index++)
        {
            joystick.GM.Arrow.transform.GetChild(index).gameObject.SetActive(true);
        }
        joystick.enabled = false;
        if(joystickType != JoystickType.Fixed)
            background.gameObject.SetActive(false);

        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}

public enum JoystickType { Fixed, Floating, Dynamic }