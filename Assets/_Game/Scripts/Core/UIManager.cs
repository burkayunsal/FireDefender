using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using SBF.Extentions.Colors;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] Panels pnl;
    [Header("Images")]
    [SerializeField] Images img;
    [Header("Buttons")]
    [SerializeField] public Buttons btn;
    [Header("Texts")]
    [SerializeField] Texts txt;

    private CanvasGroup activePanel = null;

    public Panels GetPanel() => pnl;
    public Buttons GetButtons() => btn;

    private void Start()
    {
        UpdateTexts();
        UpdateHapticStatus();
    }

    public void Initialize(bool isButtonDerived)
    {
        btn.play.gameObject.SetActive(!isButtonDerived);
        img.taptoStart.gameObject.SetActive(isButtonDerived);
        FadeInAndOutPanels(pnl.mainMenu);
    }

    public void StartGame()
    {
        GameManager.OnStartGame();
    }

    public void OnGameStarted()
    {
        FadeInAndOutPanels(pnl.gameIn);
        UpdateCoinTxt();
    }

    public void OnFail()
    {
        FadeInAndOutPanels(pnl.fail);
    }

    public void SkipLevel()
    {
        GameManager.OnLevelCompleted();
        StackManager.I.OnLevelSucceed();
    }

    public void OnSuccess(bool hasPrize = true)
    {
        
        if(hasPrize)
        {
            btn.nextLevel.gameObject.SetActive(false);
            PrizeHandler.I.ShowPrizeProcess();
        }
        else
        {
            btn.nextLevel.gameObject.SetActive(true);
            FadeInAndOutPanels(pnl.success);
            txt.amazing.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
        }
        
    }

    public void OnPrizeShow(float percentage, PrizeCanvas prizeCanvas)
    {
        btn.nextLevel.gameObject.SetActive(false);
        btn.getPrize.gameObject.SetActive(false);

        FadeInAndOutPanels(pnl.success);

        new SBF.Toolkit.DelayedAction(() => {

            AnimatePrize(percentage, prizeCanvas).OnComplete(() =>
            {
                if (percentage >= 1f)
                {
                    btn.getPrize.gameObject.SetActive(true);
                    prizeCanvas.img_sunShine.gameObject.SetActive(true);
                    prizeCanvas.img_prizeBg.enabled = false;

                    prizeCanvas.img_prizeVisible.transform.DOScale(1.1f, .2f).OnComplete(() =>
                    {
                        prizeCanvas.img_prizeVisible.transform.DOScale(.95f, .1f).OnComplete(() =>
                        {
                            prizeCanvas.img_prizeVisible.transform.DOScale(1.05f, .1f).OnComplete(() =>
                            {
                                prizeCanvas.img_prizeVisible.transform.DOScale(1f, .3f);
                            });
                        });
                    });


                    new SBF.Toolkit.DelayedAction(() => btn.nextLevel.gameObject.SetActive(true), 3f).Execute(this);
                }
                else
                {
                    btn.nextLevel.gameObject.SetActive(true);
                }
            });

        }, Configs.UI.FadeOutTime * 2f).Execute(this);
    }

    Tween AnimatePrize(float percentage, PrizeCanvas prizeCanvas)
    {
        float fillAmount = 0f;
        return DOTween.To(() => fillAmount, x => fillAmount = x, percentage, percentage * 2f).SetEase(Ease.Linear).OnUpdate(() =>
        {
            prizeCanvas.txt_percentage.text = (fillAmount * 100f).ToString("00").Insert(0, "%");
            prizeCanvas.img_prizeVisible.fillAmount = fillAmount;
        });
    }

    public void ReloadScene(bool isSuccess)
    {
        GameManager.ReloadScene(isSuccess);
    }

    private Tween activeOutTween = null, activeInTween = null;

    void FadeInAndOutPanels(CanvasGroup _in)
    {
        CanvasGroup _out = activePanel;
        activePanel = _in;

        if(_out != null)
        {
            if(activeOutTween != null)
                activeOutTween.Kill(false);
            _out.interactable = false;
            _out.blocksRaycasts = false;

            activeOutTween = _out.DOFade(0f, Configs.UI.FadeOutTime).OnComplete(() =>
            {
                activeOutTween =_in.DOFade(1f, Configs.UI.FadeOutTime).OnComplete(() =>
                {
                    _in.interactable = true;
                    _in.blocksRaycasts = true;
                    activeOutTween = null;
                });
            });
        }
        else
        {
            if(activeInTween != null)
                activeInTween.Kill(false);
            
            activeInTween = _in.DOFade(1f, Configs.UI.FadeOutTime).OnComplete(() =>
            {
                activeInTween = null;
                _in.interactable = true;
                _in.blocksRaycasts = true;
            });
        }
       
       
    }

    public void ShowJoystickHighlights(int area)
    {
        for (int i = 0; i < img.joystickHighlights.Length; i++)
        {
            img.joystickHighlights[i].gameObject.SetActive(i == area);
        }
    }

    public void UpdateTexts()
    {
        txt.level.text = "LEVEL " + (SaveLoadManager.GetLevel() + 1).ToString();
    }

    public void UpdateHapticStatus()
    {
        for (int i = 0; i < img.vibrations.Length; i++)
        {
            img.vibrations[i].color = SaveLoadManager.HasVibration() ? img.vibrations[i].color.SetAlpha(1f) : img.vibrations[i].color.SetAlpha(.1f);
        }
    }

    public void ChangeHapticStatus()
    {
        SaveLoadManager.ChangeVibrationStatus();
    }

    public void OpenUpgradePopUp()
    {
        TouchHandler.I.Enable(false);     
        pnl.upgradePanel.alpha = 1f;
        pnl.upgradePanel.blocksRaycasts = true;
        pnl.upgradePanel.interactable = true;
    }
    
    public void CloseUpgradePopUp()
    {
        TouchHandler.I.Enable(true);
        pnl.upgradePanel.alpha = 0f;
        pnl.upgradePanel.blocksRaycasts = false;
        pnl.upgradePanel.interactable = false;
    }

    // public void ShowText()
    // {
    //     txt.howToPlay.gameObject.SetActive(true);
    // }

    // public void HideText()
    // {
    //     txt.howToPlay.gameObject.SetActive(false);    
    // }
    
    public void UpdateCoinTxt()
    {
        txt.coinCount.text = SaveLoadManager.GetCoin().ToString("000");
    }
    
    [System.Serializable]
    public class Panels
    {
        public CanvasGroup mainMenu, gameIn, success, fail,upgradePanel;
    }

    [System.Serializable]
    public class Images
    {
        public Image taptoStart;
        public Image[] joystickHighlights, vibrations;
    }

    [System.Serializable]
    public class Buttons
    {
        public Button play, nextLevel,getPrize,capacity, speed, size;
    }

    [System.Serializable]
    public class Texts
    {
        public TextMeshProUGUI level, coinCount,amazing;
    }
}
