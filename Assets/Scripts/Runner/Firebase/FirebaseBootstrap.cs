using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions; // ContinueWithOnMainThread
using UnityEngine;

public class FirebaseBootstrap : MonoBehaviour
{
    public static FirebaseBootstrap Instance { get; private set; }

    public FirebaseAuth Auth { get; private set; }
    public bool IsReady { get; private set; }

    public event Action OnReady;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _ = InitAsync();
    }

    public async Task InitAsync()
    {
        if (IsReady) return;

        var dep = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dep != DependencyStatus.Available)
        {
            Debug.LogError($"Firebase dependencies not available: {dep}");
            return;
        }

        Auth = FirebaseAuth.DefaultInstance;

        // 익명 로그인
        if (Auth.CurrentUser == null)
        {
            try
            {
                await Auth.SignInAnonymouslyAsync();
            }
            catch (Exception e)
            {
                Debug.LogError($"Anonymous sign-in failed: {e}");
                return;
            }
        }

        IsReady = true;
        OnReady?.Invoke();
    }
}
