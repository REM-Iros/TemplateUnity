using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// This is the basic RPG Actor class. It will be used as the orchestrator of all character
/// components.
/// 
/// REM-i
/// </summary>
public class RPGActor : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the index for this character. Chosen on runtime by the team controllers.")]
    private int _index;

    [Tooltip("This is the health coordinator for the actor.")]
    [SerializeField, Header("Actor Components")]
    private RPGActorHealthCoordinator _healthCoordinator;

    [Tooltip("This is a get method for the health coordinator.")]
    public RPGActorHealthCoordinator HealthCoordinator => _healthCoordinator;

    [Tooltip("This is the time coordinator for the actor.")]
    [SerializeField]
    private RPGActorTimeCoordinator _timeCoordinator;

    [Tooltip("This is a get method for the time coordinator.")]
    public RPGActorTimeCoordinator TimeCoordinator => _timeCoordinator;

    [Tooltip("This is the scriptable object that will store the character data. It should be filled by the team manager on combat start.")]
    private CharacterStats _characterStats;

    [Tooltip("This is the action menu UI for the actor. It should be initialized by the team manager after other components initialize.")]
    private PlayerActionsUI _actionMenuUI;

    [Tooltip("This is the list of actions that the actor can take. It should be filled by the team manager on combat start.")]
    private List<ActionInstance> _actions;

    [Tooltip("This is a get method for the actions list count.")]
    public int ActionCount => _actions.Count;

    [Tooltip("This is the RPG stats component for the actor.")]
    private Stats _stats;

    [Tooltip("This is the Player Actions UI that will be used to display the actions for the player when ready to attack.")]
    private PlayerActionsUI _playerActionsUI;

    [Tooltip("This is the Image component that will display the actor.")]
    [SerializeField]
    private Image _actorImage;

    #region Events

    // Events for the actor, these will be used to notify the team controller and other components of important information such as when an action starts or ends, or when the actor is KOed.
    public event Action<int> OnActorKO;
    public event Action<int> OnActorActionStart;

    #endregion

    #endregion

    #region Methods

    #region Initialize Methods

    /// <summary>
    /// This initializes the actor with the given components it needs to function. It should be called by
    /// the RPG Party Manager when the combat starts
    /// </summary>
    /// <param name="characterStats"></param>
    public void InitializeActor(int index, CharacterStats characterStats)
    {
        // Check that a character can even instantiate with the given data, if not, log an error and return
        if (!ValidateInitialization(index, characterStats, characterStats.actionData))
        {
            return;
        }

        _index = index;
        
        // Initialize components
        InitializeCharacterStats(characterStats);
        InitializeActionInstancesList(characterStats.actionData);
        InitializeHealthCoordinator();
        InitializeTimeCoordinator();

        // Leaving this here for when animations get added and this needs to be refactored.
        _actorImage.sprite = characterStats.characterFullBodyImage;
    }

    /// <summary>
    /// This method checks that all the necessary components for the RPG Actor are present and valid. 
    /// If any of the checks fail, it logs an error and returns false, preventing the actor from being initialized.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="characterStats"></param>
    /// <param name="actionData"></param>
    /// <returns></returns>
    private bool ValidateInitialization(int index, CharacterStats characterStats, List<ActionData> actionData)
    {
        // Check for index and if it is valid
        if (index < 0)
        {
            Debug.LogError("RPG Actor index cannot be less than 0 and will not work.");
            return false;
        }

        // Check for character stats and if it is present
        if (characterStats == null)
        {
            Debug.LogError("RPG Actor could not find a character stat and will not work.");
            return false;
        }

        // Check for action data and if it is present
        if (actionData == null || actionData.Count == 0)
        {
            Debug.LogError("RPG Actor could not find any action data and will not work.");
            return false;
        }

        // Check for health bar and if it is present
        if (_healthCoordinator == null)
        {
            Debug.LogError("Health Coordinator not found, RPG actor will not work.");
            return false;
        }

        // Check for the time coordinator
        if (_timeCoordinator == null)
        {
            Debug.LogError("Time Coordinator not found, RPG actor will not work.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// This method initializes the character stats for the actor. 
    /// It should only be called by the InitializeActor method, and it sets the character stats.
    /// </summary>
    /// <param name="characterStats"></param>
    private void InitializeCharacterStats(CharacterStats characterStats)
    {
        // Setting stats
        _characterStats = characterStats;
        _stats = characterStats.baseStats;
    }

    /// <summary>
    /// This method initializes the action instances list for the actor. 
    /// It should only be called by the InitializeActor method, and it fills the list with the given action data.
    /// </summary>
    /// <param name="actionData"></param>
    private void InitializeActionInstancesList(List<ActionData> actionData)
    {
        List<ActionInstance> _actions = new List<ActionInstance>();

        // Initialize the actions list and fill it with the action data
        foreach (ActionData data in actionData)
        {
            _actions.Add(new ActionInstance(data));
        }
    }

    /// <summary>
    /// This method initializes the health bar for the actor. It should only be called by the InitializeActor method, 
    /// and it sets the max value of the health bar to the max hp of the character stats.
    /// </summary>
    private void InitializeHealthCoordinator()
    {
        _healthCoordinator.Initialize(_stats.maxHP, _index);
    }

    /// <summary>
    /// This method initializes the time coordinator for the actor. It should only be called by the InitializeActor method, 
    /// and it sets the max time value for the time coordinator to the max time of the character stats.
    /// </summary>
    private void InitializeTimeCoordinator()
    {
        _timeCoordinator.Initialize(_stats.maxTime, _index);
    }

    /// <summary>
    /// Method is called after the actor is initialized to set the reference for the action menu UI. This is necessary for the actor to be able to control the UI and display it when necessary.
    /// </summary>
    /// <param name="inputController"></param>
    public void InitializeActionMenu(PlayerInput inputController)
    {
        _playerActionsUI.Init(inputController);
    }

    #endregion

    #region Action Methods

    /// <summary>
    /// Activates the action menu for the actor.
    /// </summary>
    public void ActivateActionMenu()
    {
        _playerActionsUI.ActivateActionMenu();
    }

    /// <summary>
    /// Get method for returning the action instance name at a given index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetActionNameAtIndex(int index)
    {
        // Exception handling for index out of range, if it is, log an error and return an empty string
        if (index < 0 || index >= _actions.Count)
        {
            Debug.LogError("Index out of range for action instances list.");
            return string.Empty;
        }

        // Return the action name at the given index
        return _actions[index].ActionName;
    }

    /// <summary>
    /// Get method for returning the action damage modifier at a given index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetActionDamageAtIndex(int index)
    {
        // Exception handling for index out of range, if it is, log an error and return 0
        if (index < 0 || index >= _actions.Count)
        {
            Debug.LogError("Index out of range for action instances list.");
            return 0;
        }
        // Return the action damage at the given index
        return _actions[index].DamageModifier;
    }

    /// <summary>
    /// Get method for returning the action effect index at a given index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetActionEffectAtIndex(int index)
    {
        // Exception handling for index out of range, if it is, log an error and return 0
        if (index < 0 || index >= _actions.Count)
        {
            Debug.LogError("Index out of range for action instances list.");
            return 0;
        }
        // Return the action effect at the given index
        return _actions[index].EffectIndex;
    }

    /// <summary>
    /// Completes the current action, and resets the time bar for the actor.
    /// </summary>
    public void FinishAction()
    {
        _timeCoordinator.ResetTimer();

        _playerActionsUI.DeactivateActionMenu();
    }

    /// <summary>
    /// Called when the user's action is interrupted, such as by death, or stagger.
    /// </summary>
    public void InterruptAction()
    {
        _timeCoordinator.DeactivateAndResetTimer();
    }

    #endregion

    /// <summary>
    /// Get method for returning the actor's index.
    /// </summary>
    /// <returns></returns>
    public int GetActorIndex()
    {
        return _index;
    }

    /// <summary>
    /// When the actor is KOed, activate this event and notify the team controller.
    /// </summary>
    private void ActorKOActivateEvent()
    {
        OnActorKO?.Invoke(_index);
    }

    /// <summary>
    /// When the actor is ready to take an action, activate this event and notify the team controller.
    /// </summary>
    private void ActionReady()
    {
        OnActorActionStart?.Invoke(_index);
    }

    /// <summary>
    /// Unsubscribe to the actor events on disable.
    /// </summary>
    private void OnDestroy()
    {
        if (_healthCoordinator != null)
        {
            _healthCoordinator.OnKO -= ActorKOActivateEvent;
            _healthCoordinator.OnKO -= _timeCoordinator.DeactivateAndResetTimer;
        }

        if (_timeCoordinator != null)
        {
            _timeCoordinator.OnCanAct -= ActionReady;
        }
    }

    #endregion
}
