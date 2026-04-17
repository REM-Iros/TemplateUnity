using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the overall battle controller for an RPG System. This will handle instantiation of actors and context for the battle.
/// 
/// REM-i
/// </summary>
public class BattleController : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the player input controller for the battle.")]
    [SerializeField, Header("Input Controller")]
    private PlayerInputController _playerInputController;

    [Tooltip("This is the input router.")]
    private RPGBattleInputRouter _battleInputRouter;

    [Tooltip("This is the player team controller.")]
    [SerializeField, Header("Actor Team Controllers")]
    private TeamController _playerTeamController;

    // This is hardcoded team data for testing purposes.
    public List<CharacterStats> playerTeamData;

    [Tooltip("This is the enemy team controller.")]
    [SerializeField]
    private TeamController _enemyTeamController;

    // This is hardcoded team data for testing purposes.
    public List<CharacterStats> enemyTeamData;

    [Tooltip("This is the UI Manager for the battle.")]
    [SerializeField, Header("UI Manager")]
    private RPGBattleUIManager _battleUIManager;

    [Tooltip("This is the time system for the battle.")]
    [SerializeField, Header("Time System")]
    private RPGTimeSystemCoordinator _timeSystemCoordinator;

    [Tooltip("This is the current state the battle is in.")]
    private RPGBattleState _battleState;

    #endregion

    #region Methods

    /// <summary>
    /// On startup, load up the player and enemy teams by calling the team controllers to initialize with the necessary data.
    /// </summary>
    private void Start()
    {
        //_playerTeamController.InitializeActorTeam(ServiceLocator.Get<TeamManager>().TeamData);
        //_enemyTeamController.InitializeActorTeam(ServiceLocator.Get<TeamManager>().EnemyTeamData);

        // Set the battle state to startup to begin with.
        _battleState = RPGBattleState.Startup;

        // Set up the input router
        _battleInputRouter = GetComponent<RPGBattleInputRouter>();
        _battleInputRouter.Bind(_playerInputController);

        // Hacked in team data for testing purposes until I get the global manager set up.
        _playerTeamController.InitializeActorTeam(playerTeamData);
        _enemyTeamController.InitializeActorTeam(enemyTeamData);

        InitializeBattleTimeSystem();
        InitializeBattleUIManager();
        

        // Add the action menu input controller to the player team controller so that it can listen for input when the action menu is active.
        //_playerTeamController.InitializeActionMenuInputController(_playerInputController);
    }

    /// <summary>
    /// Called on startup to get the battle time system set up with events from the actors so that it can coordinate actions between them.
    /// </summary>
    private void InitializeBattleTimeSystem()
    {
        // Subscribe time system to actors
        foreach (RPGActor actor in _playerTeamController.Actors)
        {
            _timeSystemCoordinator.SubscribeToActorEvents(actor, true);
        }

        foreach (RPGActor actor in _enemyTeamController.Actors)
        {
            _timeSystemCoordinator.SubscribeToActorEvents(actor, false);
        }
    }

    /// <summary>
    /// Called on startup to get the battle UI manager set up with the actors and necessary information to display the UI correctly.
    /// </summary>
    private void InitializeBattleUIManager()
    {
        // Initialize the ui manager with actors
        foreach (RPGActor actor in _playerTeamController.Actors)
        {
            _battleUIManager.RegisterActor(actor, true);
        }

        foreach (RPGActor actor in _enemyTeamController.Actors)
        {
            _battleUIManager.RegisterActor(actor, false);
        }

        _battleUIManager.InitializeActionMenu();

        // Subscribe to time system for proper event handling
        _timeSystemCoordinator.NotifyPlayerActorUI += _battleUIManager.BindActionMenu;
    }

    #endregion
}
