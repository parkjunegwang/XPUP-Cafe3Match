using UnityEngine;
using UnityEngine.UI;
public class PopupMission : MonoBehaviour
{
    Button Button_Close;
    void Start()
    {

        Button_Close = transform.Find("Button_Close02").GetComponent<Button>();


        Button_Close.onClick.AddListener(ClosePopup);
    }

    void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
