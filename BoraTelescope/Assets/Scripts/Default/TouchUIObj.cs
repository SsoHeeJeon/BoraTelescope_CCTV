using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchUIObj : MonoBehaviour
{

    public void UITouchOn()     // 퀵메뉴, 상세설명, Filter등을 터치하고 있을 경우, 드래그 및 줌 금지
    {
        GameManager.UITouch = true;
    }

    public void UITouchOff()
    {
        GameManager.UITouch = false;
    }
}
