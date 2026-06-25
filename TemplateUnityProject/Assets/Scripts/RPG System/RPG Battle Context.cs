using System.Collections.Generic;
using System.Linq;

/// <summary>
/// This class is used to store the context that needs to be passed around when an action is taken in game. 
/// 
/// REM-i
/// </summary>
public class RPGBattleContext
{
    public List<RPGActor> playerActors;
    public List<RPGActor> enemyActors;

    /// <summary>
    /// Gets all actors and returns them in a list.
    /// </summary>
    /// <returns></returns>
    public List<RPGActor> GetAllActors()
    {
        return playerActors.Concat(enemyActors).ToList();
    }

    /// <summary>
    /// Gets the enemies of the current actor
    /// </summary>
    /// <param name="currentActor"></param>
    /// <returns></returns>
    public List<RPGActor> GetEnemiesOf(RPGActor currentActor)
    {
        if (playerActors.Contains(currentActor))
        {
            return enemyActors;
        }

        return playerActors;
    }

    /// <summary>
    /// Gets the allies of the current actor
    /// </summary>
    /// <param name="currentActor"></param>
    /// <returns></returns>
    public List<RPGActor> GetAlliesOf(RPGActor currentActor)
    {
        if (enemyActors.Contains(currentActor))
        {
            return enemyActors;
        }

        return playerActors;
    }
}
