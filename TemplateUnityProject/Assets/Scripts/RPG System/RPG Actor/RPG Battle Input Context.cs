/// <summary>
/// This is used by the battle input router to determine when and what commands a player can input during battle.
/// </summary>
public enum RPGBattleInputContext
{
    None,
    PlayerTurn,
    MenuNavigation,
    Paused
}
