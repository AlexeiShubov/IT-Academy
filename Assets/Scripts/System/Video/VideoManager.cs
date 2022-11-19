using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : IQuestControlleble
{
    private readonly List<Video> _videosList;
    private readonly VideoPlayer _videoPlayer;
    private readonly RawImage _renderTextureForVideoClip;

    public VideoManager(List<Video> videosList, VideoPlayer videoPlayer, RawImage renderTextureForVideoClip)
    {
        _videosList = videosList;
        _videoPlayer = videoPlayer;
        _renderTextureForVideoClip = renderTextureForVideoClip;
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
            _renderTextureForVideoClip.gameObject.SetActive(true);
        
            await UniTask.Delay(TimeSpan.FromSeconds(_renderTextureForVideoClip.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length));
            
            _videoPlayer.Play();
            
            await UniTask.Delay(TimeSpan.FromSeconds(_videoPlayer.clip.length / _videoPlayer.playbackSpeed));

            _renderTextureForVideoClip.gameObject.SetActive(false);
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
