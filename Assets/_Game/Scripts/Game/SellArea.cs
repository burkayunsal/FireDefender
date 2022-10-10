using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellArea : MonoBehaviour
{
 
    private bool stopCoroutine = false;
    [SerializeField] private Transform SellPoint;
    
    public void OnPlayerEnter()
    {
        stopCoroutine = false;
        StartCoroutine(ReleaseRoutine());
    }
    IEnumerator ReleaseRoutine()
    {
        while (!stopCoroutine)
        {
            StackManager.I.Drop(SellPoint);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnPlayerExit()
    {
        stopCoroutine = true;
    }

}
