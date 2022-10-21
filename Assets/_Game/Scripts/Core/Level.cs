using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelGrainPattern pattern;
    public float minSpreadTime, maxSpreadTime;
    [SerializeField] private MeteorController mc;
    [SerializeField] private MobController _mobController;
    private void Start()
    {
        SpawnGrains();
    }

    void SpawnGrains()
    {
        GrainSpawner.I.SpawnGrains(pattern.GetPattern());
    }

    public void OnGameStarted()
    {
        if(mc)
            mc.OnGameStarted();

        if (_mobController)
        {
            _mobController.OnGameStarted();
        }
    }
}
