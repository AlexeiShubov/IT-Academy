using System.Collections;
using UnityEngine;
using TMPro;

public class Quest0 : Quest, IReadyble, IStarteble, IEnding, IRestarteble
{
    [SerializeField] private Slot[] _slots;
    [SerializeField] private Item[] _items;
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _losePanel;

    private int _timer = 40;
    private bool _questIsFinish;

    private void Awake()
    {
        StartCoroutine(TimeCounter());
        SetID();
        ShuflePazle();
    }

    private void Update()
    {
        if (!_questIsFinish)
        {
            CheckForWin();
        }
    }

    private void SetID()
    {
        for (int index = 0; index < _slots.Length; index++)
        {
            _slots[index].ID = _items[index].ID = index;
        }
    }

    private void ShuflePazle()
    {
        for (int i = 0; i < 50; i++)
        {
            var randomSlot1 = _slots[Random.Range(0, _slots.Length)];
            var randomSlot2 = _slots[Random.Range(0, _slots.Length)];
            
            randomSlot1.Child.transform.SetParent(randomSlot2.transform);
            randomSlot2.Child.transform.SetParent(randomSlot1.transform);

            (randomSlot1.Child, randomSlot2.Child) = (randomSlot2.Child, randomSlot1.Child);
            
            randomSlot1.Child.transform.localPosition = Vector3.zero;
            randomSlot2.Child.transform.localPosition = Vector3.zero;
        }
    }

    private void CheckForWin()
    {
        int i = _slots.Length;

        foreach (var slot in _slots)
        {
            if (slot.ID == slot.Child.GetComponent<Item>().ID)
            {
                i--;
            }
        }

        if (i == 0 && _timer >= 0)
        {
            _questIsFinish = true;
            _victoryPanel.gameObject.SetActive(true);
            GameEvents.OnQuestIsFinish?.Invoke();
            
            return;
        }
        else if (_timer <= 0)
        {
            StopCoroutine(TimeCounter());
            _questIsFinish = true;
            _losePanel.gameObject.SetActive(true);
            GameEvents.OnQuestIsFail?.Invoke();
            
            return;
        }

        i = _slots.Length;
    }
    
    private IEnumerator TimeCounter()
    {
        while (_timer > 0 && !_questIsFinish)
        {
            _timer -= 1;
            _timerText.text = _timer.ToString();
            
            yield return new WaitForSeconds(1);
        }
    }


    public void QuestReady()
    {
        throw new System.NotImplementedException();
    }

    public void QuestStart()
    {
        throw new System.NotImplementedException();
    }

    public void QuestEnd()
    {
        throw new System.NotImplementedException();
    }
    
    public void QuestRestart()
    {
        _questIsFinish = false;
        _losePanel.gameObject.SetActive(false);
        _timer = 40;

        StartCoroutine(TimeCounter());
        ShuflePazle();
    }
}
