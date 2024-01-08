using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDragRange : MonoBehaviour
{
    //public GameManager gamemanager;

    public static bool StopMove = false;

    public static void TouchStart()
    {
        StopMove = true;
    }

    public static void TouchFinish()
    {
        StopMove = false;
    }

    public void ALLFuncDragRange()
    {
        /*
        if (gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose && gamemanager.xrmode.labeldetail.Detail_Background.transform.localPosition.x != gamemanager.xrmode.labeldetail.Detail_Close_x)  // 네비게이션 비활성화, 상세설명 활성화
        {
            if (Input.GetTouch(0).position.y < 350)
            {
                if (Input.GetTouch(0).position.x <= 1400 && Input.GetTouch(0).position.x >= 430 && Input.GetTouch(0).position.y > 150)
                {
                    //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{
                    gamemanager.arrowmove.DragArrow();
                    //} else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{
                    //    gamemanager.see360.SeeAllRound();
                    //}
                }
                else
                {
                    //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{
                    gamemanager.arrowmove.OtherDragRange();
                    //}
                    //else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{

                    //}
                }
            }
            else if (Input.GetTouch(0).position.y >= 350)
            {
                if (Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.x <= 1400)
                {
                    //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{
                    gamemanager.arrowmove.DragArrow();
                    //}
                    //else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{
                    //    gamemanager.see360.SeeAllRound();
                    //}
                }
                else
                {
                    //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{
                    gamemanager.arrowmove.OtherDragRange();
                    //}
                    //else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{

                    //}
                }
            }
        }
        else if (gamemanager.allbar.NaviRect.sizeDelta.x == AllBarOnOff.barClose && gamemanager.xrmode.labeldetail.Detail_Background.transform.localPosition.x == gamemanager.xrmode.labeldetail.Detail_Close_x)   // 네비게이션 비활성화, 상세설명 비활성화
        {

            if (Input.GetTouch(0).position.x <= 430 && Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.y <= 350 && Input.GetTouch(0).position.y >= 0)
            {
                //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                //{
                gamemanager.arrowmove.OtherDragRange();
                //}
                //else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                //{

                //}
            }
            else
            {
                if (Input.GetTouch(0).position.y > 150)
                {
                    //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{
                    gamemanager.arrowmove.DragArrow();
                    //}
                    //else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{
                    //    gamemanager.see360.SeeAllRound();
                    //}
                }
                else if(Input.GetTouch(0).position.y <= 150)
                {
                    //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{
                    gamemanager.arrowmove.OtherDragRange();
                    //}
                    //else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                    //{

                    //}
                }
            }

        }
        else if (gamemanager.allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose && gamemanager.xrmode.labeldetail.Detail_Background.transform.localPosition.x != gamemanager.xrmode.labeldetail.Detail_Close_x)  // 네비게이션 활성화, 상세설명 활성화
        {
            if (Input.GetTouch(0).position.x >= 410 && Input.GetTouch(0).position.x <= 1400 && Input.GetTouch(0).position.y > 150)
            {
                //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                //{
                gamemanager.arrowmove.DragArrow();
                //}
                //else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                //{
                //    gamemanager.see360.SeeAllRound();
                //}
            }
            else
            {
                //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                //{
                gamemanager.arrowmove.OtherDragRange();
                //}
                //else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                //{

                //}
            }
        }
        else if (gamemanager.allbar.NaviRect.sizeDelta.x > AllBarOnOff.barClose && gamemanager.xrmode.labeldetail.Detail_Background.transform.localPosition.x == gamemanager.xrmode.labeldetail.Detail_Close_x)  // 네비게이션 활성화, 상세설명 비활성화
        {
            if (Input.GetTouch(0).position.x >= 480 && Input.GetTouch(0).position.y > 150)
            {
                //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                //{
                gamemanager.arrowmove.DragArrow();
                //}
                //else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                //{
                //    gamemanager.see360.SeeAllRound();
                //}
            }
            else
            {
                //if (!gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                //{
                    gamemanager.arrowmove.OtherDragRange();
                //}
                //else if (gamemanager.see360.obj360.transform.parent.gameObject.activeSelf)
                //{
                    
                //}
            }
        }*/
    }

    public void TelescopeDragRange()
    {
        /*
        if (Input.GetTouch(0).position.x <= 430 && Input.GetTouch(0).position.x >= 70 && Input.GetTouch(0).position.y <= 300 && Input.GetTouch(0).position.y >= 0)
        {
            gamemanager.OtherDragRange();
        }
        else
        {
            if (Input.GetTouch(0).position.y > 150)
            {
                //if (Input.GetTouch(0).position.x >= 600 && Input.GetTouch(0).position.x <= 1360 && Input.GetTouch(0).position.y >= 900 && Input.GetTouch(0).position.y <= 1015)
                //{
                //    gamemanager.OtherDragRange();
                //}
                //else
                //{
                gamemanager.DragArrow();
                //}
            }
        }
        */
    }
}
