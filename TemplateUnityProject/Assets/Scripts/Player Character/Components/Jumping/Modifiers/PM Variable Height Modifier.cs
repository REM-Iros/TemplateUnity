using UnityEngine;

/// <summary>
/// This is a modifier for jumping that allows the player to hold
/// the jump button to gain more height.
/// 
/// REM-i
/// </summary>
public class PMVariableHeightModifier : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the force that will be applied over time as the jump is held.")]
    [SerializeField, Header("Variable Height Vars")]
    private float _jumpHeldForce;

    [Tooltip("This is the length of time that the jump can be held for more heigth")]
    [SerializeField]
    private float _maxHoldTime;

    // Getter for max hold time
    public float MaxHoldTime { get { return _maxHoldTime; } }

    #endregion

    #region Methods

    #region Jumping Methods

    /// <summary>
    /// Get the velocity request for this jump.
    /// </summary>
    /// <returns></returns>
    public VelocityRequest GetVelocityRequest()
    {
        return new VelocityRequest(new Vector2(0, _jumpHeldForce), VelocityPriority.Normal, "VariableHeight");
    }

    #endregion

    #endregion
}
