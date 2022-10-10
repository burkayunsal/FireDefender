using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelGrainPattern : MonoBehaviour
{
    public abstract List<Vector3> GetPattern();
}
