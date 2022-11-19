using UnityEngine;

public class TriggerQuest : MonoBehaviour
{
    [SerializeField] Quest _quest;

    public string Name => _quest.QuestName.ToString();
}
