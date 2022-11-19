using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public enum NameQuest
    {
        Quest0,
        Quest1,
        Quest2,
        Quest3,
        Quest4,
        Quest5
    }
    
    public NameQuest QuestName;

    public abstract void StartQuest();
    public abstract void ExitQuest();
}
