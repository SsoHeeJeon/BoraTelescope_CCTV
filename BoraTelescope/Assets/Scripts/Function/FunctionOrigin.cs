using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionOrigin : MonoBehaviour
{
    public GameManager gamemanager;
    public ContentsOtherList otherlist;
    public CaptureMode capturemode;
    public Clear_Pano SeasonPano;
    public View360 view360;
    public PastContents pastcontents;
    public GuideMode guidemode;
    public FilterEffect filtereffect;
    public FilterFunction filterfunction;
    public SelfiFunction selfifunction;
    public TourismMode tourisimmode;

    public void FilterReset()
    {
        if (filterfunction.FilterBar.transform.localPosition.y < 720)
        {
            filtereffect.FinishFilterEffect();
            filterfunction.filternavi_t = 0;
            FilterFunction.FilterBarMoveOn = true;
            filterfunction.FilterBarMove = false;

            gamemanager.FilterBtn.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 카메라 필터Bar OnOff 및 각 필터OnOff
    /// </summary>
    /// <param name="btn"></param>
    public void CameraEffect()
    {
        if (FilterFunction.FilterBarMoveOn == false)
        {
            FilterFunction.FilterBarMoveOn = true;
            if (filterfunction.FilterBar.transform.localPosition.y < 720)
            {
                filterfunction.filternavi_t = 0;
                filterfunction.FilterBarMove = false;
                gamemanager.FilterBtn.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (filterfunction.FilterBar.transform.localPosition.y > 508)
            {
                filterfunction.filternavi_t = 0;
                filterfunction.FilterBarMove = true;
                gamemanager.FilterBtn.transform.GetChild(0).gameObject.SetActive(true);

                if (SeasonPano.seasonBar.transform.localPosition.y < 720)
                {
                    SeasonPano.SeasonChange();
                }
            }
        }
    }

    public void OnceLiveXR_notice()
    {
        if (GameManager.PrevMode == "WaitingMode" || GameManager.PrevMode == null)
        {
            gamemanager.xrmode.XRToggle.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            if (XRMode.FirstXRClick == false)
            {
                gamemanager.xrmode.XRToggle.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (XRMode.FirstXRClick == true)
            {
                gamemanager.xrmode.XRToggle.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
