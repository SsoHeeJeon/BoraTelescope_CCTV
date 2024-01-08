using Amazon.IoTSiteWise.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitHomeObj : MonoBehaviour
{
    public enum State
    {
        Idle,
        Animation,
    }
    public State state = 0;
    public float speed;
    RectTransform rec;
    [SerializeField]
    GameObject BackGround;
    [SerializeField]
    public GameObject CloseBtn;
    [SerializeField]
    Visitmanager visitmanager;
    public Vector2 PrePos;
    // Start is called before the first frame update
    void Start()
    {
        rec = GetComponent<RectTransform>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Animation)
        {
            rec.anchoredPosition = Vector2.Lerp(rec.anchoredPosition, new Vector2(509, -200), Time.deltaTime* speed);
            rec.sizeDelta = Vector3.Lerp(rec.sizeDelta, new Vector3(640, 640, 640), Time.deltaTime* speed);

            float dist = Vector2.Distance(rec.anchoredPosition, new Vector2(509, -200));
            if(dist<10 && rec.sizeDelta.x>635)
            {
                rec.anchoredPosition = new Vector2(509, -200);
                rec.sizeDelta = new Vector3(640, 640, 640);
                CloseBtn.SetActive(true);
                state = State.Idle;
            }
        }
    }

    public void OnCLikcBtn()
    {
        if(transform.parent == BackGround.GetComponent<ScrollRect>().content)
        {
            visitmanager.gamemanager.WriteLog(LogSendServer.NormalLogCode.Visit_See, "GuestSee", GetType().ToString());
            PrePos = transform.localPosition;
            transform.parent = BackGround.transform;
            BackGround.GetComponent<ScrollRect>().content.gameObject.SetActive(false);
            state = State.Animation;
        }
        else if(transform.parent = BackGround.transform)
        {
            if(state == State.Idle)
            {
                OnClickCloseBtn();
            }
        }
    }

    public void OnClickCloseBtn()
    {
        transform.parent = BackGround.GetComponent<ScrollRect>().content;
        transform.localPosition = PrePos;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector3(360f, 360f, 360f);
        PrePos = Vector2.zero;
        BackGround.GetComponent<ScrollRect>().content.gameObject.SetActive(true);
        CloseBtn.SetActive(false);
    }
}
