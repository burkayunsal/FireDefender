using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PrizeHandler : Singleton<PrizeHandler>
{
    [SerializeField] PrizeCanvas prefab;
    [SerializeField] PrizeRule prizeRule;

    private PrizeCanvas crntOne;

    private void Start()
    {
        if (prizeRule != null)
        {
            crntOne = Instantiate(prefab, UIManager.I.GetPanel().success.transform);
            UIManager.I.GetButtons().getPrize = crntOne.btnReceivePrize;
        }
    }

    public void ShowPrizeProcess()
    {
        if (crntOne == null)
        {
            UIManager.I.OnSuccess(false);
            return;
        } 

        PrizeRule.Prize prize = prizeRule.GetCurrentRule();

        if (prize == null)
        {
            UIManager.I.OnSuccess(false);
            return;
        }

        AnimatePrize(prize);

    }

    void AnimatePrize(PrizeRule.Prize prize)
    {
        crntOne.img_prizeBg.sprite = prize.sprite;
        crntOne.img_prizeVisible.sprite = prize.sprite;

        crntOne.img_prizeVisible.fillAmount = 0f;

        crntOne.canvasGroup.alpha = 1f;
        crntOne.canvasGroup.interactable = true;

        UIManager.I.OnPrizeShow(prize.percentage, crntOne);
    }

}
