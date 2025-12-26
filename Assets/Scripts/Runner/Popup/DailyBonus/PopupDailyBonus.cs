using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupDailyBonus : MonoBehaviour
{
    Button Dimed;
    Button Button_Close;

    private PlayerCloudService _service;

    private async void Start()
    {
        // Firebase 준비 대기
        if (FirebaseBootstrap.Instance == null)
        {
            Debug.LogError("FirebaseBootstrap is missing in scene.");
            return;
        }

        if (!FirebaseBootstrap.Instance.IsReady)
            await FirebaseBootstrap.Instance.InitAsync();

        _service = new PlayerCloudService(FirebaseBootstrap.Instance.Auth);

        var data = await _service.GetOrCreateAsync();
        Debug.Log($"Loaded: currency={data.currency}, lastMissionAtUtc={data.lastMissionAtUtc.ToDateTime():u}");

        Dimed = transform.Find("Dimed").GetComponent<Button>();

        Button_Close = transform.Find("Button_Close02").GetComponent<Button>();

        Dimed.onClick.AddListener(ClosePopup);

        Button_Close.onClick.AddListener(ClosePopup);

        OnClickClaimDaily();
    }

    public async void OnClickClaimDaily()
    {
        if (_service == null) return;

        int reward = 1;
        TimeSpan cooldown = TimeSpan.FromHours(24);

        var result = await _service.TryClaimDailyKstAsync(reward);

        if (result.ok)
        {
            Debug.Log($"✅ Daily claimed! currencyAfter={result.currencyAfter}");
        }
        else
        {
            Debug.Log($"⏳ Cooldown. remain={FormatRemain(result.remain)}");
        }
    }

    private string FormatRemain(TimeSpan t)
    {
        if (t < TimeSpan.Zero) t = TimeSpan.Zero;
        return $"{(int)t.TotalHours:00}:{t.Minutes:00}:{t.Seconds:00}";
    }
    void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
