using Cysharp.Threading.Tasks;

public interface IQuestControlleble
{
    public abstract void ReadyQuest(string questName);
    public abstract UniTask StartQuest();
    public abstract UniTaskVoid ExitQuest();
}
