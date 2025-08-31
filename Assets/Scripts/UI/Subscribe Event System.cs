using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Bootstrap script to attach to each new scene with an event system to attach the event system
/// to the focus manager.
/// 
/// REM-i
/// </summary>
public class SubscribeEventSystem : MonoBehaviour
{
    [Tooltip("The event system reference we send to the Focus Manager.")]
    [SerializeField, Header("Event System Vars")]
    private EventSystem _es;

    [Tooltip("Default gameobject to subscribe to the event system.")]
    [SerializeField]
    private GameObject _defaultObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        ServiceLocator.Get<UIFocusManager>().AttachEventSystem(_es, _defaultObj);
    }
}
