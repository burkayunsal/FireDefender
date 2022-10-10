
using DG.Tweening;
using UnityEngine;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image upgradeImg;

    public void OnPlayerEnter()
    {
        upgradeImg.DOFillAmount(1, Configs.UI.PopUpTimer).SetEase(Ease.Linear).OnComplete(() =>
        {
            UIManager.I.OpenUpgradePopUp();
        });
    }

    public void OnPlayerExit()
    {
        UIManager.I.CloseUpgradePopUp();
        upgradeImg.DOKill();
        upgradeImg.DOFillAmount(0, 0.5f).SetEase(Ease.Linear);
    }
   
}
