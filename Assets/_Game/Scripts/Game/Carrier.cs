using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    [SerializeField] private Transform prevWagon,prevWagonPin,pool;
    [SerializeField] private float rotLerpTimer;
    private List<Grain> lsGrains = new List<Grain>();
    public ParticleSystem[] dustParticles;
    

    private void LateUpdate()
    {
        if (GameManager.isRunning)
        {
            Rotate();
            Move();
        }
    }

    void Move()
    {
        transform.position = prevWagonPin.position;
    }
    void Rotate()
    {
        if (TouchHandler.I.isDragging)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,prevWagon.rotation,rotLerpTimer);
        }
    }

    public bool IsEmpty() => lsGrains.Count == 0;
    
    public void AddGrain(Grain g)
    {
        if(!HasSpace()) return;
        
        if (!lsGrains.Contains(g))
        {
            lsGrains.Add(g);
            StackManager.I.lsCollectedGrains.Add(g);
            PlaceGrain(g);
        }
    }

    private const int stackCount = 5;
    
    void PlaceGrain(Grain g)
    {
        g.transform.SetParent(pool);
        int count = lsGrains.Count - 1;
        float posZ = (count % stackCount) * -0.675f;
        float posY = Mathf.FloorToInt((float)count / stackCount) * 0.606f;
        g.transform.DOLocalJump(new Vector3(0, posY, posZ),3f,1, .6f);
        g.transform.localRotation = Quaternion.identity;

    }
    
    public void RemoveGrain(Grain g)
    {
        if (lsGrains.Contains(g))
        {
            lsGrains.Remove(g);
            StackManager.I.lsCollectedGrains.Remove(g);
        }
    }

    public Grain RemoveLast()
    {
        Grain g = null;

        if (lsGrains.Count > 0)
        {
            g = lsGrains[lsGrains.Count - 1];
        }
        return g;
    }
    public bool HasSpace()
    {
        return lsGrains.Count < Configs.Player.carrierCapacity;
    }

    public void GiveCoin()
    {
        SaveLoadManager.AddCoin(lsGrains.Count);
    }
}
