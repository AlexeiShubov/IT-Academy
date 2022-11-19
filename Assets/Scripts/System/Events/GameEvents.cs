using System;

public static class GameEvents
{
    public static Action<string> OnQuestReady;
    public static Action OnStartQuest;
    public static Action OnQuestFinish;
    public static Action OnQuestIsFail;
}
