using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {      
        if(other.CompareTag("Draggable"))
        {
            Grain g = other.GetComponent<Grain>();
            if (g)
            {
                if (g.State == Grain.GrainState.Fresh)
                {
                    StackManager.I.CollectGrain(g);
                }
            }
        } 
        else if (other.CompareTag("Upgrade"))
        {
            UpgradeArea upgradeArea = other.GetComponent<UpgradeArea>();
            upgradeArea.OnPlayerEnter();

        }
        else if (other.CompareTag("SellArea"))
        {
            SellArea sellArea = other.GetComponent<SellArea>();
            sellArea.OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Upgrade"))
        {
            UpgradeArea upgradeArea = other.GetComponent<UpgradeArea>();
            upgradeArea.OnPlayerExit();
        }
        else if (other.CompareTag("SellArea"))
        {
            SellArea sellArea = other.GetComponent<SellArea>();
            sellArea.OnPlayerExit();
        }
    }

}