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
            PlayerController.I.EnableLine(false);
        }
        else if (other.CompareTag("UpgradeOpen"))
        {
            UpgradeOpen uo = other.GetComponent<UpgradeOpen>();
            uo.OnPlayerEnter(transform.position);
        }
        else if (other.CompareTag("Dirt"))
        {
            PlayerController.I.isInGround = true;
        }
        else if (other.CompareTag("Start"))
        {
            LevelHandler.I.OnGameStarted();
            other.gameObject.SetActive(false);
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
        else if (other.CompareTag("UpgradeOpen"))
        {
            UpgradeOpen uo = other.GetComponent<UpgradeOpen>();
            uo.OnPlayerExit();
        }
        else if (other.CompareTag("Dirt"))
        {
            PlayerController.I.isInGround = false;
        }
    }

}
