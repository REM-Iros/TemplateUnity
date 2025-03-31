using System;
using UnityEngine.Events;

/// <summary>
/// The Event Manager for the game. This will handle all events that are called throughout
/// the game, but should break them up into different categories to help make the project 
/// easier to understand and enable scalability as time goes on.
/// 
/// WARNING: Because this is static, before anything destroys itself (including scene change),
/// the event needs to unsubscribe itself, done using -= instead of += from earlier. This
/// will cause errors otherwise and can lead to all sorts of bad news.
/// 
/// Changed to now use C# events as they are better for performance
/// 
/// REM-i
/// </summary>
public static class EventManager
{
    //This is just a set of event templates to help someone call events using this, not actually
    //meant to be used in the project
    #region Templates

    /*
     * No Input Event: 
     * EventManager.noInputEvent += someOtherScriptMethod; to subscribe
     * 
     * EventManager.InvokeNoInputEvent(); to invoke the event
     */

    //public static event Action noInputEvent;
    //public static void InvokeNoInputEvent() => noInputEvent?.Invoke();

    /*
     * One Input Event: 
     * EventManager.oneInputEvent += someOtherScriptMethod; to subscribe
     * NOTE: Some other script method must implement (T) as a parameter
     * 
     * EventManager.InvokeOneInputEvent(); to invoke the event
     */

    //public static event Action<T> oneInputEvent;
    //public static void InvokeOneInputEvent(T input) => oneInputEvent?.Invoke(input);

    /*
     * Multi Input Event: 
     * EventManager.multInputEvent += someOtherScriptMethod; to subscribe
     * NOTE: Some other script method must implement (T1, T2, T3, ..., T) as a parameter
     * 
     * EventManager.InvokeMultInputEvent(); to invoke the event
     */

    //public static event Action<T1, T2, T3, ..., T> multInputEvent;
    //public static void InvokeMultInputEvent(T1 input1, T2 input2, T3 input3, ..., T input)
    //                                          => oneInputEvent?.Invoke(input1, input2, input3, ..., input);

    #endregion

    #region Events

    #region Main Menu Events

    public static event Action MMENU_ChangeLoadSnapshot;

    #endregion

    #endregion

    #region Event Triggers

    #region Main Menu Events

    public static void InvokeMMENU_ChangeLoadSnapshot() => MMENU_ChangeLoadSnapshot?.Invoke();

    #endregion

    #endregion
}
