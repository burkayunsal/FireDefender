using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SBF.Extentions.Vector;
using TMPro;
using UnityEngine;

public class UpgradeOpen : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image upgradeOpenImg;
    [SerializeField] private int upgradeAreaOpenCost;
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private GameObject upgradeArea;
    
    private Vector3 playerPos;
    private bool stopCoroutine = false;
    public void OnPlayerEnter(Vector3 pos)
    {
        playerPos = pos;
        upgradeOpenImg.DOFillAmount(1, Configs.UI.PopUpTimer).SetEase(Ease.Linear).OnComplete(() =>
        {
            stopCoroutine = false;
            StartCoroutine(CoinDrop());
        });
    }

    private void Start()
    {
        if (SaveLoadManager.IsOpen() > 1)
        {
            OpenUpgradeArea();
        } 
        else if (SaveLoadManager.GetCost() == 0 && !upgradeArea.activeSelf)
        {
            SaveLoadManager.SetCost(upgradeAreaOpenCost);
        }
        txt.text = SaveLoadManager.GetCost().ToString("000");
    }

    public void OnPlayerExit()
    {
        stopCoroutine = true;
        upgradeOpenImg.DOKill();
        upgradeOpenImg.DOFillAmount(0, 0.5f).SetEase(Ease.Linear);
    }
    IEnumerator CoinDrop()
    {
        for (int i = 0; i < SaveLoadManager.GetCoin(); i++) 
        {
            if (SaveLoadManager.GetCoin() < 10 || stopCoroutine) yield break;
            SaveLoadManager.AddCoin(-10);
            SaveLoadManager.SetCost(-10);
            SoundManager.I.PlaySound(SoundName.Cash);
            UpdateCoinTxt();

            Coin coin = PoolManager.I.GetObject<Coin>();
            coin.transform.position = playerPos;
            coin.CoinMovementToUpgradeOpen(transform.position.WithY(1));

            if (SaveLoadManager.GetCost() < 10)
            {
                OpenUpgradeArea();
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    void OpenUpgradeArea()
    {
        SaveLoadManager.IsOpen(true);
        upgradeArea.gameObject.SetActive(true);
        upgradeArea.transform.localScale = Vector3.zero;
        SoundManager.I.PlaySound(SoundName.UpgradeOpen);
        upgradeArea.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
        gameObject.transform.DOScale(Vector3.zero, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    void UpdateCoinTxt()
    {
        txt.text = SaveLoadManager.GetCost().ToString("000");
    }
}
