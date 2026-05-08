using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script handles the UI for the player actions. It will display the text for the actions the player can take,
/// as well as any relevant information such as cooldowns or costs.
/// 
/// REM-i
/// </summary>
public class PlayerActionsUI : MonoBehaviour, RPGIUIInterface
{
    #region Vars

    [Tooltip("This is the current actor we are following with the action menu.")]
    private RPGActor _currentActor;

    [Tooltip("This is the current actor's transform we are following with the action menu.")]
    private Transform _currentActorTransform;

    [Tooltip("These are the action UI Elements that compose the menu")]
    [SerializeField, Header("Action UI Components")]
    private List<PlayerActionUIElement> _actionUIElements;

    #region Events

    public event Action<int> OnActionAtIndex;
    public event Action OnRunSelected;

    #endregion

    #endregion

    #region Methods

    /// <summary>
    /// On Awake, get the Player Input component for control scheme detection.
    /// </summary>
    public void Init(PlayerInput playerInput)
    {
        //TODO: Work on this eventually, this rebinding crap is awful
        foreach (var actionUIElement in _actionUIElements)
        {
            actionUIElement.SetUpRebindIcons(playerInput);
        }

        // Deactivate all elements at startup.
        foreach (PlayerActionUIElement element in _actionUIElements)
        {
            element.DeactivateUIElement();
        }

        // Deactivate menu
        DeactivateActionMenu();
    }

    /// <summary>
    /// On bind, we get the actor and cache actor and transform, initialize the icons for the actions, 
    /// and activate the menu
    /// </summary>
    /// <param name="actor"></param>
    public void Bind(RPGActor actor)
    {
        // Get current actor and transform
        _currentActor = actor;
        _currentActorTransform = actor.transform;

        // Set index for actions (we use index 1 because index 0 is reserved for default attack action)
        int index = 2;

        // Fill the action elements with data
        foreach(ActionInstance action in _currentActor.Actions)
        {
            _actionUIElements[index].SetActionName(action.ActionName);
            _actionUIElements[index].ActivateUIElement();
            index++;
        }

        // Set action menu to actor location
        FollowActorTransform();

        // Activate the action menu
        ActivateActionMenu();

        // Sub to events
        // _currentActor.ActionCoordinator.OnActorActionStart += HideActionMenu;
        _currentActor.HealthCoordinator.OnKO += HideActionMenu;
        
    }

    /// <summary>
    /// Called when either the actor finishes their action or becomes incapacitated. Unbinds from the current actor.
    /// </summary>
    public void Unbind()
    {
        // Hide the menu
        DeactivateActionMenu();

        foreach (PlayerActionUIElement element in _actionUIElements)
        {
            element.DeactivateUIElement();
        }

        // Unbind the actor
        //_currentActor.ActionCoordinator.OnActorActionStart -= HideActionMenu;
        _currentActor.HealthCoordinator.OnKO -= HideActionMenu;

        // Clear cached actor and transform
        _currentActor = null;
        _currentActorTransform = null;
    }

    /// <summary>
    /// If the actor completes their action or is KOed, hide the menu.
    /// </summary>
    /// <param name="actor"></param>
    private void HideActionMenu(RPGActor actor)
    {
        // TODO: Validate if this is necessary, I think the time coordinator should only trigger these events for the current actor, but just in case we can check here.
        if (_currentActor != actor)
        {
            return;
        }

        Unbind();
    }

    /// <summary>
    /// Enables the action menu UI. 
    /// </summary>
    private void ActivateActionMenu()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Disables the action menu UI.
    /// </summary>
    private void DeactivateActionMenu()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Have the UI Element follow the player transform.
    /// </summary>
    private void FixedUpdate()
    {
        FollowActorTransform();
    }

    /// <summary>
    /// Transposese world position to UI screen position and moves UI to actor coordinates.
    /// </summary>
    private void FollowActorTransform()
    {
        // Transpose world position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_currentActorTransform.position);

        // Follow player position
        transform.position = screenPos;
    }


    #region Button Press Events

    /// <summary>
    /// Called when the basic attack button is pressed. Will trigger an event that the battle controller will listen to and execute based on the basic attack action.
    /// </summary>
    public void OnActionChosen(int index)
    {
        // Don't think this should ever be null but just in case.
        if (_currentActor == null)
        {
            Debug.LogError("No actor bound to action menu.");
            return;
        }

        // Trigger event for basic attack action
        OnActionAtIndex?.Invoke(index);
    }

    /// <summary>
    /// Called when the run button is pressed. Will trigger an event that the battle controller will listen to and execute based on the run action.
    /// </summary>
    public void OnRun()
    {

        if (_currentActor == null)
        {
            Debug.LogError("No actor bound to action menu.");
            return;
        }

        OnRunSelected?.Invoke();
    }

    #endregion

    #endregion
}
