using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Video;

public class VideoManager : IQuestControlleble
{
    private readonly List<Video> _videosList;
    private readonly VideoPlayer _videoPlayer;

    public VideoManager(List<Video> videosList, VideoPlayer videoPlayer)
    {
        _videosList = videosList;
        _videoPlayer = videoPlayer;
    }

    public void ReadyQuest(string questName)
    {
        LoadVideo(questName);
    }

    public async UniTask StartQuest()
    {
        if (_videoPlayer.clip != null)
        {
            _videoPlayer.gameObject.SetActive(true);
            _videoPlayer.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(_videoPlayer.clip.length / _videoPlayer.playbackSpeed));
        }
    }

    public UniTaskVoid ExitQuest()
    {
        _videoPlayer.clip = null;

        return new UniTaskVoid();
    }
    
    private void LoadVideo(string nameQuest)
    {
        _videoPlayer.clip = _videosList.Find(x=> x.Name == nameQuest).VideoClip;
    }
}
