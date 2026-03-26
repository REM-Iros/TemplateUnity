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

    #endregion

    #region Methods

    /// <summary>
    /// On startup, load up the player and enemy teams by calling the team controllers to initialize with the necessary data.
    /// </summary>
    private void Start()
    {
        //_playerTeamController.InitializeActorTeam(ServiceLocator.Get<TeamManager>().TeamData);
        //_enemyTeamController.InitializeActorTeam(ServiceLocator.Get<TeamManager>().EnemyTeamData);

        // Hacked in team data for testing purposes until I get the global manager set up.
        _playerTeamController.InitializeActorTeam(playerTeamData);
        _enemyTeamController.InitializeActorTeam(enemyTeamData);

        // Add the action menu input controller to the player team controller so that it can listen for input when the action menu is active.
        _playerTeamController.InitializeActionMenuInputController(_playerInputController.PlayerInput);
    }

    #endregion
}
