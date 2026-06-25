/// <summary>
/// This is the state for when the player loses a battle. It is currently a placeholder for future implementation.
/// 
/// REM-i
/// </summary>
public class RPGBattleLoseState : RPGIBattleState
{
    // The controller for the battle, used to perform necessary behaviors and set up the battle to start.
    private readonly RPGBattleController _battleController;

    /// <summary>
    /// Constructor called to pass in battle controller so we can perform the behaviors we need to in this state. 
    /// </summary>
    /// <param name="battleController"></param>
    public RPGBattleLoseState(RPGBattleController battleController)
    {
        _battleController = battleController;
    }

    /// <summary>
    /// On startup, remove the time system subscription to the action menu.
    /// </summary>
    public void Enter()
    {
        // Unsubscribe from time system events to avoid potential memory leaks.
        _battleController.TimeSystem.OnPlayerActorTurnStarted -= _battleController.HandlePlayerActorReady;
    }

    /// <summary>
    /// Don't need either of these states currently.
    /// </summary>
    public void Exit() { }
    public void Tick() { }
}
