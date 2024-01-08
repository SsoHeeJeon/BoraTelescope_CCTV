using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoDetail : MonoBehaviour
{
    public VideoPlayer PlayVideo;

    public void ChangeVideo(string label)
    {
        switch (label)
        {
            case "Gijungdong":
                PlayVideo.gameObject.SetActive(true);
                //PlayVideo.transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(468, 253);
                PlayVideo.gameObject.transform.localScale = new Vector3(1.043f, 1.043f, 1.043f);
                //PlayVideo.clip = Aegibong.Gijungdong;
                break;
            case "Daesungdong":
                PlayVideo.gameObject.SetActive(true);
                //PlayVideo.transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(468, 253);
                //PlayVideo.transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(468, 280);
                PlayVideo.gameObject.transform.localScale = new Vector3(1.043f, 1.043f, 1.043f);
                //PlayVideo.clip = Aegibong.Daesungdong;
                break;
        }
    }
}
