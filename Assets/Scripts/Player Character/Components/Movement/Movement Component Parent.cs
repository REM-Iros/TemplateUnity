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
public class MovementComponentParent : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the rigidbody we need for movement.")]
    [SerializeField]
    protected Rigidbody2D _rb2d;

    #region Movement Vars

    [Tooltip("This is the basic speed variable.")]
    [SerializeField, Header("Movement Vars")]
    protected float _speed;

    // This is the 2D vector that movement will be passed into
    protected Vector2 _moveVector;

    [Space(5)]

    #endregion

    #region Dash Vars

    [Tooltip("This is a bool that will be checked whenever dashing is looked at.")]
    [SerializeField, Header("Dashing Vars")]
    private bool _enableDashing;
    public bool IsDashingEnabled { get { return _enableDashing; } }

    [Tooltip("This is the basic speed variable.")]
    [SerializeField]
    protected float _dashSpeed;

    // This is the boolean that will be used to check if the player is dashing
    protected bool _isDashing;

    #endregion

    #endregion

    #region Methods

    #region Startup Methods

    /// <summary>
    /// On startup, we need to initialize
    /// </summary>
    private void Awake()
    {
        InitDependencies();
    }

    /// <summary>
    /// Finds the components necessary for this script to operate
    /// </summary>
    private void InitDependencies()
    {
        // Check for player input and rigidbody
        if (_rb2d == null)
        {
            _rb2d = GetComponent<Rigidbody2D>();
            Debug.LogError("No Rigidbody2D found, script will NOT work!");
            return;
        }
    }

    #endregion

    #region Event Methods

    /*
     * Attach these scripts to the input script from the parent
     */
    public void GetMovementVector(Vector2 MoveVector)
    {
        _moveVector = MoveVector;
    }

    public void GetDashPressed()
    {
        _isDashing = true;
    }

    public void GetDashReleased()
    {
        _isDashing = false;
    }

    #endregion

    #region Movement Methods

    /// <summary>
    /// This is the method that will be called to move the player
    /// </summary>
    public virtual void MovePlayer()
    {
        // Only call this if the rigidbody is not null
        if (_rb2d == null)
        {
            Debug.LogError("Rigidbody2D is null");
            return;
        }

        // Implement move player for this script at this point
    }

    #endregion

    #endregion
}
