using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [SerializeField] public GameObject[] lightnings;

    public void OnGameStarted()
    {
        StartCoroutine(LightningHit());
    }

    IEnumerator LightningHit()
    {
        foreach (var lightning in lightnings)
        {
            lightning.gameObject.SetActive(true);
            SoundManager.I.PlaySound(SoundName.Lightning);
            yield return new WaitForSeconds(1f);
        }
    }
}
