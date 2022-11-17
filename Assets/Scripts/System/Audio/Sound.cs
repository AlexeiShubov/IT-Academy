using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public bool Loop;
    public AudioClip AudioClip;

    public void Construct(AudioSource audioSource)
    {
        audioSource.clip = AudioClip;
        audioSource.loop = Loop;
    }
}
