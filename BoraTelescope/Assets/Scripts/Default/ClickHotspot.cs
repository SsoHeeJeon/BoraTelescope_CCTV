using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickHotspot : MonoBehaviour
{
    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void GoHotspot(GameObject spot)
    {
        GM.minimap.SelectHotspot(spot);
        GM.ButtonClickSound();
    }
}
