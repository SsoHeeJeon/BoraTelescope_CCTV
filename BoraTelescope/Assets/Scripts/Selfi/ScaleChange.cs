using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    public SelfiFunction selfifunc;

    public static bool changeStart = false;
    Vector2 startposition;
    Vector2 moveposition;
    float changescale;
    public GameObject Imageobj;
    float startScale;

    // Start is called before the first frame update
    void Start()
    {
        startScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeStart == true)
        {
            selfifunc.gamemanager.touchuiobj.UITouchOn();
            if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                startposition = Input.GetTouch(0).position;
                startScale = Imageobj.transform.localScale.x;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //��ġ������ ���� �簢�� �̵�
                moveposition = Input.GetTouch(0).position;
                
                changescale = Vector2.Distance(startposition, moveposition) * 0.001f;

                if ((startposition.x - Camera.main.WorldToScreenPoint(Imageobj.transform.position).x) > 0)
                {
                    if (((moveposition.x - Camera.main.WorldToScreenPoint(Imageobj.transform.position).x) > 0 && (moveposition.x - startposition.x) < 0)
                        || ((moveposition.x - Camera.main.WorldToScreenPoint(Imageobj.transform.position).x) < 0 && (moveposition.x - startposition.x) < 0))
                    {
                        changescale *= -1;
                    }
                } else if (startposition.x - Camera.main.WorldToScreenPoint(Imageobj.transform.position).x < 0)
                {
                    if (((moveposition.x - Camera.main.WorldToScreenPoint(Imageobj.transform.position).x) > 0 && (moveposition.x - startposition.x) > 0)
                        ||  ((moveposition.x - Camera.main.WorldToScreenPoint(Imageobj.transform.position).x) < 0 && (moveposition.x - startposition.x) > 0))
                    {
                        changescale *= -1;
                    }
                }

                if (changescale != 0)
                {
                    if (startScale + changescale != Imageobj.transform.localScale.x && (startScale + changescale) > 0.1f)
                    {
                        Imageobj.transform.localScale = new Vector3(startScale + changescale, startScale + changescale, startScale + changescale);
                    }
                }
            } else if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                startScale = Imageobj.transform.localScale.x;
                selfifunc.SelectItem = null;
                FinishChange();
            }
        }
    }

    public void SetChange(GameObject obj)
    {
        selfifunc.drawing.enabled = false;
        changeStart = true;
        Imageobj = obj;
        //selfifunc.Scale_obj.color = new Color(1, 1, 1, 1);
        selfifunc.rotation_obj.gameObject.SetActive(false);
        selfifunc.position_obj.gameObject.SetActive(false);
        selfifunc.rotation_obj.raycastTarget = false;
        selfifunc.position_obj.raycastTarget = false;

        //selfifunc.SaveUndo(Imageobj, "PRS");
    }

    public void FinishChange()
    {
        selfifunc.SaveUndo(Imageobj.name, Imageobj, "PRS");
        changeStart = false;
        selfifunc.gamemanager.touchuiobj.UITouchOff();
        Imageobj = null;
        selfifunc.SelectItem = null;
        //selfifunc.Scale_obj.color = new Color(1, 1, 1, 1);
        selfifunc.rotation_obj.gameObject.SetActive(true);
        selfifunc.position_obj.gameObject.SetActive(true);
        selfifunc.rotation_obj.raycastTarget = true;
        selfifunc.position_obj.raycastTarget = true;
    }
}