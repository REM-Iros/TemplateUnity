using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a simple binder script that gets a characters headshot from the actor it is bound to.
/// 
/// REM-i
/// </summary>
public class RPGActorHeadshotUI : MonoBehaviour, RPGIUIInterface
{
    #region Vars

    [Tooltip("This is the image component used to get the actor's headshot.")]
    [SerializeField, Header("Actor Headshot Image")]
    private Image _actorHeadshot;

    #endregion

    #region Methods

    /// <summary>
    /// Binds the RPG Actor to the this and gets the headshot.
    /// </summary>
    /// <param name="actor"></param>
    public void Bind(RPGActor actor)
    {
        _actorHeadshot.sprite = actor.ActorHeadshot;
    }

    /// <summary>
    /// Unbinds the headshot (probably not going to be used unless object pooling)
    /// </summary>
    public void Unbind()
    {
        _actorHeadshot.sprite = null;
    }

    #endregion
}
