using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusItem : MonoBehaviour
{
    //¿À´Ã ¹ÞÀ»³ðµé
    GameObject Foucus;
    Image FoucusTop;

    //ÀÌ¹Ì ¹ÞÀº³ðµé
    GameObject DividerLine;
    GameObject Check;

    //ÇÊ¼ö 
    Image image_Icon;
    Image image_BG;
    TextMeshPro text_Count;
    TextMeshPro text_Day;

    public void Start()
    {
        Foucus = GameObject.Find("Focus");
        FoucusTop = GameObject.Find("Bg/FocusTop").GetComponent<Image>();

        DividerLine = GameObject.Find("Bg/DividerLine");
        Check = GameObject.Find("Icon_Check");

        image_BG = GameObject.Find("Bg").GetComponent<Image>();

        image_Icon = GameObject.Find("ItemIcon").GetComponent<Image>();
        text_Count = GameObject.Find("Text_Num").GetComponent<TextMeshPro>();
        text_Day = GameObject.Find("Text_Day").GetComponent<TextMeshPro>();
    }

    public void SetToday()
    {
        text_Day.color = Color.green;
        image_BG.color = Color.green;
        FoucusTop.color = Color.white;

        Foucus.SetActive(true);
        FoucusTop.gameObject.SetActive(true);

        DividerLine.SetActive(false);
        Check.SetActive(false);
    }

    public void SetGet()
    {
        text_Day.color = Color.gray;
        Foucus.SetActive(false);
        FoucusTop.gameObject.SetActive(false);

        DividerLine.SetActive(true);
        Check.SetActive(true);
    }
    public void SetNext()
    {
        text_Day.color = Color.white;
        image_BG.color = GetHexColor("0787FF");
        FoucusTop.color = GetHexColor("0787FF");
        Foucus.SetActive(false);
        FoucusTop.gameObject.SetActive(false);

        DividerLine.SetActive(true);
        Check.SetActive(false);
    }

    public UnityEngine.Color GetHexColor(string hex)
    {
        UnityEngine.Color color;
        ColorUtility.TryParseHtmlString(hex, out color);

        return color;
    }
}   
