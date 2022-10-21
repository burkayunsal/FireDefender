using System;
using System.Collections;
using DG.Tweening;
using SBF.Extentions.Transforms;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] private Rigidbody rb;

    [SerializeField] Transform HandPoint, shootPoint,targetPoint;
  
   private Vector3 throwVector, spawnPoint;

   private bool startedWalking = false;
   private bool hasAttacked = false; 
   
    public enum AnimStates
    {
        Idle,
        Run,
        Throw
    }

    AnimStates _animState = AnimStates.Idle;

    AnimStates AnimState
    {
        get => _animState;
        set
        {
            if (_animState != value)
            {
                _animState = value;
                anim.SetTrigger(_animState.ToString());
            }
        }
    }

    delegate void OnUpdate();
    OnUpdate onUpdate;

    private void Update()
    {
        onUpdate?.Invoke();
    }

    private void Start()
    {
        spawnPoint = transform.position;
    }

    void Run()
    {
        Vector3 targetPos = !hasAttacked ? shootPoint.position : spawnPoint;
        AnimState = AnimStates.Run;
        rb.velocity = rb.transform.forward * 5f;
        rb.transform.SlowLookAt(targetPos, 8f);

        float distance = Vector3.Distance(rb.transform.position, targetPos);

        if (distance <= 1.5f && !SaveLoadManager.HasCamSwitch())
        {
            CameraController.I.SetTarget(transform);
        }
        if (distance <= 0.5f && !hasAttacked)
        {
            StopRunning();
            new SBF.Toolkit.DelayedAction(Throw,.3f).Execute(this);
            new SBF.Toolkit.DelayedAction(StartRunning,1f).Execute(this);
        }
        else if (distance <= 0.5f && hasAttacked)
        {
            StopRunning();
            gameObject.SetActive(false);
        }
    }

    public void StartRunning()
    {
        AnimState = AnimStates.Run;
        onUpdate += Run;
        rb.isKinematic = false;
    }

    public void StopRunning()
    {
        AnimState = AnimStates.Idle;
        onUpdate -= Run;
        rb.isKinematic = true;
    }

    public void Throw()
    {
        Molotov a = PoolManager.I.GetObject<Molotov>();
        a.transform.position = HandPoint.position;
        
        rb.transform.SlowLookAt(targetPoint.position, 10f);
        throwVector = targetPoint.position - shootPoint.position;
        hasAttacked = true;
        a.Fire(throwVector);
        ShowMolotov(a.transform);
        anim.SetTrigger("Throw");

        FireController.I.RemoveMob(this);
    }

    void ShowMolotov(Transform t)
    {
        if (!SaveLoadManager.HasCamSwitch())
        {
            SaveLoadManager.SetCamSwitchDone();
            TouchHandler.I.textHidden = false;
            
            UIManager.I.ShowText();
            CameraController.I.SetTarget(t);
            
            TouchHandler.I.OnUp();
            TouchHandler.I.Enable(false);
            PlayerController.I.dragSpeed = 0f;
        }
    }
}
    
