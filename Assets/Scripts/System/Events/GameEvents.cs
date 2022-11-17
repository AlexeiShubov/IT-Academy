using System;

public static class GameEvents
{
    public static Action<bool> OnCanMoving;
    public static Action<bool> OnCanRotate;
    public static Action<string> OnStartQuestName;
    public static Action<string> OnQuestIsReady; 
    public static Action OnQuestIsFinish;
    public static Action OnQuestIsFail;
}
