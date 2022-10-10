using System.Collections.Generic;
using UnityEngine;

public class PatternPlus : LevelGrainPattern
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
                if (!(sizeZ * 0.45f < i && i < sizeZ * 0.55f || sizeX * 0.45f < j && j < sizeX * 0.55f ))
                {
                    ls.Add(new Vector3(j * distBetweenCrops ,0,i * distBetweenCrops));
                }
            }
        }
        
        return ls;
    }
}
