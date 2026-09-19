using UnityEngine;

/// <summary>
/// This is the base class for all RPG Actors. It will contain all of the components needed for an RPG Actor to function in the game.
/// 
/// REM-i
/// </summary>
public class RPGActor : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the scriptable object that will store the actor data. It should be filled by the team manager on combat start.")]
    private RPGActorStats _actorStats;

    [Tooltip("This is the getter method for the name of the actor.")]
    public string ActorName => _actorStats.characterName;

    [Tooltip("This is the getter method for the headshot of the actor.")]
    public Sprite ActorHeadshot => _actorStats.characterHeadshot;

    [Tooltip("This is the Sprite Renderer component that will display the actor.")]
    [SerializeField]
    private SpriteRenderer _actorImage;

    #endregion

    #region Methods

    /// <summary>
    /// This is called when an actor is created by the team manager. It will initialize the actor with the given stats.
    /// </summary>
    /// <param name="actorStats"></param>
    public void InitializeActor(RPGActorStats actorStats)
    {
        // Set the actor stats to the given stats
        _actorStats = actorStats;

        // Set the actor image
        _actorImage.sprite = _actorStats.characterFullBodyImage;
    }

    #endregion
}
