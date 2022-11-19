using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIController : MonoBehaviour, IQuestControlleble
{
    [SerializeField] private Animator _miniTablet;
    [SerializeField] private Animator _tablet;
    [SerializeField] private Animator _renderTextureForVideoClip;
    [SerializeField] private MiniTabletMechanic _miniTabletMechanic;
    
    public void ReadyQuest(string questName)
    {
        _miniTabletMechanic.ReadyQuest(questName);
        
        _miniTablet.CrossFade("QuestIsActive", 0);
    }

    public async UniTask StartQuest()
    {
        _miniTablet.gameObject.SetActive(false);
        _tablet.gameObject.SetActive(true);

        await UniTask.Delay(TimeSpan.FromSeconds(_tablet.GetCurrentAnimatorClipInfo(0).Length));
        
        _renderTextureForVideoClip.gameObject.SetActive(true);
        
        await UniTask.Delay(TimeSpan.FromSeconds(_renderTextureForVideoClip.GetCurrentAnimatorClipInfo(0).Length));
    }

    public UniTaskVoid ExitQuest()
    {
        _miniTablet.gameObject.SetActive(true);
        _tablet.gameObject.SetActive(false);
        _renderTextureForVideoClip.gameObject.SetActive(false);

        return new UniTaskVoid();
    }
}
