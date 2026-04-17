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

    [Tooltip("This is the scriptable object that will store the character data. It should be filled by the team manager on combat start.")]
    private CharacterStats _characterStats;

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

    [Tooltip("This is the action coordinator for the actor.")]
    [SerializeField]
    private RPGActorActionCoordinator _actionCoordinator;

    [Tooltip("This is a get method for the action coordinator.")]
    public RPGActorActionCoordinator ActionCoordinator => _actionCoordinator;

    [Tooltip("This is the list of actions that the actor can take. It should be filled by the team manager on combat start.")]
    private List<ActionInstance> _actions;

    [Tooltip("This is a public getter for actions.")]
    public List<ActionInstance> Actions => _actions;

    [Tooltip("This is a get method for the actions list count.")]
    public int ActionCount => _actions.Count;

    [Tooltip("This is the RPG stats component for the actor.")]
    private Stats _stats;

    [Tooltip("This is the Image component that will display the actor.")]
    [SerializeField]
    private Image _actorImage;

    #endregion

    #region Methods

    #region Initialize Methods

    /// <summary>
    /// This initializes the actor with the given components it needs to function. It should be called by
    /// the RPG Party Manager when the combat starts
    /// </summary>
    /// <param name="characterStats"></param>
    public void InitializeActor(CharacterStats characterStats)
    {
        // Check that a character can even instantiate with the given data, if not, log an error and return
        if (!ValidateInitialization(characterStats, characterStats.actionData))
        {
            return;
        }
        
        // Initialize components
        InitializeCharacterStats(characterStats);
        InitializeHealthCoordinator();
        InitializeTimeCoordinator();
        InitializeActionCoordinator();

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
    private bool ValidateInitialization(CharacterStats characterStats, List<ActionData> actionData)
    {
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

        // Check for the action coordinator
        if (_actionCoordinator == null)
        {
            Debug.LogError("Action Coordinator not found, RPG actor will not work.");
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
    /// This method initializes the health bar for the actor. It should only be called by the InitializeActor method, 
    /// and it sets the max value of the health bar to the max hp of the character stats.
    /// </summary>
    private void InitializeHealthCoordinator()
    {
        //TODO: This needs to be changed to current hp eventually but will work for now.
        _healthCoordinator.Initialize(this, _stats.maxHP, _stats.maxHP);
    }

    /// <summary>
    /// This method initializes the time coordinator for the actor. It should only be called by the InitializeActor method, 
    /// and it sets the max time value for the time coordinator to the max time of the character stats.
    /// </summary>
    private void InitializeTimeCoordinator()
    {
        _timeCoordinator.Initialize(this, _stats.maxTime);

        // Sub the ko event to the time coordinator
        _healthCoordinator.OnKO += _timeCoordinator.HandleActorKO;
    }

    /// <summary>
    /// This method initializes the action instances list for the actor. 
    /// It should only be called by the InitializeActor method, and it fills the list with the given action data.
    /// </summary>
    /// <param name="actionData"></param>
    private void InitializeActionCoordinator()
    {
        // Initialize the action coordinator
        _actionCoordinator.InitializeActionCoordinator();

        // Fill the action instances list with the given action data
        foreach (ActionData data in _characterStats.actionData)
        {
            _actionCoordinator.AddAction(data);
        }
    }

    #endregion

    #region Action Methods

    /// <summary>
    /// Completes the current action, and resets the time bar for the actor.
    /// </summary>
    public void FinishAction()
    {
        _timeCoordinator.ResetTimer();

        //TODO: This should go to the action coordinator for the actor. The event should be called by the action coordinator when an action finishes, not the actor itself. This is just a placeholder for now.
        //OnActorActionFinish?.Invoke(this);
    }

    #endregion

    #endregion
}
