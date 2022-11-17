using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [Space] public List<Video> videosList;

    [SerializeField] private VideoPlayer _videoPlayer;

    public IEnumerator PlayVideo(string videoName)
    {
        _videoPlayer.clip = videosList.Find(x=> x.Name == videoName).VideoClip;

        if (_videoPlayer.clip != null)
        {
            _videoPlayer.gameObject.SetActive(true);
            _videoPlayer.Play();

            yield return new WaitForSeconds((float)_videoPlayer.clip.length / _videoPlayer.playbackSpeed);

            _videoPlayer.clip = null;
            _videoPlayer.gameObject.SetActive(false);
        }
    }
}
