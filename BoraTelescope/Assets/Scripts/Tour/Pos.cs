using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour
{
    public string longitude;
    public string latitude;

    public void OnClickDirBtn(DirectionManager dir)
    {
        if(dir.longitudelist.Count == 0) 
        {
            dir.longitudelist.Add(longitude);
            dir.laitudelist.Add(latitude);
        }
        else
        {
            for(int i=0; i<dir.longitudelist.Count; i++) 
            {
                if (dir.longitudelist[i] == longitude && dir.laitudelist[i] == latitude)
                {
                    dir.longitudelist.RemoveAt(i);
                    dir.laitudelist.RemoveAt(i);
                    break;
                }
                else
                {
                    if(i == dir.longitudelist.Count-1)
                    {
                        dir.longitudelist.Add(longitude);
                        dir.laitudelist.Add(latitude);
                        break;
                    }
                }
            }
        }

        //if(dir.longitude == "")
        //{
        //    dir.longitude= longitude;
        //    dir.laitude= latitude;
        //}
        //else if(dir.longitude == longitude)
        //{
        //    dir.longitude = "";
        //    dir.laitude = "";
        //}
        //else
        //{
        //    dir.goallaitude= latitude;
        //    dir.goallongitude= longitude;
        //}
    }
}
