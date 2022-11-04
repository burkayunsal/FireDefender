using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class Molotov : PoolObject
{
    public Rigidbody rb;

    public override void OnCreated()
    {
        OnDeactivate();
    }

    public override void OnDeactivate()
    {
        gameObject.SetActive(false);
    }

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
        new SBF.Toolkit.DelayedAction( () =>
        {
            OnDeactivate();
        },4f).Execute(GameManager.I);
    }

    public void Fire(Vector3 v)
    {
        rb.transform.LookAt(v);
        rb.AddForce(rb.transform.forward * v.magnitude / 2f + Vector3.up * 10f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            ParticleManager.I.Explode(transform.position);
            SoundManager.I.PlaySound(SoundName.Molotov);
            Vibrator.Haptic();
            OnDeactivate();
        }
    }
    
}
