
    using System;
using TMPro;
using UnityEngine;
using DG.Tweening;


public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] TextMeshProUGUI[] txtCost;
    [SerializeField] TextMeshProUGUI[] txtLevel;
    [SerializeField] TextMeshProUGUI[] txtUpgrade;
    [SerializeField] private GameObject[] coinImages;

    [SerializeField] private ParticleSystem speedUpParticle;
    
    private void Start()
    {
        InitializeTexts();
    }

    private void InitializeTexts()
    {
        WriteCapacityText();
        WriteSpeedText();
        WriteSizeText();
    }

    public void UpgradeCapactiy()
    {
        {
            var cost = Configs.Player.upgradeCosts[SaveLoadManager.GetCarrierLevel()];
            if (SaveLoadManager.GetCoin() >= cost)
            {
                SaveLoadManager.AddCoin(-cost);
                SaveLoadManager.UpgradeCarrier();
                
                WriteCapacityText();
            } 
        } 
    }

    void WriteCapacityText()
    {
        if (SaveLoadManager.GetCarrierLevel() == Configs.Player.capacity.Length - 1)
        {
            txtLevel[0].SetText("");
            txtCost[0].SetText("");
            txtUpgrade[0].SetText(" FULL ");
            coinImages[0].SetActive(false);
            UIManager.I.btn.capacity.interactable = false;
        }
        else
        {
            txtCost[0].SetText(Configs.Player.upgradeCosts[SaveLoadManager.GetCarrierLevel()].ToString());
            txtLevel[0].SetText("Level " + (SaveLoadManager.GetCarrierLevel() + 1));
        }
    }

    public void UpgradeSpeed()
    {
        if (SaveLoadManager.GetSpeedLevel() < Configs.Player.speed.Length)
        {
            var cost = Configs.Player.upgradeCosts[SaveLoadManager.GetSpeedLevel()];
            if (SaveLoadManager.GetCoin() >= cost)
            {
                SaveLoadManager.AddCoin(-cost);
                SaveLoadManager.IncreaseSpeedLevel();
                
                speedUpParticle.Play();
                
                WriteSpeedText();
            } 
        }
    }
    
    void WriteSpeedText()
    {
        if (SaveLoadManager.GetSpeedLevel() == Configs.Player.speed.Length - 1)
        {
            txtLevel[1].SetText("");
            txtCost[1].SetText("");
            txtUpgrade[1].SetText(" FULL ");
            coinImages[1].SetActive(false);
            UIManager.I.btn.speed.interactable = false;
        }
        else
        {
            txtCost[1].SetText(Configs.Player.upgradeCosts[SaveLoadManager.GetSpeedLevel()].ToString());
            txtLevel[1].SetText("Level " + (SaveLoadManager.GetSpeedLevel() + 1));
        }
    }
    
    public void UpgradeHarvestSize()
    {
        if (SaveLoadManager.GetSize() < Configs.Player.size.Length)
        {
            var cost = Configs.Player.upgradeCosts[SaveLoadManager.GetSize()];
            if (SaveLoadManager.GetCoin() >= cost)
            {
                SaveLoadManager.AddCoin(-cost);
                SaveLoadManager.IncreaseSize();
                PlayerController.I.SetSize();

                WriteSizeText();
            } 
        }
    }
    
 
    void WriteSizeText()
    {
        if (SaveLoadManager.GetSize() == Configs.Player.size.Length - 1)
        {
            txtLevel[2].SetText("");
            txtCost[2].SetText("");
            txtUpgrade[2].SetText(" FULL ");
            coinImages[2].SetActive(false);
            UIManager.I.btn.size.interactable = false;
        }
        else
        {
            txtCost[2].SetText( Configs.Player.upgradeCosts[SaveLoadManager.GetSize()].ToString());
            txtLevel[2].SetText("Level " + (SaveLoadManager.GetSize() + 1));
        }
    }
}
    
