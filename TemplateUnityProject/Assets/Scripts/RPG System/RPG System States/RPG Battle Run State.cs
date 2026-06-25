/// <summary>
/// This state handles battle running.
/// 
/// REM-i
/// </summary>
public class RPGBattleRunState : RPGIBattleState
{
    // The controller for the battle, used to perform necessary behaviors and set up the battle to start.
    private readonly RPGBattleController _battleController;

    /// <summary>
    /// Constructor called to pass in battle controller so we can perform the behaviors we need to in this state. 
    /// </summary>
    /// <param name="battleController"></param>
    public RPGBattleRunState(RPGBattleController battleController)
    {
        _battleController = battleController;
    }

    /// <summary>
    /// After startup, we want to enable timers to run for actors and enemies.
    /// </summary>
    public void Enter() 
    { 
        // Turn the timers on for both groups
        _battleController.PlayerTeam.Actors.ForEach(actor => actor.TimeCoordinator.ToggleTimer(true));
        _battleController.EnemyTeam.Actors.ForEach(actor => actor.TimeCoordinator.ToggleTimer(true));
    }

    /// <summary>
    /// While we are running, we need to check for if the player or enemy is ever defeated to end the fight.
    /// </summary>
    public void Tick()
    {
        if (_battleController.PlayerTeam.IsTeamKOed())
        {
            _battleController.ChangeState(new RPGBattleLoseState(_battleController));
        }

        if (_battleController.EnemyTeam.IsTeamKOed())
        {
            _battleController.ChangeState(new RPGBattleWinState(_battleController));
        }
    }

    /// <summary>
    /// On exit, we want to pause the timers for actors and enemies so that time is not progressing when we are not in the run state. This will also be used for other states that pause time, such as the action menu state.
    /// </summary>
    public void Exit() 
    { 
        // Turn the timers off for both groups
        _battleController.PlayerTeam.Actors.ForEach(actor => actor.TimeCoordinator.ToggleTimer(false));
        _battleController.EnemyTeam.Actors.ForEach(actor => actor.TimeCoordinator.ToggleTimer(false));
    }

    

}
