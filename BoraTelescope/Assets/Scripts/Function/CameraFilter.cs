using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[ExecuteInEditMode]
public class CameraFilter : MonoBehaviour
{
    RenderTexture source;
    RenderTexture destination;

    public Material NullMaterial;
    public Material originMaterial;
    public Material PancilSkatch;
    public Material OilMaterial;
    public Material SharpenMaterial;
    public Material GaussianBlurMaterial;
    public Material TestImageMaterial;
    public Material SoftLightMaterial;

    public Material SpringMaterial;
    public Material SummerMaterial;
    public Material FallMaterial;
    public Material WinterMaterial;

    //public Slider SharpValue;
    //public Slider BlurValue;
    //public Slider SoftValue;
    //public Slider SoftValue_2;

    //[Range(5f, 10f)]
    public float sharpenAmount = 8f;

    [Range(0f, 1f)]
    [HideInInspector]
    public float maximumSharpness = 0.25f;

    [Range(0f, 2f)]
    [HideInInspector]
    public float sharpenRadius = 1.0f;

    [HideInInspector]
    public float bluramount = 1.0f;
    [HideInInspector]
    public float Soft_tint_1 = 1.0f;
    [HideInInspector]
    public float Soft_tint_2 = 1.0f;

    /*
    public Color shadowcolor;
    public float ShadowThreshold = 0.5f;
    */
    public static bool StartPancilSketch = false;
    
    // Start is called before the first frame update
    void Start()
    {
        sharpenAmount = 20;
        bluramount = 0.2f;
        Soft_tint_1 = 1;
        Soft_tint_2 = 1;
        /*
        if(SharpValue != null && SceneManager.GetActiveScene().name.Contains("ARMode"))
        {
            SharpValue = GameObject.Find("Canvas").transform.GetChild(1).gameObject.GetComponent<Slider>();
            BlurValue = SharpValue;
            SoftValue = SharpValue;
            SoftValue_2 = SharpValue;
        }

        sharpenAmount = SharpValue.value * 20;
        bluramount = BlurValue.value * 0.2f;
        Soft_tint_1 = SoftValue.value;
        Soft_tint_2 = SoftValue_2.value;
        */
    }
    /*
    // Update is called once per frame
    void Update()
    {
        Debug.Log(StartPancilSketch);
    }
    */
    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        if (NullMaterial == SharpenMaterial)
        {
            //sharpenAmount = SharpValue.value * 20;
            NullMaterial.SetFloat("_SharpenAmount", sharpenAmount);
            NullMaterial.SetFloat("sharp_clamp", maximumSharpness);
            NullMaterial.SetFloat("offset_bias", sharpenRadius);

            Graphics.Blit(source, destination, NullMaterial, 0);
        }
        else if (NullMaterial == GaussianBlurMaterial)
        {
            if (StartPancilSketch == false)
            {
                //bluramount = BlurValue.value;
                //NullMaterial.SetFloat("Blur Size", bluramount);
                //NullMaterial.SetColor("Tint on Texture", new Color(1, 1, 1, Soft_tint_1));
                //NullMaterial.SetColor("Tint on Blended Result", new Color(1, 1, 1, Soft_tint_2));

                var temporaryTexture = RenderTexture.GetTemporary(source.width, source.height);
                var temporaryTexture_1 = RenderTexture.GetTemporary(source.width, source.height);
                Graphics.Blit(source, temporaryTexture, NullMaterial, 0);
                Graphics.Blit(temporaryTexture, temporaryTexture_1, NullMaterial, 1);
                Graphics.Blit(temporaryTexture_1, destination, NullMaterial, 2);
                RenderTexture.ReleaseTemporary(temporaryTexture);
                Debug.Log("gau");
            }
            else if (StartPancilSketch == true)
            {
                //bluramount = BlurValue.value;
                NullMaterial.SetFloat("Blur Size", bluramount);
                NullMaterial.SetColor("Tint on Texture", new Color(1, 1, 1, Soft_tint_1));
                NullMaterial.SetColor("Tint on Blended Result", new Color(1, 1, 1, Soft_tint_2));
                var temporaryTexture = RenderTexture.GetTemporary(source.width, source.height);
                Graphics.Blit(source, temporaryTexture, NullMaterial, 0);
                Graphics.Blit(temporaryTexture, destination, NullMaterial, 1);
                RenderTexture.ReleaseTemporary(temporaryTexture);

                NullMaterial = SoftLightMaterial;
                Graphics.Blit(source, destination, NullMaterial);
                Debug.Log("plus");
            }
        }
        else if (NullMaterial == TestImageMaterial)
        {/*
            NullMaterial.SetFloat("_ShadowThreshold", ShadowThreshold);
            NullMaterial.SetColor("_shadowColor", shadowcolor);
            Graphics.Blit(source, destination, NullMaterial);*/
        }
        else
        {
            if (NullMaterial == SoftLightMaterial)
            {
                NullMaterial.SetColor("Tint on Texture", new Color(1, 1, 1, Soft_tint_1));
                NullMaterial.SetColor("Tint on Blended Result", new Color(1, 1, 1, Soft_tint_2));
                Debug.Log("soft");
            }

            Graphics.Blit(source, destination, NullMaterial);
        }
    }
}