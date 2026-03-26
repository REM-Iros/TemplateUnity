using UnityEngine;

/// <summary>
/// This is the parent script for all of the movement scripts. All versions of this
/// should only need to implement moving the player and should include anything that
/// it might need that isn't included in the current script. Might need to add in a dash
/// script as well, but I'm not sure how it would handle the behaviors. It might not
/// even be worth it to be honest.
/// 
/// REM-i
/// </summary>
public class MovementComponentParent : MonoBehaviour, IMovementInterface
{
    #region Vars

    [Tooltip("This is the basic speed variable.")]
    [SerializeField, Header("Movement Vars")]
    protected float _speed;

    #endregion

    #region Methods

    /// <summary>
    /// This is the method that will be called to move the player
    /// </summary>
    public virtual VelocityRequest Move(Vector2 input)
    {
        // Needs to be implemented properly in the components
        return VelocityRequest.None;
    }

    #endregion
}
