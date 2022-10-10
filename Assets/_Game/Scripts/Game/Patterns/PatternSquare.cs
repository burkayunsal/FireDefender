using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class PatternSquare : LevelGrainPattern
{
    [SerializeField] private int sizeX, sizeZ;
    [SerializeField] private float distBetweenCrops;
    
    [SerializeField] bool useOffset;
    [ConditionalField(nameof(useOffset), true, false)] 
    [SerializeField] private float offsetX, offsetZ;
    public override List<Vector3> GetPattern()
    {
        List<Vector3> ls = new List<Vector3>();
        
        for (int i = 0; i < sizeZ; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                if (!useOffset)
                {
                    ls.Add(new Vector3(j * distBetweenCrops ,0,i * distBetweenCrops));
                }
                else
                {
                    ls.Add(new Vector3((j * distBetweenCrops) + offsetX ,0,(i * distBetweenCrops) + offsetZ));
                }
            }
        }
        
        return ls;
    }
}
