using UnityEngine;

/// <summary>
/// This is the overall battle controller for an RPG System. This will handle instantiation of actors and context for the battle.
/// 
/// REM-i
/// </summary>
public class BattleController : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the player team controller.")]
    [SerializeField, Header("Actor Team Controllers")]
    private TeamController _playerTeamController;

    [Tooltip("This is the enemy team controller.")]
    [SerializeField]
    private TeamController _enemyTeamController;

    #endregion

    #region Methods

    /// <summary>
    /// On startup, load up the player and enemy teams by calling the team controllers to initialize with the necessary data.
    /// </summary>
    private void Awake()
    {
        //_playerTeamController.InitializeActorTeam(ServiceLocator.Get<TeamManager>().TeamData);
        //_enemyTeamController.InitializeActorTeam(ServiceLocator.Get<TeamManager>().EnemyTeamData);
    }

    #endregion
}
