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

    [Tooltip("This is the rigidbody we need for movement.")]
    [SerializeField]
    protected Rigidbody2D _rb2d;

    #region Movement Vars

    [Tooltip("This is the basic speed variable.")]
    [SerializeField, Header("Movement Vars")]
    protected float _speed;

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

    #region Movement Methods

    /// <summary>
    /// This is the method that will be called to move the player
    /// </summary>
    public virtual void Move(Vector2 input)
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
