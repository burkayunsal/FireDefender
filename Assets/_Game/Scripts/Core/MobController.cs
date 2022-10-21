using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
        [SerializeField] public Mob[] mobs;
        public void OnGameStarted()
        {
            foreach (var _mob in mobs)
            {
                FireController.I.AddMob(_mob);
                _mob.StartRunning(); 
            }
        }
}
