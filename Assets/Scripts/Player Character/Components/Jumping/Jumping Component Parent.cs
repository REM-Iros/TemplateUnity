using Unity.VisualScripting;
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

    [Tooltip("This is the rigidbody we need for movement.")]
    [SerializeField]
    protected Rigidbody2D _rb2d;

    [Tooltip("This is the jump force for the player.")]
    [SerializeField, Header("Jump Vars")]
    protected float _jumpForce;

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

    #region Jumping Methods

    /// <summary>
    /// This method is used to determine player eligibility for jumping. If they can't
    /// jump, then it will be passed over for the next jump in the list.
    /// </summary>
    /// <returns></returns>
    public bool CanJump()
    {
        // Check for player input and rigidbody
        if (_rb2d == null)
        {
            Debug.LogError("No Rigidbody2D found, jump being skipped");
            return false;
        }

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
    public void Jump()
    {
        ApplyJumpForce();
    }

    /// <summary>
    /// This is the script the children variants will inherit and override
    /// to provide functionality.
    /// </summary>
    protected virtual void ApplyJumpForce()
    {
        // Implement functionality in child scripts here
    }

    #endregion

    #endregion
}
