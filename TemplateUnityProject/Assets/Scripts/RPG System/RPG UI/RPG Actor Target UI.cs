using UnityEngine;

/// <summary>
/// This script functionally just determines if the actor is targetable and updates the UI appropriately.
/// 
/// REM-i
/// </summary>
public class RPGActorTargetUI : MonoBehaviour, RPGIUIInterface
{
    #region Vars

    [Tooltip("This var determines when the actor itself is targetable. It should only verify if the actor is alive or not.")]
    private bool isTargetable;

    #endregion

    #region Methods

    public void Bind(RPGActor actor)
    {

    }

    public void Unbind()
    {

    }

    public bool IsTargetable()
    {
        return false;
    }

    #endregion
}
