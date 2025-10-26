using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// This script should enable scroll views to scroll with kb and gamepad inputs. It's weird
/// that isn't a default feature, but whatever I guess.
/// 
/// REM-i
/// </summary>
[RequireComponent(typeof(ScrollRect))]
public class ScrollWithInput : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the Scroll Rect component.")]
    [SerializeField, Header("Viewport Vars")]
    private ScrollRect _scrollRect;

    // This is the viewport of the scroll rect
    private RectTransform _viewport;

    // This is the content of the scroll rect
    private RectTransform _content;

    // This is the last object that was selected
    private GameObject _lastSelected;

    #endregion

    #region Methods

    /// <summary>
    /// On startup, we need to get the viewport and the content
    /// </summary>
    void Awake()
    {
        // Don't run this if we don't have a scroll rect
        if (_scrollRect == null)
        {
            return;
        }

        // Get the viewport
        _viewport = _scrollRect.viewport;

        // Get the content
        _content = _scrollRect.content;
    }

    /// <summary>
    /// This is what will actually run the scroll to method.
    /// </summary>
    private void LateUpdate()
    {
        // Get the current selected gameobject from the eventsystem
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        // If nothing is selected, if content isn't found, or if the object isn't a child, just return
        if (selected == null || _content == null || !selected.transform.IsChildOf(_content))
        {
            return;
        }

        // Only react if selection changed
        if (selected == _lastSelected)
        {
            return;
        }

        _lastSelected = selected;

        // If we have no target, return
        if (selected.TryGetComponent<RectTransform>(out var target)) 
        { 
            ScrollTo(target); 
        }
    }

    /// <summary>
    /// This script will scroll the view so that all of the options are available.
    /// </summary>
    /// <param name="target"></param>
    private void ScrollTo(RectTransform target)
    {
        // Update the canvas before making changes
        Canvas.ForceUpdateCanvases();

        // Get content and viewport height
        float contentHeight = _content.rect.height;
        float viewportHeight = _viewport.rect.height;

        // Return if nothing to scroll
        if (contentHeight <= viewportHeight)
        {
            return;
        }

        // Get the position locally within the content
        Vector3 localPosition = _content.InverseTransformPoint(target.position);

        // Find the distance from the top
        float itemCenterFromTop = (_content.rect.height * (1f - _content.pivot.y)) - localPosition.y;

        // To avoid only showing half of the button, we shift it up by half
        float targetScroll = (itemCenterFromTop - (viewportHeight * 0.5f)) / (contentHeight - viewportHeight);

        // Invert it and clamp it
        float normalized = 1f - Mathf.Clamp01(targetScroll);

        _scrollRect.verticalNormalizedPosition = normalized;
    }

    #endregion
}
