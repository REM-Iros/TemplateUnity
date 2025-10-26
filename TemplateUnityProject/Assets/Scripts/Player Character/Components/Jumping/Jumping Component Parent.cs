using UnityEngine;

/// <summary>
/// This is the parent script for all of the jumping scripts. All versions of this
/// should only need to implement jumping the player and should include anything that
/// it might need that isn't included in the current script. Components will also be checked
/// for to enable more features for the script.
/// 
/// NOTE: Jumping is not compatible for top down movement... you'll need to implement a different
/// script for that.
/// 
/// REM-i
/// </summary>
public class JumpingComponentParent : MonoBehaviour, IJumpInterface
{
    #region Vars

    [Tooltip("This is the jump force for the player.")]
    [SerializeField, Header("Jump Vars")]
    protected float _jumpForce;

    #endregion

    #region Methods

    #region Jumping Methods

    /// <summary>
    /// This method is used to determine player eligibility for jumping. If they can't
    /// jump, then it will be passed over for the next jump in the list.
    /// </summary>
    /// <returns></returns>
    public bool CanJump()
    {
        return CheckForCanJump();
    }

    /// <summary>
    /// This is the method implemented by each individual script
    /// that will determine whether the player can jump or not.
    /// </summary>
    /// <returns></returns>
    protected virtual bool CheckForCanJump()
    {
        // Override this for the behavior you want.
        return true;
    }

    /// <summary>
    /// This is the method that will be called to apply force to make
    /// the player jump. Will need to be implemented in different variants.
    /// </summary>
    public virtual VelocityRequest Jump()
    {
        return VelocityRequest.None;
    }

    /// <summary>
    /// This method is used to determine eligibility for a persistant force after the initial
    /// jump.
    /// </summary>
    /// <returns></returns>
    public bool IsPersistantForce()
    {
        return CheckForPersistantForce();
    }

    /// <summary>
    /// This is the method that will be overwritten to check for whether
    /// a persistant force should be applied.
    /// </summary>
    /// <returns></returns>
    protected virtual bool CheckForPersistantForce()
    {
        // Override this for the behavior you want.
        return true;
    }

    /// <summary>
    /// This is the method that will be called to cue up a force to use
    /// for persistant jump forces.
    /// </summary>
    /// <returns></returns>
    public virtual TimedVelocityRequest PersistantJump()
    {
        // Override this for the behavior you want.
        return new TimedVelocityRequest(VelocityRequest.None, 0f);
    }

    /// <summary>
    /// This method is checked if we need to override for a duration.
    /// </summary>
    /// <returns></returns>
    public virtual bool HasOverride()
    {
        return false;
    }

    /// <summary>
    /// This is the float that we will need to return if we are overriding.
    /// </summary>
    /// <returns></returns>
    public virtual float GetOverrideDuration()
    {
        return 0f;
    }

    #endregion

    #endregion
}
