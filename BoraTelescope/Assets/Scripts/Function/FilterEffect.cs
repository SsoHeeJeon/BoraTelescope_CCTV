using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FilterEffect : MonoBehaviour
{
    private CameraFilter PancilSketch;
    public GameManager gamemanager;
    public GameObject springparticle;
    public GameObject fallparticle;
    public GameObject winterparticle;

    // Start is called before the first frame update
    public void ReadyToFilter()
    {
        if (SceneManager.GetActiveScene().name.Contains("ARMode"))
        {
            PancilSketch = gamemanager.xrmode.CameraWindow.GetComponent<CameraFilter>();

            springparticle = gamemanager.xrmode.CameraWindow.transform.GetChild(5).gameObject;
            fallparticle = gamemanager.xrmode.CameraWindow.transform.GetChild(6).gameObject;
            winterparticle = gamemanager.xrmode.CameraWindow.transform.GetChild(7).gameObject;
        }
        else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            PancilSketch = gamemanager.clearmode.CameraWindow.GetComponent<CameraFilter>();

            springparticle = gamemanager.clearmode.CameraWindow.transform.GetChild(4).gameObject;
            fallparticle = gamemanager.clearmode.CameraWindow.transform.GetChild(5).gameObject;
            winterparticle = gamemanager.clearmode.CameraWindow.transform.GetChild(6).gameObject;
        }
    }
    public void OnOffPancilSketch()
    {
        springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

        if (springparticle.activeSelf)
        {
            springparticle.gameObject.SetActive(false);
        }
        else if (fallparticle.activeSelf)
        {
            fallparticle.gameObject.SetActive(false);
        }
        else if (winterparticle.activeSelf)
        {
            winterparticle.gameObject.SetActive(false);
        }

        if (PancilSketch.enabled == false)
        {
            PancilSketch.enabled = true;
            CameraFilter.StartPancilSketch = false;
            PancilSketch.NullMaterial = PancilSketch.PancilSkatch;
            if (SceneManager.GetActiveScene().name.Contains("ARMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:PancilSketch", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:PancilSketch", GetType().ToString());
            }
        }
        else if (PancilSketch.enabled == true)
        {
            PancilSketch.enabled = false;

            if (PancilSketch.NullMaterial == PancilSketch.PancilSkatch)
            {
                PancilSketch.enabled = false;
                PancilSketch.NullMaterial = PancilSketch.NullMaterial;
                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:PancilSketch", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:PancilSketch", GetType().ToString());
                }
            }
            else if (PancilSketch.NullMaterial != PancilSketch.PancilSkatch)
            {
                PancilSketch.enabled = true;
                CameraFilter.StartPancilSketch = false;
                PancilSketch.NullMaterial = PancilSketch.PancilSkatch;

                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:PancilSketch", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:PancilSketch", GetType().ToString());
                }
            }
        }
    }

    public void OnOffOilShader()
    {
        springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

        if (springparticle.activeSelf)
        {
            springparticle.gameObject.SetActive(false);
        }
        else if (fallparticle.activeSelf)
        {
            fallparticle.gameObject.SetActive(false);
        }
        else if (winterparticle.activeSelf)
        {
            winterparticle.gameObject.SetActive(false);
        }

        if (PancilSketch.enabled == false)
        {
            PancilSketch.enabled = true;
            CameraFilter.StartPancilSketch = false;
            PancilSketch.NullMaterial = PancilSketch.OilMaterial;
            if (SceneManager.GetActiveScene().name.Contains("ARMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:OilPaint", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:OilPaint", GetType().ToString());
            }
        }
        else if (PancilSketch.enabled == true)
        {
            PancilSketch.enabled = false;

            if (PancilSketch.NullMaterial == PancilSketch.OilMaterial)
            {
                PancilSketch.enabled = false;
                PancilSketch.NullMaterial = PancilSketch.NullMaterial;
                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:OilPaint", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("Clear"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:OilPaint", GetType().ToString());
                }
            }
            else if (PancilSketch.NullMaterial != PancilSketch.OilMaterial)
            {
                PancilSketch.enabled = true;
                CameraFilter.StartPancilSketch = false;
                PancilSketch.NullMaterial = PancilSketch.OilMaterial;
                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:OilPaint", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:OilPaint", GetType().ToString());
                }
            }
        }
    }
    public void OnOffSharpenShader()
    {
        springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

        if (springparticle.activeSelf)
        {
            springparticle.gameObject.SetActive(false);
        }
        else if (fallparticle.activeSelf)
        {
            fallparticle.gameObject.SetActive(false);
        }
        else if (winterparticle.activeSelf)
        {
            winterparticle.gameObject.SetActive(false);
        }

        if (PancilSketch.enabled == false)
        {
            PancilSketch.enabled = true;
            //PancilSketch.SharpValue.value = 10;
            //PancilSketch.SharpValue.gameObject.SetActive(true);
            CameraFilter.StartPancilSketch = false;
            PancilSketch.NullMaterial = PancilSketch.SharpenMaterial;
            if (SceneManager.GetActiveScene().name.Contains("ARMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:Sharpen", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:Sharpen", GetType().ToString());
            }
        }
        else if (PancilSketch.enabled == true)
        {
            PancilSketch.enabled = false;

            if (PancilSketch.NullMaterial == PancilSketch.SharpenMaterial)
            {
                //PancilSketch.SharpValue.gameObject.SetActive(false);
                PancilSketch.enabled = false;
                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:Sharpen", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:Sharpen", GetType().ToString());
                }
            }
            else if (PancilSketch.NullMaterial != PancilSketch.SharpenMaterial)
            {
                PancilSketch.enabled = true;
                //PancilSketch.SharpValue.value = 10;
                //PancilSketch.SharpValue.gameObject.SetActive(true);
                CameraFilter.StartPancilSketch = false;
                PancilSketch.NullMaterial = PancilSketch.SharpenMaterial;
                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:Sharpen", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:Sharpen", GetType().ToString());
                }
            }
        }
    }
    public void OnOffGaussianShader()
    {
        springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

        if (springparticle.activeSelf)
        {
            springparticle.gameObject.SetActive(false);
        }
        else if (fallparticle.activeSelf)
        {
            fallparticle.gameObject.SetActive(false);
        }
        else if (winterparticle.activeSelf)
        {
            winterparticle.gameObject.SetActive(false);
        }

        if (PancilSketch.enabled == false)
        {
            PancilSketch.enabled = true;
            //PancilSketch.BlurValue.gameObject.SetActive(true);
            //PancilSketch.SoftValue.gameObject.SetActive(true);
            //PancilSketch.SoftValue_2.gameObject.SetActive(true);
            CameraFilter.StartPancilSketch = false;
            PancilSketch.NullMaterial = PancilSketch.GaussianBlurMaterial;
            if (SceneManager.GetActiveScene().name.Contains("ARMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:SoftBlur", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:SoftBlur", GetType().ToString());
            }
        }
        else if (PancilSketch.enabled == true)
        {
            PancilSketch.enabled = false;

            if (PancilSketch.NullMaterial == PancilSketch.GaussianBlurMaterial)
            {
                //PancilSketch.BlurValue.gameObject.SetActive(false);
                //PancilSketch.SoftValue.gameObject.SetActive(false);
                //PancilSketch.SoftValue_2.gameObject.SetActive(false);
                PancilSketch.enabled = false;
                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:SoftBlur", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:SoftBlur", GetType().ToString());
                }
            }
            else if (PancilSketch.NullMaterial != PancilSketch.GaussianBlurMaterial)
            {
                PancilSketch.enabled = true;
                //PancilSketch.BlurValue.gameObject.SetActive(true);
                //PancilSketch.SoftValue.gameObject.SetActive(true);
                //PancilSketch.SoftValue_2.gameObject.SetActive(true);
                CameraFilter.StartPancilSketch = false;
                PancilSketch.NullMaterial = PancilSketch.GaussianBlurMaterial;
                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:SoftBlur", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:SoftBlur", GetType().ToString());
                }
            }
        }
    }

    public void OnOffSoftShader()
    {
        springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

        if (springparticle.activeSelf)
        {
            springparticle.gameObject.SetActive(false);
        }
        else if (fallparticle.activeSelf)
        {
            fallparticle.gameObject.SetActive(false);
        }
        else if (winterparticle.activeSelf)
        {
            winterparticle.gameObject.SetActive(false);
        }
        Invoke("WaitClose",0.3f);
    }

    public void WaitClose() {
        if (PancilSketch.enabled == false)
        {
            PancilSketch.enabled = true;
            //PancilSketch.SoftValue.gameObject.SetActive(true);
            //PancilSketch.SoftValue_2.gameObject.SetActive(true);
            CameraFilter.StartPancilSketch = false;
            PancilSketch.NullMaterial = PancilSketch.SoftLightMaterial;
            if (SceneManager.GetActiveScene().name.Contains("ARMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:SoftLight", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:SoftLight", GetType().ToString());
            }
        }
        else if (PancilSketch.enabled == true)
        {
            PancilSketch.enabled = false;

            if (PancilSketch.NullMaterial == PancilSketch.SoftLightMaterial)
            {
                //PancilSketch.SoftValue.gameObject.SetActive(false);
                //PancilSketch.SoftValue_2.gameObject.SetActive(false);
                PancilSketch.enabled = false;
                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:SoftLight", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:SoftLight", GetType().ToString());
                }
            }
            else if (PancilSketch.NullMaterial != PancilSketch.SoftLightMaterial)
            {
                PancilSketch.enabled = true;
                //PancilSketch.SoftValue.gameObject.SetActive(true);
                //PancilSketch.SoftValue_2.gameObject.SetActive(true);
                CameraFilter.StartPancilSketch = false;
                PancilSketch.NullMaterial = PancilSketch.SoftLightMaterial;
                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:SoftLight", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:SoftLight", GetType().ToString());
                }
            }
        }
    }

    public void OnOffGaussiansoftShader()
    {
        springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
        winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

        if (springparticle.activeSelf)
        {
            springparticle.gameObject.SetActive(false);
        }
        else if (fallparticle.activeSelf)
        {
            fallparticle.gameObject.SetActive(false);
        }
        else if (winterparticle.activeSelf)
        {
            winterparticle.gameObject.SetActive(false);
        }

        if (PancilSketch.enabled == false)
        {
            PancilSketch.enabled = true;
            CameraFilter.StartPancilSketch = true;
            PancilSketch.NullMaterial = PancilSketch.GaussianBlurMaterial;
        }
        else if (PancilSketch.enabled == true)
        {
            PancilSketch.enabled = false;
        }
    }

    public void OnOffSpringMaterial()
    {
        if (PancilSketch.enabled == false)
        {
            PancilSketch.enabled = true;
            CameraFilter.StartPancilSketch = false;
            PancilSketch.NullMaterial = PancilSketch.SpringMaterial;

            springparticle.gameObject.SetActive(true);
            fallparticle.gameObject.SetActive(false);
            winterparticle.gameObject.SetActive(false);
            springparticle.gameObject.GetComponent<ParticleSystem>().Play();
            fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
            winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

            if (SceneManager.GetActiveScene().name.Contains("ARMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:Spring", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:Spring", GetType().ToString());
            }
        }
        else if (PancilSketch.enabled == true)
        {
            if (PancilSketch.NullMaterial == PancilSketch.SpringMaterial)
            {
                PancilSketch.enabled = false;

                springparticle.gameObject.SetActive(false);
                fallparticle.gameObject.SetActive(false);
                winterparticle.gameObject.SetActive(false);
                springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:Spring", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:Spring", GetType().ToString());
                }
            }
            else if (PancilSketch.NullMaterial != PancilSketch.SpringMaterial)
            {
                PancilSketch.enabled = true;
                CameraFilter.StartPancilSketch = false;
                PancilSketch.NullMaterial = PancilSketch.SpringMaterial;

                springparticle.gameObject.SetActive(true);
                fallparticle.gameObject.SetActive(false);
                winterparticle.gameObject.SetActive(false);
                springparticle.gameObject.GetComponent<ParticleSystem>().Play();
                fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:Spring", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:Spring", GetType().ToString());

                    springparticle.gameObject.SetActive(true);
                    fallparticle.gameObject.SetActive(false);
                    winterparticle.gameObject.SetActive(false);
                    springparticle.gameObject.GetComponent<ParticleSystem>().Play();
                    fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                    winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                }
            }
        }
    }

    public void OnOffSummerMaterial()
    {
        if (PancilSketch.enabled == false)
        {
            PancilSketch.enabled = true;
            CameraFilter.StartPancilSketch = false;
            PancilSketch.NullMaterial = PancilSketch.SummerMaterial;
            if (SceneManager.GetActiveScene().name.Contains("ARMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:Summer", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:Summer", GetType().ToString());
            }
        }
        else if (PancilSketch.enabled == true)
        {
            if (PancilSketch.NullMaterial == PancilSketch.SummerMaterial)
            {
                PancilSketch.enabled = false;
                
                springparticle.gameObject.SetActive(false);
                fallparticle.gameObject.SetActive(false);
                winterparticle.gameObject.SetActive(false);
                springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:Summer", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:Summer", GetType().ToString());
                }
            }
            else if (PancilSketch.NullMaterial != PancilSketch.SummerMaterial)
            {
                PancilSketch.enabled = true;
                CameraFilter.StartPancilSketch = false;
                PancilSketch.NullMaterial = PancilSketch.SummerMaterial;

                springparticle.gameObject.SetActive(false);
                fallparticle.gameObject.SetActive(false);
                winterparticle.gameObject.SetActive(false);
                springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:Summer", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:Summer", GetType().ToString());
                }
            }
        }
    }

    public void OnOffFallMaterial()
    {
        if (PancilSketch.enabled == false)
        {
            PancilSketch.enabled = true;
            CameraFilter.StartPancilSketch = false;
            PancilSketch.NullMaterial = PancilSketch.FallMaterial;

            springparticle.gameObject.SetActive(false);
            fallparticle.gameObject.SetActive(true);
            winterparticle.gameObject.SetActive(false);
            springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
            fallparticle.gameObject.GetComponent<ParticleSystem>().Play();
            winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

            if (SceneManager.GetActiveScene().name.Contains("ARMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:Fall", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:Fall", GetType().ToString());
            }
        }
        else if (PancilSketch.enabled == true)
        {
            if (PancilSketch.NullMaterial == PancilSketch.FallMaterial)
            {
                PancilSketch.enabled = false;

                springparticle.gameObject.SetActive(false);
                fallparticle.gameObject.SetActive(false);
                winterparticle.gameObject.SetActive(false);
                springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:Fall", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:Fall", GetType().ToString());
                }
            }
            else if (PancilSketch.NullMaterial != PancilSketch.FallMaterial)
            {
                PancilSketch.enabled = true;
                CameraFilter.StartPancilSketch = false;
                PancilSketch.NullMaterial = PancilSketch.FallMaterial;

                springparticle.gameObject.SetActive(false);
                fallparticle.gameObject.SetActive(true);
                winterparticle.gameObject.SetActive(false);
                springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                fallparticle.gameObject.GetComponent<ParticleSystem>().Play();
                winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:Fall", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:Fall", GetType().ToString());
                }
            }
        }
    }

    public void OnOffWinterMaterial()
    {
        if (PancilSketch.enabled == false)
        {
            PancilSketch.enabled = true;
            CameraFilter.StartPancilSketch = false;
            PancilSketch.NullMaterial = PancilSketch.WinterMaterial;

            springparticle.gameObject.SetActive(false);
            fallparticle.gameObject.SetActive(false);
            winterparticle.gameObject.SetActive(true);
            springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
            fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
            winterparticle.gameObject.GetComponent<ParticleSystem>().Play();

            if (SceneManager.GetActiveScene().name.Contains("ARMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:Winter", GetType().ToString());
            }
            else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
            {
                gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:Winter", GetType().ToString());
            }
        }
        else if (PancilSketch.enabled == true)
        {
            if (PancilSketch.NullMaterial == PancilSketch.WinterMaterial)
            {
                PancilSketch.enabled = false;
                
                springparticle.gameObject.SetActive(false);
                fallparticle.gameObject.SetActive(false);
                winterparticle.gameObject.SetActive(false);
                springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:Winter", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:Winter", GetType().ToString());
                }
            }
            else if (PancilSketch.NullMaterial != PancilSketch.WinterMaterial)
            {
                PancilSketch.enabled = true;
                CameraFilter.StartPancilSketch = false;
                PancilSketch.NullMaterial = PancilSketch.WinterMaterial;

                springparticle.gameObject.SetActive(false);
                fallparticle.gameObject.SetActive(false);
                winterparticle.gameObject.SetActive(true);
                springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                winterparticle.gameObject.GetComponent<ParticleSystem>().Play();

                if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterStart:Winter", GetType().ToString());
                }
                else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                {
                    gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterStart:Winter", GetType().ToString());
                }
            }
        }
    }

    public void FinishFilterEffect()
    {
        if(FunctionCustom.functionorigin.filterfunction.FilterBar.activeSelf)
        {
            if (PancilSketch.enabled == true)
            {
                PancilSketch.enabled = false;

                springparticle.gameObject.SetActive(false);
                fallparticle.gameObject.SetActive(false);
                winterparticle.gameObject.SetActive(false);
                springparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                fallparticle.gameObject.GetComponent<ParticleSystem>().Stop();
                winterparticle.gameObject.GetComponent<ParticleSystem>().Stop();

                if (PancilSketch.NullMaterial == PancilSketch.PancilSkatch)
                {
                    if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:PancilSketch", GetType().ToString());
                    }
                    else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:PancilSketch", GetType().ToString());
                    }

                }
                else if (PancilSketch.NullMaterial == PancilSketch.OilMaterial)
                {
                    if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:OilPaint", GetType().ToString());
                    }
                    else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:OilPaint", GetType().ToString());
                    }
                }
                else if (PancilSketch.NullMaterial == PancilSketch.SharpenMaterial)
                {
                    if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:Sharpen", GetType().ToString());
                    }
                    else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:Sharpen", GetType().ToString());
                    }
                }
                else if (PancilSketch.NullMaterial == PancilSketch.GaussianBlurMaterial)
                {
                    if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:SoftBlur", GetType().ToString());
                    }
                    else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:SoftBlur", GetType().ToString());
                    }
                }
                else if (PancilSketch.NullMaterial == PancilSketch.SoftLightMaterial)
                {
                    if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:SoftLight", GetType().ToString());
                    }
                    else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:SoftLight", GetType().ToString());
                    }
                }
                else if (PancilSketch.NullMaterial == PancilSketch.SpringMaterial)
                {
                    if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:SoftLight", GetType().ToString());
                    }
                    else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:SoftLight", GetType().ToString());
                    }
                }
                else if (PancilSketch.NullMaterial == PancilSketch.SummerMaterial)
                {
                    if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:SoftLight", GetType().ToString());
                    }
                    else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:SoftLight", GetType().ToString());
                    }
                }
                else if (PancilSketch.NullMaterial == PancilSketch.FallMaterial)
                {
                    if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:SoftLight", GetType().ToString());
                    }
                    else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:SoftLight", GetType().ToString());
                    }
                }
                else if (PancilSketch.NullMaterial == PancilSketch.WinterMaterial)
                {
                    if (SceneManager.GetActiveScene().name.Contains("ARMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_Filter, "AR_FilterFinish:SoftLight", GetType().ToString());
                    }
                    else if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
                    {
                        gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_Filter, "Clear_FilterFinish:SoftLight", GetType().ToString());
                    }
                }

                PancilSketch.NullMaterial = PancilSketch.NullMaterial;
            }
        }
    }

    public void stoprender()
    {
        PancilSketch.enabled = false;
    }

}
