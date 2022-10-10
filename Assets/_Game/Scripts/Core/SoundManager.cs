using UnityEngine;
public enum SoundName
{
    GoldCollect = 0,
    Harvest = 1,
    Explosion = 2,
    Molotov = 3,
    
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;

    public void PlaySound(SoundName soundName)
    {
        _audioSource.PlayOneShot(_audioClips[(int)soundName]);
    }

}