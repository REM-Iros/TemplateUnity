using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the overall battle controller for an RPG System. This will handle instantiation of actors and context for the battle.
/// 
/// REM-i
/// </summary>
public class RPGBattleController : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the current state of the battle.")]
    private RPGIBattleState _currentState;

    [Tooltip("This is the player input controller for the battle.")]
    [SerializeField, Header("Input Controller")]
    private PlayerInputController _playerInputController;

    [Tooltip("This is the public getter for player input controller for state access.")]
    public PlayerInputController PlayerInputController => _playerInputController;

    [Tooltip("This is the input router.")]
    [SerializeField]
    private RPGBattleInputRouter _battleInputRouter;

    [Tooltip("This is the public getter for battle input router for state access.")]
    public RPGBattleInputRouter BattleInputRouter => _battleInputRouter;

    [Tooltip("This is the player team controller.")]
    [SerializeField, Header("Actor Team Controllers")]
    private RPGTeamManager _playerTeamController;

    [Tooltip("This is the public getter for player team controller for state access.")]
    public RPGTeamManager PlayerTeam => _playerTeamController;

    // This is hardcoded team data for testing purposes.
    public List<CharacterStats> playerTeamData;

    [Tooltip("This is the enemy team controller.")]
    [SerializeField]
    private RPGTeamManager _enemyTeamController;

    [Tooltip("This is the public getter for enemy team controller for state access.")]
    public RPGTeamManager EnemyTeam => _enemyTeamController;

    // This is hardcoded team data for testing purposes.
    public List<CharacterStats> enemyTeamData;

    [Tooltip("This is the UI Manager for the battle.")]
    [SerializeField, Header("UI Manager")]
    private RPGBattleUIManager _battleUIManager;

    [Tooltip("This is the public getter for battle UI manager for state access.")]
    public RPGBattleUIManager UIManager => _battleUIManager;

    [Tooltip("This is the time system for the battle.")]
    [SerializeField, Header("Time System")]
    private RPGTimeSystemManager _timeSystemManager;

    [Tooltip("This is the public getter for time system manager for state access.")]
    public RPGTimeSystemManager TimeSystem => _timeSystemManager;

    [Tooltip("This is the action manager for the battle.")]
    private RPGActionManager _actionManager;

    [Tooltip("This is the public getter for action manager.")]
    public RPGActionManager ActionManager => _actionManager;

    [Tooltip("This is the targeting manager for the battle.")]
    private RPGTargetingManager _targetingManager;

    [Tooltip("This is the public getter for targeting manager.")]
    public RPGTargetingManager TargetingManager => _targetingManager;

    [Tooltip("This is the battle context stored for the battle.")]
    private RPGBattleContext _context;

    #endregion

    #region Methods

    /// <summary>
    /// On startup, load up the player and enemy teams by calling the team controllers to initialize with the necessary data.
    /// </summary>
    private void Start()
    {
        // On startup, we need to initialize the player and enemy teams by calling the team controllers to initialize with the necessary data.
        // This will likely come from a global manager of some sort that holds the team data for the battle, but for now we can just hardcode some data in the inspector for testing purposes.
        //_playerTeamController.InitializeActorTeam(ServiceLocator.Get<TeamManager>().TeamData);
        //_enemyTeamController.InitializeActorTeam(ServiceLocator.Get<TeamManager>().EnemyTeamData);
        _context = new RPGBattleContext();

        // Go into the startup state to set up the battle.
        ChangeState(new RPGBattleStartState(this));

        // Add the action menu input controller to the player team controller so that it can listen for input when the action menu is active.
        //_playerTeamController.InitializeActionMenuInputController(_playerInputController);
    }

    /// <summary>
    /// Change state called when the system needs to change to a different battle state.
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(RPGIBattleState newState)
    {
        // Don't change states if we are using the same state again.
        if (_currentState == newState)
        {
            return;
        }

        // Exit the current state if it exists, then enter the new state and set it as the current state.
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    /// <summary>
    /// Called when the time system notifies the battle controller that an actor is ready to take an action.
    /// </summary>
    /// <param name="actor"></param>
    public void HandlePlayerActorReady(RPGActor actor)
    {
        // Notify the UI manager that an actor is ready to take an action so that it can update the UI accordingly.
        _battleUIManager.BindActionMenu(actor);
    }

    /// <summary>
    /// This is called by the action menu ui buttons directly. It pulls the current actors action by index and
    /// sends the targeting strategy of the action to the targeting manager.
    /// </summary>
    /// <param name="index"></param>
    public void OnActionSelection(int index)
    {
        // TODO: Move this method to the action selection
        //_currActor.ActionCoordinator.GetActionAtIndex(index).Data.actionTargetStrategy.GetValidTargets(GenerateBattleContext(), _currActor);
    }

    /// <summary>
    /// This is a helper method that generates the battle context for handling targeting
    /// </summary>
    /// <returns></returns>
    public RPGBattleContext GenerateBattleContext()
    {
        // Get the player actors and the enemy actors.
        _context.playerActors = _playerTeamController.Actors;
        _context.enemyActors = _enemyTeamController.Actors;

        return _context;
    }

    #endregion
}
