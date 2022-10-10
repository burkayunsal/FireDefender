﻿using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    #region LEVEL

    const string KEY_LEVEL = "levels";

    public static void IncreaseLevel() => PlayerPrefs.SetInt(KEY_LEVEL, GetLevel() + 1);
    public static int GetLevel() => PlayerPrefs.GetInt(KEY_LEVEL, 0);

    #endregion

    #region COIN

    const string KEY_COIN = "coins";

    public static void AddCoin(int add)
    {
        PlayerPrefs.SetInt(KEY_COIN, GetCoin() + add);
        UIManager.I.UpdateCoinTxt();
    }

    public static int GetCoin() => PlayerPrefs.GetInt(KEY_COIN, 0);

    #endregion

    #region COIN

    const string KEY_VIBRATION = "vibrator";

    public static bool HasVibration() => PlayerPrefs.GetInt(KEY_VIBRATION, 1) == 1;

    public static void ChangeVibrationStatus() { if (HasVibration()) SetVibrationStatus(false); else SetVibrationStatus(true); }

    public static void SetVibrationStatus(bool isEnabled) { PlayerPrefs.SetInt(KEY_VIBRATION, isEnabled ? 1 : 0); UIManager.I.UpdateHapticStatus(); }

    #endregion

    #region PRIZES

    const string KEY_PRIZES = "priozes_";

    public static bool HasPrizeTaken(int id) => PlayerPrefs.GetInt(KEY_PRIZES + id, 0) == 1;

    public static void SetPrizeTaken(int id) => PlayerPrefs.SetInt(KEY_PRIZES + id, 1);

    #endregion

    #region CARRIER UPGRADE

    private const string KEY_CARRIER = "asfgasdas";

    public static void UpgradeCarrier()
    {
        PlayerPrefs.SetInt(KEY_CARRIER, GetCarrierLevel() + 1);
        StackManager.I.InitCarriers();
    }

    public static int GetCarrierLevel()
    {
        return PlayerPrefs.GetInt(KEY_CARRIER, 0);
    }

    #endregion

    #region SPEED

    const string KEY_SPEED_LEVEL = "speed";

    public static void IncreaseSpeedLevel() 
    {
        PlayerPrefs.SetInt(KEY_SPEED_LEVEL, GetSpeedLevel() + 1);
        PlayerController.I.Speed =  Configs.Player.speed[GetSpeedLevel()];
    }
    public static int GetSpeedLevel() => PlayerPrefs.GetInt(KEY_SPEED_LEVEL, 0);

    #endregion
    
    
    #region SIZE

    const string KEY_MAX_SIZE = "size";

    public static void IncreaseSize() => PlayerPrefs.SetInt(KEY_MAX_SIZE, GetSize() + 1);
    public static int GetSize() => PlayerPrefs.GetInt(KEY_MAX_SIZE, 0);

    #endregion
}
