
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
            var cost = Configs.Player.vagoonCosts[SaveLoadManager.GetCarrierLevel()];
            if (SaveLoadManager.GetCoin() >= cost)
            {
                SaveLoadManager.AddCoin(-cost);
                SaveLoadManager.UpgradeCarrier();
                
                PlayerController.I.playerCapacityisFull(false);
                WriteCapacityText();
                SoundManager.I.PlaySound(SoundName.Cash);
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
            txtCost[0].SetText(Configs.Player.vagoonCosts[SaveLoadManager.GetCarrierLevel()].ToString());
            txtLevel[0].SetText("Level " + (SaveLoadManager.GetCarrierLevel() + 1));
        }
    }

    public void UpgradeSpeed()
    {
        if (SaveLoadManager.GetSpeedLevel() < Configs.Player.speed.Length)
        {
            var cost = Configs.Player.speedCosts[SaveLoadManager.GetSpeedLevel()];
            if (SaveLoadManager.GetCoin() >= cost)
            {
                SaveLoadManager.AddCoin(-cost);
                SaveLoadManager.IncreaseSpeedLevel();
                PlayerController.I.SetSpeedUpgrades();
                ParticleManager.I.speedUpParticle.Play();
                SoundManager.I.PlaySound(SoundName.Cash);

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
            txtCost[1].SetText(Configs.Player.speedCosts[SaveLoadManager.GetSpeedLevel()].ToString());
            txtLevel[1].SetText("Level " + (SaveLoadManager.GetSpeedLevel() + 1));
        }
    }
    
    public void UpgradeHarvestSize()
    {
        if (SaveLoadManager.GetSize() < Configs.Player.size.Length)
        {
            var cost = Configs.Player.sizeCosts[SaveLoadManager.GetSize()];
            if (SaveLoadManager.GetCoin() >= cost)
            {
                SaveLoadManager.AddCoin(-cost);
                SaveLoadManager.IncreaseSize();
                PlayerController.I.SetSize();
                
                ParticleManager.I.sizeUpgradeParticleSystem.Play();
                SoundManager.I.PlaySound(SoundName.Cash);

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
            txtCost[2].SetText( Configs.Player.sizeCosts[SaveLoadManager.GetSize()].ToString());
            txtLevel[2].SetText("Level " + (SaveLoadManager.GetSize() + 1));
        }
    }
}
    
