using UnityEngine;

/// <summary>
/// This class serves as a context container for passing relevant information to event actions when they are executed.
/// 
/// REM-i
/// </summary>
public class EventContext
{
    // The Player GameObject needs to always be stored for events.
    public GameObject player;
    // The Camera needs to always be stored for events.
    public Camera camera;
    // The Event Controller that is managing the current event.
    public EventController eventController;
}
