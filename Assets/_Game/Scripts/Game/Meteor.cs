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
            gameObject.SetActive(false);
        }
    }
}