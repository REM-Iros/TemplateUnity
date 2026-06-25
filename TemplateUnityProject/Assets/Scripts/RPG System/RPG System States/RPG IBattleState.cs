/// <summary>
/// This is the interface script for all the battle states in the RPG Battle System.
/// 
/// REM-i
/// </summary>
public interface RPGIBattleState
{
    /// <summary>
    /// Battle state enter method. Called by Battle State Machine when entering this state.
    /// </summary>
    void Enter();

    /// <summary>
    /// Battle state exit method. Called by Battle State Machine when exiting this state.
    /// </summary>
    void Exit();

    /// <summary>
    /// Battle state tick method. Called by Battle State Machine every frame while in this state.
    /// </summary>
    void Tick();
}
