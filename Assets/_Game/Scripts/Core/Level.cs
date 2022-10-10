using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelGrainPattern pattern;
    public float minSpreadTime, maxSpreadTime;
    private void Start()
    {
        SpawnGrains();
    }

    void SpawnGrains()
    {
        GrainSpawner.I.SpawnGrains(pattern.GetPattern());
    }
}
