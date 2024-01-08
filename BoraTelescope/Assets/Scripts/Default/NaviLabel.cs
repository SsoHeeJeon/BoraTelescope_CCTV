using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NaviLabel : MonoBehaviour
{
    public GameManager GM;
    public Image LabelIcon;
    public Text labelname;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void ClickNavigationLabel(GameObject label)
    {
        for (int index = 0; index < GM.label.LabelsParent.transform.childCount; index++)
        {
            GM.label.LabelsParent.transform.GetChild(index).gameObject.GetComponent<Button>().enabled = false;
        }
        GM.ButtonClickSound();
        GM.Navigation(label);
    }
}
