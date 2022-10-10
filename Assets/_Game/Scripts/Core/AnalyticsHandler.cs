//#define Facebook
//#define GameAnalytics
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if Facebook
using Facebook.Unity;
#endif
#if GameAnalytics
using GameAnalyticsSDK;
#endif

public class AnalyticsHandler : MonoBehaviour
{
    public static AnalyticsHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            Init();
        }
        else
            Destroy(gameObject);
    }

    void Init()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

    #if GameAnalytics
        GameAnalytics.Initialize();
        GameAnalytics.StartSession();
    #endif

    #if Facebook

        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    #endif

    }

    private void InitCallback()
    {
        #if Facebook
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
        #endif
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public static void SendEvent(string s)
    {
        SendEvent(s, s);
    }

    public static void SendEvent(string key, string label)
    {
        #if Facebook
        if (!FB.IsInitialized) return;

        var tutParams = new Dictionary<string, object>();
        tutParams[key] = key;
        tutParams[label] = label;

        FB.LogAppEvent(
            key,
            parameters: tutParams
        );
        #endif
    }

    void SendEventForOnce(string key, string label)
    {
    }

    private void OnApplicationQuit()
    {
        #if GameAnalytics
        GameAnalytics.EndSession();
        #endif

    }
}
