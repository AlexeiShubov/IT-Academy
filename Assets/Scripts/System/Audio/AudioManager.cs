using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Space] public List<Sound> soundsList;

    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        PlaySound("Start");
    }

    public void PlaySound(string soundName)
    {
        StopAllCoroutines();
        soundsList.Find(x=> x.Name == soundName).Construct(_audioSource);
        _audioSource.Play();
        
        StartCoroutine(PlayMusic(_audioSource.clip.length));
    }

    private IEnumerator PlayMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        soundsList.Find(x => x.Name == "Music").Construct(_audioSource);
        _audioSource.Play();
    }

    private void OnEnable()
    {
        GameEvents.OnQuestIsFinish += () => PlaySound("Win");
        GameEvents.OnQuestIsFail += () => PlaySound("Loos");
    }

    private void OnDisable()
    {
        GameEvents.OnQuestIsFinish -= () => PlaySound("Win");
        GameEvents.OnQuestIsFail -= () => PlaySound("Loos");
    }
}
