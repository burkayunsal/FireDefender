using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [SerializeField] public Meteor[] meteors;
    
    public void OnGameStarted()
    {
        foreach (var _meteor in meteors)
        {
            FireController.I.AddMeteor(_meteor);
            _meteor.gameObject.SetActive(true);
        }
    }
}
