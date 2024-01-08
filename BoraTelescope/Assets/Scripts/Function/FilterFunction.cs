using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterFunction : MonoBehaviour
{
    public FunctionOrigin functionorigin;

    /// <summary>
    /// filter function
    /// </summary>
    public GameObject FilterBar;
    public Scrollbar FilterScroll;

    public float filternavi_t;
    bool changevaule = false;
    bool changevaule_left = false;
    bool changevaule_right = false;
    public static bool FilterBarMoveOn = false;
    public bool FilterBarMove = false;

    public void FilterBarOnOff()
    {
        if (FilterBarMove == false)
        {
            if (FilterBar.transform.localPosition.y < 720)      //filterOpen
            {
                functionorigin.gamemanager.FilterBtn.transform.GetChild(0).gameObject.SetActive(false);
                FilterBar.transform.localPosition = new Vector3(FilterBar.transform.localPosition.x, Mathf.Lerp(508, 720, filternavi_t), FilterBar.transform.localPosition.z);
            }
            else if (FilterBar.transform.localPosition.y >= 720)
            {
                FilterBar.transform.localPosition = new Vector3(FilterBar.transform.localPosition.x, 720, FilterBar.transform.localPosition.z);
                FilterBarMove = true;
                FilterBarMoveOn = false;
            }
        }
        else if (FilterBarMove == true)
        {
            if (FilterBar.transform.localPosition.y > 508)  //filterClose
            {
                functionorigin.gamemanager.FilterBtn.transform.GetChild(0).gameObject.SetActive(true);
                FilterBar.transform.localPosition = new Vector3(FilterBar.transform.localPosition.x, Mathf.Lerp(720, 508, filternavi_t), FilterBar.transform.localPosition.z);
            }
            else if (FilterBar.transform.localPosition.y <= 508)
            {
                FilterBar.transform.localPosition = new Vector3(FilterBar.transform.localPosition.x, 508, FilterBar.transform.localPosition.z);
                FilterBarMove = false;
                FilterBarMoveOn = false;
            }
        }
    }

    /// <summary>
    /// 카메라 필터Bar OnOff 및 각 필터OnOff
    /// </summary>
    /// <param name="btn"></param>
    public void CameraEffect(GameObject btn)
    {
        switch (btn.name)
        {
            case "None":
                FilterBar.GetComponent<FilterEffect>().FinishFilterEffect();
                break;
            case "SoftLight":
                FilterBar.GetComponent<FilterEffect>().OnOffSoftShader();
                break;
            case "SoftBlur":
                FilterBar.GetComponent<FilterEffect>().OnOffGaussianShader();
                break;
            case "Sharpen":
                FilterBar.GetComponent<FilterEffect>().OnOffSharpenShader();
                break;
            case "OilPaint":
                FilterBar.GetComponent<FilterEffect>().OnOffOilShader();
                break;
            case "PancilSketch":
                FilterBar.GetComponent<FilterEffect>().OnOffPancilSketch();
                break;
            case "Spring":
                FilterBar.GetComponent<FilterEffect>().OnOffSpringMaterial();
                break;
            case "Summer":
                FilterBar.GetComponent<FilterEffect>().OnOffSummerMaterial();
                break;
            case "Fall":
                FilterBar.GetComponent<FilterEffect>().OnOffFallMaterial();
                break;
            case "Winter":
                FilterBar.GetComponent<FilterEffect>().OnOffWinterMaterial();
                break;
            case "Left":
                changevaule = true;
                changevaule_left = true;
                //FilterBar.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Scrollbar>().value -= 0.02f;
                break;
            case "Right":
                changevaule = true;
                changevaule_right = true;
                //FilterBar.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Scrollbar>().value += 0.02f;
                break;
        }
    }

    // filterbar에서 화살표 꾹 누르고 있다가 뗄 때
    public void filtermoveOff()
    {
        changevaule = false;
        changevaule_left = false;
        changevaule_right = false;
    }

    public void FilterBarScroll()
    {
        if (changevaule == true)
        {
            if (changevaule_left == true)
            {
                FilterScroll.value -= 0.02f;
            }
            else if (changevaule_right == true)
            {
                FilterScroll.value += 0.02f;
            }
        }
    }
}
