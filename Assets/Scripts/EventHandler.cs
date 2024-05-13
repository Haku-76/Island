using System;
public static class EventHandler
{
    /// <summary>
    /// 结束游戏的事件
    /// </summary>
    /// <param name="data">游戏结果数据 ： 酒精度， 口感</param>
    public static event Action<MixedWine_Data> FinishGameEvent;

    public static void CallFinishGameEvent(MixedWine_Data data)
    {
        FinishGameEvent?.Invoke(data);
    }

    
    public static event Action GameStartEvent;
    public static void CallGameStartEvent()
    {
        GameStartEvent?.Invoke();
    }
}
