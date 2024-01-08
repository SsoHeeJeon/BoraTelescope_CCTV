using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideMode : MonoBehaviour
{
    public GameManager gamemanager;
    public GameObject GuideObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuideLanguage()
    {

    }

    public void CloaseGuide()
    {
        GuideObj.SetActive(false);
        if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            if(gamemanager.WantNoLabel == false)
            {
                gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
            }
            else if(gamemanager.WantNoLabel == true)
            {
                gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            }
        } else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            gamemanager.MenuBar.transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
        }

        gamemanager.GuideModeBtn.transform.GetChild(0).gameObject.SetActive(false);
    }
}
