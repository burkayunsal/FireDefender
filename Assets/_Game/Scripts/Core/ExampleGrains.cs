
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SBF.Extentions.Vector;
using UnityEngine;

public class ExampleGrains : MonoBehaviour
{
    public enum ExpGrainState
    {
        Fresh = 0,
        Burning = 1,
        Ash = 2,
    }
    
    [SerializeField] private GameObject[] meshes;
    [SerializeField] private Animator animFresh;
    private ExpGrainState mState = ExpGrainState.Fresh;
    public ExpGrainState State
    {
        get => mState;
        set
        {
            mState = value;
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].SetActive(i == (int)mState);
            }

            switch (mState)
            {
                case ExpGrainState.Burning :
                    OnBurning();
                    break;
                
            }
        }
    }
    private void Start()
    {
        animFresh.SetFloat("Speed", Random.Range(0.3f, 0.9f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            if (State == ExpGrainState.Fresh)
                State = ExpGrainState.Burning;
        }
    }
    private Coroutine activeRoutine = null;
    
    void OnBurning()
    {
        if (activeRoutine == null)
        {
            activeRoutine = StartCoroutine(BurnRoutine());
        }
    }

    IEnumerator BurnRoutine()
    {
        
        yield return new WaitForSeconds(Random.Range(1 , 2));
        
        activeRoutine = null;
        BurnAround();
        State = ExpGrainState.Ash;
    }
    


    void BurnAround()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 1.5f, Vector3.up, 3f, 1 << 8);
        
        for (int i = 0; i < hits.Length; i++)
        {
            ExampleGrains g = hits[i].collider.gameObject.GetComponent<ExampleGrains>();
            Grain grain = hits[i].collider.gameObject.GetComponent<Grain>();

            if (g)
            {
                if (g.State == ExpGrainState.Fresh)
                {
                    g.State = ExpGrainState.Burning;
                }
            }
            if (grain)
            {
                if (grain.State == Grain.GrainState.Fresh)
                {
                    grain.State = Grain.GrainState.Burning;
                }
            }
        }
    }
    
}

