using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Grain : MonoBehaviour
{
    public enum GrainState
    {
        Fresh = 0,
        Burning = 1,
        Ash = 2,
        Collected = 3
    }
    
    [SerializeField] private GameObject[] meshes;
    [SerializeField] private Animator animFresh;
    private Transform sellPoint;
    public Color grain_color;
    
    private GrainState mState = GrainState.Fresh;
    public GrainState State
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
                case GrainState.Burning :
                    OnBurning();
                    break;
                case GrainState.Ash :
                    OnAsh();
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
            if (State == GrainState.Fresh)
                State = GrainState.Burning;
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
        float min = LevelHandler.I.GetLevel().minSpreadTime;
        float max = LevelHandler.I.GetLevel().maxSpreadTime;
        yield return new WaitForSeconds(Random.Range(min, max));
        activeRoutine = null;
        BurnAround();
        State = GrainState.Ash;
    }

    void OnAsh()
    {
        GrainSpawner.I.RemoveFromBurnedList(this);
    }

    void BurnAround()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2f, Vector3.up, 3f, 1 << 8);
        
        for (int i = 0; i < hits.Length; i++)
        {
            Grain g = hits[i].collider.gameObject.GetComponent<Grain>();
            if (g)
            {
                if (g.State == GrainState.Fresh)
                {
                    g.State = GrainState.Burning;
                    GrainSpawner.I.AddToBurnedList(g);
                }
            }
        }
    }
    
    public void OnDrop(Transform target)
    {
        sellPoint = target;
        transform.DOJump(target.position, 3f,1,0.75f).OnComplete(() =>
        {
            Coin coin = PoolManager.I.GetObject<Coin>();
            coin.transform.position = transform.position;
            
            SaveLoadManager.AddCoin(1);
            SoundManager.I.PlaySound(SoundName.GoldCollect);
            
            transform.parent = null;
            gameObject.SetActive(false);
        });
    }

    public void Move()
    {
        State = GrainState.Collected;
        transform.DOMove(sellPoint.position, .5f).OnComplete(() =>
        {
            //SaveLoadManager.AddCoin(1);
            //SoundManager.I.PlaySound(SoundName.GoldCollect);
            transform.parent = null;
            gameObject.SetActive(false);
        });
        
    }
    
    
    
}
