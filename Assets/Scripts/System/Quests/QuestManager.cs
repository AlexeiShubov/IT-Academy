using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource), typeof(VideoPlayer))]
public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> _questList;
    [Space] [SerializeField] private Media _media;
    [SerializeField] private UIController _uiController;
    [SerializeField] private Transform _parentForCanvasQuests;

    private IQuestControlleble _videoManager;
    private IQuestControlleble _audioManager;

    private List<IQuestControlleble> _iQuestControllebles;
    private AudioSource _audioSource;
    private VideoPlayer _videoPlayer;
    private Quest _currentQuest;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _videoPlayer = GetComponent<VideoPlayer>();
        
        _videoPlayer.targetTexture = Resources.Load<RenderTexture>("Render Texture");
        
        _videoManager = new VideoManager(_media.VideosList, _videoPlayer);
        _audioManager = new AudioController(_media.SoundsList, _audioSource);

        _iQuestControllebles = new List<IQuestControlleble>()
        {
            _uiController,
            _videoManager,
            _audioManager
        };
    }

    private void QuestIsReady(string nameQuest)
    {
        foreach (var item in _iQuestControllebles)
        {
            item.ReadyQuest(nameQuest);
        }

        SpawnQuest(nameQuest);
    }

    private async UniTaskVoid StartQuest()
    {
        foreach (var item in _iQuestControllebles)
        {
            await item.StartQuest();
        }

        _currentQuest.gameObject.SetActive(true);
        _currentQuest.StartQuest();
    }

    private void QuestStart()
    {
        StartQuest().Forget();
    }
    
    private void QuestFinish()
    {
        foreach (var item in _iQuestControllebles)
        {
            item.ExitQuest();
        }

        _currentQuest.ExitQuest();
    }

    private void SpawnQuest(string questName)
    {
        foreach (var item in _questList)
        {
            if (item.QuestName.ToString() != questName) continue;
            
            _currentQuest = Instantiate(item, _parentForCanvasQuests.transform.position, Quaternion.identity, _parentForCanvasQuests);
                
            return;
        }
    }
    
    private void OnEnable()
    {
        GameEvents.OnQuestReady += QuestIsReady;
        GameEvents.OnStartQuest += QuestStart;
        GameEvents.OnQuestFinish += QuestFinish;
    }

    private void OnDisable()
    {
        GameEvents.OnQuestReady -= QuestIsReady;
        GameEvents.OnStartQuest -= QuestStart;
        GameEvents.OnQuestFinish -= QuestFinish;
    }
}
