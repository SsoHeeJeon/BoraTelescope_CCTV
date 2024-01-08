using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Category : MonoBehaviour
{
    public enum CurrentCategory
    {
        Total, Eco, Building
    }
    public static CurrentCategory curcate;

    public GameManager gamemanager;
    public GameObject CategoryContent;

    public Sprite[] cateImage = new Sprite[12];
    public Sprite[] SelectcateImage = new Sprite[12];

    /// <summary>
    /// ī�װ� ��ư ���ÿ� ���� ������Ʈ ���� �� �α� ����
    /// ������ ī�װ��� ���� �� Ȱ��ȭ/��Ȱ��ȭ ����
    /// </summary>
    /// <param name="cate"></param>
    public void SelectCategory(GameObject cate)
    {
        if (SceneManager.GetActiveScene().name.Contains("ClearMode"))
        {
            gamemanager.WriteLog(LogSendServer.NormalLogCode.Clear_CategorySelect, "Clear_CategorySelect:" + cate.name, GetType().ToString());
        }
        else if (SceneManager.GetActiveScene().name.Contains("XRMode"))
        {
            gamemanager.WriteLog(LogSendServer.NormalLogCode.AR_CategorySelect, "XR_CategorySelect:" + cate.name, GetType().ToString());
        }

        gamemanager.label.SelectCategortButton(cate);
    }

    public void seedebug()
    {
        gamemanager.labelmake.ReadytoStart();
        gamemanager.labelmake.NavigationText();
        gamemanager.labelmake.MapLabel();
    }

    /// <summary>
    /// �� ī�װ� ��ư ������, ��ġ, ��ũ��Content ũ�� �����ϱ�
    /// </summary>
    public void SetCategory()
    {
        // ī�װ��� �̹��� ������ ����
        CategoryContent.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();
        CategoryContent.transform.GetChild(1).gameObject.GetComponent<Image>().SetNativeSize();
        CategoryContent.transform.GetChild(2).gameObject.GetComponent<Image>().SetNativeSize();

        // ī�װ��� ��ġ ����
        float firstcate_x = 5;
        float secondcate_x = firstcate_x + CategoryContent.transform.GetChild(0).gameObject.GetComponent<RectTransform>().rect.width + 10;
        float thirdcate_x = secondcate_x + CategoryContent.transform.GetChild(1).gameObject.GetComponent<RectTransform>().rect.width + 10;
        float total_Width = CategoryContent.transform.GetChild(0).gameObject.GetComponent<RectTransform>().rect.width + CategoryContent.transform.GetChild(1).gameObject.GetComponent<RectTransform>().rect.width
                            + CategoryContent.transform.GetChild(2).gameObject.GetComponent<RectTransform>().rect.width + 30;

        CategoryContent.transform.GetChild(0).localPosition = new Vector3(firstcate_x, CategoryContent.transform.GetChild(0).localPosition.y, 0);
        CategoryContent.transform.GetChild(1).localPosition = new Vector3(secondcate_x, CategoryContent.transform.GetChild(0).localPosition.y, 0);
        CategoryContent.transform.GetChild(2).localPosition = new Vector3(thirdcate_x, CategoryContent.transform.GetChild(0).localPosition.y, 0);
        CategoryContent.GetComponent<RectTransform>().sizeDelta = new Vector2(total_Width, CategoryContent.GetComponent<RectTransform>().sizeDelta.y);  // ī�װ� ��ũ�Ѻ� ũ�� ����

        //seedebug();
        Invoke("seedebug", 0.01f);
    }

    public void CategorySelect_korea()
    {
        switch (curcate)
        {
            case CurrentCategory.Total:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = SelectcateImage[0];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = cateImage[1];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = cateImage[2];
                break;
            case CurrentCategory.Eco:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = cateImage[0];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = SelectcateImage[1];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = cateImage[2];
                break;
            case CurrentCategory.Building:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = cateImage[0];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = cateImage[1];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = SelectcateImage[2];
                break;
        }
    }

    public void CategorySelect_English()
    {
        switch (curcate)
        {
            case CurrentCategory.Total:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = SelectcateImage[3];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = cateImage[4];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = cateImage[5];
                break;
            case CurrentCategory.Eco:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = cateImage[3];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = SelectcateImage[4];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = cateImage[5];
                break;
            case CurrentCategory.Building:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = cateImage[3];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = cateImage[4];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = SelectcateImage[5];
                break;
        }
    }

    public void CategorySelect_Chinese()
    {
        switch (curcate)
        {
            case CurrentCategory.Total:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = SelectcateImage[6];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = cateImage[7];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = cateImage[8];
                break;
            case CurrentCategory.Eco:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = cateImage[6];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = SelectcateImage[7];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = cateImage[8];
                break;
            case CurrentCategory.Building:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = cateImage[6];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = cateImage[7];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = SelectcateImage[8];
                break;
        }
    }

    public void CategorySelect_Japanese()
    {
        switch (curcate)
        {
            case CurrentCategory.Total:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = SelectcateImage[9];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = cateImage[10];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = cateImage[11];
                break;
            case CurrentCategory.Eco:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = cateImage[9];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = SelectcateImage[10];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = cateImage[11];
                break;
            case CurrentCategory.Building:
                gamemanager.CategoryContent.transform.GetChild(0).GetComponent<Image>().sprite = cateImage[9];
                gamemanager.CategoryContent.transform.GetChild(1).GetComponent<Image>().sprite = cateImage[10];
                gamemanager.CategoryContent.transform.GetChild(2).GetComponent<Image>().sprite = SelectcateImage[11];
                break;
        }
    }
}
