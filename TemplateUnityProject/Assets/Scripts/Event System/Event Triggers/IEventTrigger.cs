/// <summary>
/// This interface is used to mark a class as an event trigger.
/// 
/// REM-i
/// </summary>
public interface IEventTrigger
{
    /// <summary>
    /// This is the generic method that each trigger will inherit and use.
    /// </summary>
    /// <param name="controller"></param>
    void Initialize(EventController controller);
}
