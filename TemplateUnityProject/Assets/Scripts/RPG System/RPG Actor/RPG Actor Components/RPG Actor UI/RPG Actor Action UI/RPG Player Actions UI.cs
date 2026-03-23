using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script handles the UI for the player actions in the bullet hell segment. It will display the text for the actions the player can take,
/// as well as any relevant information such as cooldowns or costs.
/// 
/// REM-i
/// </summary>
public class PlayerActionsUI : MonoBehaviour
{
    #region Vars

    // Player input ref
    [Tooltip("This is the Player Input Script from the new Input System for passing current control scheme to actions")]
    private PlayerInput _playerInput;

    [Tooltip("This is the player transform that we want to follow with the UI")]
    [SerializeField, Header("Player Transform Reference")]
    private Transform _playerTransform;

    // UI Elements
    [Tooltip("This is the parent object that contains all action UI Elements")]
    [SerializeField, Header("Action UI Parent")]
    private GameObject _actionsUIParent;

    [Tooltip("This is the transform for the action UI GameObj")]
    private RectTransform _actionUITransform;

    [Tooltip("These are the action UI Elements that compose the menu")]
    [SerializeField, Header("Action UI Components")]
    private List<PlayerActionUIElement> _actionUIElements;

    #endregion

    #region Methods

    /// <summary>
    /// On Awake, get the Player Input component for control scheme detection.
    /// </summary>
    public void Init(PlayerInput playerInput)
    {
        // Set player input and pass to elements
        _playerInput = playerInput;

        foreach (var actionUIElement in _actionUIElements)
        {
            actionUIElement.Init(_playerInput);
        }

        // Store the ref for performance and ease of access
        _actionUITransform = _actionsUIParent.GetComponent<RectTransform>();

        // Deactivate menu at start
        _actionsUIParent.SetActive(false);
    }

    /// <summary>
    /// Enables the action menu UI. 
    /// </summary>
    public void ActivateActionMenu()
    {
        _actionsUIParent.SetActive(true);
    }

    /// <summary>
    /// Disables the action menu UI.
    /// </summary>
    public void DeactivateActionMenu()
    {
        _actionsUIParent.SetActive(false);
    }

    /// <summary>
    /// Called by the team controller when a character is ready to act, updates the action menu text.
    /// </summary>
    public void SetMenuElementAtIndex(int actionIndex, string actionText)
    {
        _actionUIElements[actionIndex].SetActionName(actionText);
    }

    /// <summary>
    /// Have the UI Element follow the player transform.
    /// </summary>
    private void FixedUpdate()
    {
        // Transpose world position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_playerTransform.position);

        // Follow player position
        _actionUITransform.position = screenPos;
    }

    #endregion
}
