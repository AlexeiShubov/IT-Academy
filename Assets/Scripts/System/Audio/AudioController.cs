using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AudioController : IQuestControlleble
{
    private readonly List<Sound> _soundsList;
    private readonly AudioSource _audioSource;
    private AudioClip _currentQuestAudioClip;

    public AudioController(List<Sound> soundsList, AudioSource audioSource)
    {
        _soundsList = soundsList;
        _audioSource = audioSource;

        GameEvents.OnQuestIsFail += QuestIsFail;
        
        GameStart().Forget();
    }

    public void ReadyQuest(string questName)
    {
        _currentQuestAudioClip = _soundsList.Find(x => x.Name == questName).AudioClip;
    }
    
    public async UniTask StartQuest()
    {
        _audioSource.clip = _currentQuestAudioClip;
        _audioSource.Play();

        await UniTask.Delay(TimeSpan.FromSeconds(_audioSource.clip.length));
        
        LoadSound("Music");
    }
    
    public async UniTaskVoid ExitQuest()
    {
        LoadSound("Win");

        await UniTask.Delay(TimeSpan.FromSeconds(_audioSource.clip.length));

        LoadSound("Music");
    }

    private void QuestIsFail()
    {
        LoadSound("Loos");
    }
    
    private void LoadSound(string nameSound)
    {
        _soundsList.Find(x => x.Name == nameSound).Construct(_audioSource);
        _audioSource.Play();
    }

    private async UniTaskVoid GameStart()
    {
        LoadSound("Start");

        await UniTask.Delay(TimeSpan.FromSeconds(_audioSource.clip.length));
        
        LoadSound("Music");
    }
}
