using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{

    [SerializeField] public ParticleSystem explosionPS,MeteorExplosionParticle, confetti;
    public void Explode(Vector3 v)
    {
        explosionPS.transform.position = v;
        explosionPS.Play();
    }

    public void PlayDust()
    {
        foreach(ParticleSystem ps in PlayerController.I.plyayerDustParticles)
        {
            ps.Play();
        }
        
        foreach (Carrier c in StackManager.I.lsActiveCarriers)
        {
            foreach (ParticleSystem dustParticle in c.dustParticles)
            {
                dustParticle.Play();
            }
        }
    }
    
    public void StopDust()
    {
        foreach(ParticleSystem ps in PlayerController.I.plyayerDustParticles)
        {
            ps.Stop();
        } 
        
        foreach (Carrier c in StackManager.I.lsActiveCarriers)
        {
            foreach (ParticleSystem dustParticle in c.dustParticles)
            {
                dustParticle.Stop();
            }
        }
    }

    public void PlayMeteorParticles(Vector3 v)
    {
        MeteorExplosionParticle.transform.position = v;
        MeteorExplosionParticle.Play();
    }

    public void Confetti()
    {
        confetti.Play();
    }
    
}
