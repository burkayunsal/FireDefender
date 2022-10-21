using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackManager : Singleton<StackManager>
{
    [SerializeField] private Carrier[] allCarriers;
    public List<Carrier> lsActiveCarriers = new List<Carrier>();
    public List<Grain> lsCollectedGrains = new List<Grain>();

    private void Start()
    {
        InitCarriers();
    }

    public void InitCarriers()
    {
        int lvl = SaveLoadManager.GetCarrierLevel();

        for (int i = 0; i < allCarriers.Length; i++)
        {
            if (i <= lvl)
            {
                allCarriers[i].gameObject.SetActive(true);
                lsActiveCarriers.Add(allCarriers[i]);
            }
        }
    }

    public void CollectGrain(Grain g)
    {
        if (HasSpace())
        {
            Carrier c = GetAvailableCarrier();
            if (c != null)
            {
                g.State = Grain.GrainState.Collected;
                SoundManager.I.PlaySound(SoundName.Harvest);
                c.AddGrain(g);
                GrainSpawner.I.lsAllGrains.Remove(g);
            }
        }
        else
        {
            PlayerController.I.playerCapacityisFull(true);
        }
    }
    
    

    public bool HasSpace()
    {
        foreach (Carrier c in lsActiveCarriers)
        {
            if (c.HasSpace()) return true;
        }
        return false;
    }
    
    Carrier GetAvailableCarrier()
    {
        Carrier c = null;
        for (int i = 0; i < lsActiveCarriers.Count; i++)
        {
            if (lsActiveCarriers[i].HasSpace())
            {
                c = lsActiveCarriers[i];
                break;
            }
        }
        if (c == null)
        {
            // ALL FULL
        }
        return c;
    }

    Carrier GetLastCarrier()
    {
        for (int i = lsActiveCarriers.Count; i > 0; i--)
        {
            if (!lsActiveCarriers[i-1].IsEmpty())
                return lsActiveCarriers[i-1];
        }

        return null;
    }

    public void Drop(Transform targetPos)
    {
        Carrier c = GetLastCarrier();
        if (c != null)
        {
            Grain droppedGrain = c.RemoveLast();
            c.RemoveGrain(droppedGrain);
            droppedGrain.OnDrop(targetPos);
            PlayerController.I.playerCapacityisFull(false);

        }
    }

    public void StartlevelEndRoutine()
    {
        StartCoroutine(CollectedRoutine());
    }

    IEnumerator CollectedRoutine()
    {
        for (int i = 0; i < lsCollectedGrains.Count; i++)
        {
            yield return new WaitForSeconds(0.05f);
    
            Coin coin = PoolManager.I.GetObject<Coin>();
            coin.transform.position = transform.position;
            
            SaveLoadManager.AddCoin(1);
            SoundManager.I.PlaySound(SoundName.GoldCollect);
            gameObject.SetActive(false);
        }
    }

    public void OnLevelSucceed()
    {
        foreach (var carrier in lsActiveCarriers)
        {
            carrier.GiveCoin();
        }
    }
}
