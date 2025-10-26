using UnityEngine;

/// <summary>
/// This class is a trigger that activates when the player enters its collider.
/// 
/// REM-i
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class OnEnterTrigger : MonoBehaviour, IEventTrigger
{
    // The event controller that this trigger will activate
    private EventController _controller;

    /// <summary>
    /// Set our event controller on startup.
    /// </summary>
    /// <param name="controller"></param>
    public void Initialize(EventController controller)
    {
        _controller = controller;
    }

    /// <summary>
    /// When a player enters the collider, trigger the event.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.CompareTag("Player"))
        {
            // Activate the event controller
            _controller.TriggerEvent();
        }
    }
}
