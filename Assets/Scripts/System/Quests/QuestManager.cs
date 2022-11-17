using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _questList;
    [SerializeField] private GameObject _parentForCanvasQuests;
    [SerializeField] private VideoManager _videoManager;
    [SerializeField] private AudioManager _audioManager;
    
    private void StartQuest(string nameQuest)
    {
        StartCoroutine(ActiveQuest(nameQuest));
    }
    
    private IEnumerator ActiveQuest(string nameQuest)
    {
        yield return new WaitForSeconds(1.25f);

        foreach (var item in _videoManager.videosList) // Переработать этот бред
        {
            if (item.Name == nameQuest)
            {
                yield return StartCoroutine(_videoManager.PlayVideo(nameQuest));
            }
        }

        _audioManager.PlaySound(nameQuest);
        
        Instantiate(_questList.Find(x => x.CompareTag(nameQuest)), _parentForCanvasQuests.transform.position, Quaternion.identity, _parentForCanvasQuests.transform);
    }

    private void OnEnable()
    {
        GameEvents.OnStartQuestName += StartQuest;
    }
    
    private void OnDisable()
    {
        GameEvents.OnStartQuestName -= StartQuest;
    }
}
