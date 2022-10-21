using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GrainSpawner : Singleton<GrainSpawner>
{
    [SerializeField] private Transform pool;
    [SerializeField] private Grain grainPrefab;
    
     public List<Grain> lsBurnedGrains = new List<Grain>();
     public List<Grain> lsAllGrains = new List<Grain>();

    public Image progressBar;
    public TextMeshProUGUI textPercentage;
    bool isFailed, isLevelEnd = false;
    [SerializeField] private Button button;

    [SerializeField] private FireFollower _fireFollower;
    private int counter = 0, totalGrain;
    
    public void SpawnGrains(List<Vector3> lsPos)
    {
        textPercentage.text = "00.0%";
        foreach (Vector3 v in lsPos)
        {
            Grain g = Instantiate(grainPrefab, pool);
            g.transform.localPosition = v;
            lsAllGrains.Add(g);
        }
        totalGrain = lsAllGrains.Count;
    }

    private void Update()
    {
        if (lsAllGrains.Count == 0 && !isLevelEnd && GameManager.isRunning)
        {
            TouchHandler.I.OnUp();
            TouchHandler.I.Enable(false);
            PlayerController.I.ForceStop();
            StackManager.I.OnLevelSucceed();
            GameManager.OnLevelCompleted();
            isLevelEnd = true;
        }
    }

    public void AddToBurnedList(Grain g)
    {
        if (!lsBurnedGrains.Contains(g))
        {
            counter++;
            lsBurnedGrains.Add(g);
            lsAllGrains.Remove(g);
        }

        if (lsBurnedGrains.Count != 0)
            _fireFollower.target = lsBurnedGrains[0].transform;

        float amount = Mathf.Clamp01(counter / (float)totalGrain);
        if (amount <= 0.8f)
        {
            progressBar.DOFillAmount(amount, 0.5f);
            textPercentage.text = (amount * 100).ToString("00.0") + "%";
        }
        else if (!isFailed)
        {
            GameManager.OnLevelFailed();
            isFailed = true;
        }
    }

    public void RemoveFromBurnedList(Grain g)
    {
        if (lsBurnedGrains.Contains(g))
        {
            lsBurnedGrains.Remove(g);
            if (lsBurnedGrains.Count == 0)
            {
                _fireFollower.HidePointer();
                if (FireController.I.lsMobs.Count == 0 && FireController.I.lsMeteors.Count == 0)
                {
                    button.gameObject.SetActive(true);
                }
            }
        }
    }
    
}
