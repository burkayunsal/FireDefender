using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : PoolObject
{
    public override void OnDeactivate()
    {
        gameObject.transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
        
        gameObject.transform.DOMoveY(6f, .5f).SetEase(Ease.Linear);
        gameObject.transform.DOScale(Vector3.one, .5f).SetEase(Ease.Linear).OnComplete(OnDeactivate);
    }

    public override void OnCreated()
    {
        OnDeactivate();
    }
}
