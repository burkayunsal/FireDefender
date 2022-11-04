using System;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            FireController.I.RemoveMeteor(this);
            ParticleManager.I.PlayMeteorParticles(transform.position);
            SoundManager.I.PlaySound(SoundName.Explosion);
            Vibrator.Haptic(HapticTypes.HeavyImpact);
            
            if (!SaveLoadManager.HasCamSwitchToMeteor())
            {
                SaveLoadManager.SetMetCamSwitchDone();
                new SBF.Toolkit.DelayedAction(ShowPlayer, 2f).Execute(GameManager.I);
            }
            gameObject.SetActive(false);
        }

        if (other.CompareTag("CameraSwitchTrigger") && !SaveLoadManager.HasCamSwitchToMeteor())
        {
            Transform t = transform;
            CameraController.I.MoveCamera(t.position, t);
            TouchHandler.I.OnUp();
            TouchHandler.I.Enable(false);
            PlayerController.I.dragSpeed = 0f;
            other.gameObject.SetActive(false);
        }
    }

   
    void ShowPlayer()
    {
        CameraController.I.SetPlayerAsTarget();
        TouchHandler.I.Enable(true);
    }
}