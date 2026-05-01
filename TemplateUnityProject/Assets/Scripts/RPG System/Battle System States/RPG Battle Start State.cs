/// <summary>
/// Battle Start interface used to set up the battle and initialize necessary data for the battle to start. This will be the first state of the battle when it starts.
/// 
/// REM-i
/// </summary>
public class RPGBattleStartState : RPGIBattleState
{
    // The controller for the battle, used to perform necessary behaviors and set up the battle to start.
    private readonly BattleController _battleController;

    /// <summary>
    /// Constructor called to pass in battle controller so we can perform the behaviors we need to in this state. 
    /// This will be used to initialize the battle and set up the necessary data for the battle to start.
    /// </summary>
    /// <param name="battleController"></param>
    public RPGBattleStartState(BattleController battleController)
    {
        _battleController = battleController;
    }

    /// <summary>
    /// On startup, we need to initialize the teams and controllers.
    /// </summary>
    public void Enter()
    {
        // This is here for future implementation reference when I set up the global manager to hold the team data for the battle. For now, we can just hardcode some data in the inspector for testing purposes.
        //_battleController.PlayerTeam.InitializeActorTeam(ServiceLocator.Get<TeamManager>().TeamData);
        //_battleController.EnemyTeam.InitializeActorTeam(ServiceLocator.Get<TeamManager>().EnemyTeamData);

        // Hacked in team data for testing purposes until I get the global manager set up.
        _battleController.PlayerTeam.InitializeActorTeam(_battleController.playerTeamData);
        _battleController.EnemyTeam.InitializeActorTeam(_battleController.enemyTeamData);

        // Set up the input router
        _battleController.BattleInputRouter.Bind(_battleController.PlayerInputController);

        // Set up the time system for the battle
        _battleController.TimeSystem.InitializeQueues();

        // Subscribe time system and ui system to actors
        foreach (RPGActor actor in _battleController.PlayerTeam.Actors)
        {
            _battleController.TimeSystem.SubscribeToActorEvents(actor, true);
            _battleController.UIManager.RegisterActor(actor, true);
        }

        foreach (RPGActor actor in _battleController.EnemyTeam.Actors)
        {
            _battleController.TimeSystem.SubscribeToActorEvents(actor, false);
            _battleController.UIManager.RegisterActor(actor, false);
        }

        // Initialize the action menu and subscribe it to the time system so it can update when the player actor changes.
        _battleController.UIManager.InitializeActionMenu();
        _battleController.TimeSystem.OnPlayerActorTurnStarted += _battleController.HandlePlayerActorReady;

        // TODO: Set up missing components

        // Move into the run state
        _battleController.ChangeState(new RPGBattleRunState(_battleController));
    }

    /// <summary>
    /// Don't need either of these states currently.
    /// </summary>
    public void Exit() { }
    public void Tick() { }
}
