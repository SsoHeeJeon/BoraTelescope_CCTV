using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchUIObj : MonoBehaviour
{

    public void UITouchOn()     // ���޴�, �󼼼���, Filter���� ��ġ�ϰ� ���� ���, �巡�� �� �� ����
    {
        GameManager.UITouch = true;
    }

    public void UITouchOff()
    {
        GameManager.UITouch = false;
    }
}
