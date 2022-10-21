using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : PoolObject
{
    public override void OnDeactivate()
    {      
        gameObject.transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }
    

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public void CoinAtSellPoint(Vector3 target)
    {
        gameObject.transform.localScale = Vector3.one * 0.5f;
        gameObject.transform.DOJump(target, 10f, 1, 1f);
        gameObject.transform.DOScale(Vector3.one, 1f).SetEase(Ease.Linear).OnComplete(OnDeactivate);
    }

    public void CoinMovementToUpgradeOpen(Vector3 target)
    {
        gameObject.transform.DOJump(target, 7f, 1, 1f).OnComplete(OnDeactivate);
        gameObject.transform.DOScale(Vector3.one * 0.5f, 1f).SetEase(Ease.Linear);
    }
    
    public override void OnCreated()
    {
        OnDeactivate();
    }
}
