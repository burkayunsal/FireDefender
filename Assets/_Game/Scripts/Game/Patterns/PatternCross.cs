using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCross : LevelGrainPattern
{
    [SerializeField] private int sizeX, sizeZ;
    [SerializeField] private float distBetweenCrops;
    public override List<Vector3> GetPattern()
    {
        List<Vector3> ls = new List<Vector3>();
        
        for (int i = 0; i < sizeZ; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                if (i+j > 14 && i+j < 26 || Mathf.Abs(i-j) < 4)
                {
                    ls.Add(new Vector3(j * distBetweenCrops ,0,i * distBetweenCrops));
                }
            }
        }
        
        return ls;
    }
}

