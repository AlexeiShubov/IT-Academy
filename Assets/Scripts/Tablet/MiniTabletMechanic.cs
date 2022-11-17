using UnityEngine;

public class MiniTabletMechanic : MonoBehaviour
{
    [SerializeField] private GameObject _miniTablet;
    [SerializeField] private GameObject _tablet;
    
    private Animator _animator;

    private string _numberReadyQuest;

    private void Awake()
    {
        _animator = _miniTablet.GetComponent<Animator>();
    }

    public void OnClickMiniTablet()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("QuestIsActive"))
        {
            GameEvents.OnStartQuestName?.Invoke(_numberReadyQuest);
            _tablet.SetActive(true);
            _miniTablet.SetActive(false);
        }
    }
    
    private void QuestIsReady(string numberReadyQuest)
    {
        _numberReadyQuest = numberReadyQuest;
        _animator.CrossFade("QuestIsActive", 0);
    }

    private void QuestIsFinish()
    {
        _miniTablet.SetActive(true);
        _tablet.SetActive(false);
        _animator.CrossFade("Idle", 0);
    }
    
    private void OnEnable()
    {
        GameEvents.OnQuestIsReady += QuestIsReady;
        GameEvents.OnQuestIsFinish += QuestIsFinish;
    }
    
    private void OnDisable()
    {
        GameEvents.OnQuestIsReady -= QuestIsReady;
        GameEvents.OnQuestIsFinish -= QuestIsFinish;
    }
}
