using UnityEngine;
using TMPro;

/// <summary>
/// Simple visual element script that uses the binder to display the actor's name.
/// 
/// REM-i
/// </summary>
public class RPGActorNameText : MonoBehaviour, RPGIUIInterface
{
    #region Vars

    [Tooltip("This is the Text Mesh used for displaying the actor name.")]
    [SerializeField, Header("Actor Name Text")]
    private TextMeshProUGUI _actorNameText;

    #endregion

    #region Methods

    /// <summary>
    /// On bind, we set the actors name text.
    /// </summary>
    /// <param name="actor"></param>
    public void Bind(RPGActor actor)
    {
        _actorNameText.text = actor.ActorName;
    }

    /// <summary>
    /// On unbind, we remove the actors name text (I don't see this being used in game unless for some reason you would keep the
    /// enemy status visible or if you are using object pooling).
    /// </summary>
    public void Unbind()
    {
        _actorNameText.text = string.Empty;
    }

    #endregion
}
