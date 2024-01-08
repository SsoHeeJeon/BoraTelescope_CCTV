using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnounceMode : MonoBehaviour
{
    public GameObject[] Announce_O = new GameObject[4];

    public static bool DontAnnounce = false;

    public void OpenMode(string mode)
    {
        switch (mode) {
            case "Live":
                Announce_O[0].SetActive(true);
                break;
            case "XR":
                Announce_O[1].SetActive(true);
                break;
            case "Clear":
                Announce_O[2].SetActive(true);
                break;
            case "Past":
                Announce_O[3].SetActive(true);
                break;
        }

        Invoke("CloseObj", 3f);
    }

    public void CloseObj()
    {
        for(int index = 0; index < Announce_O.Length; index++)
        {
            if (Announce_O[index].activeSelf)
            {
                Announce_O[index].SetActive(false);
            }
        }
    }
}
