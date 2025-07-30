using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Since we will likely have multiple movement components that will all want to affect the
/// velocity of the rigidbody, this script will be used to control the velocity. It prevents certain
/// components from completely overwriting things, allowing for more complex movement systems and blending
/// of movement styles. We will also have an override system in place to allow for certain components to run critical
/// methods that need to run before others, such as wall jumps or dashes.
/// 
/// REM-i
/// </summary>
public class PMVelocityController : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the rigidbody we will be controlling the velocity of.")]
    [SerializeField, Header("Rigidbody")]
    private Rigidbody2D _rb2d;

    // This is the list of velocity requests we have to process
    private List<VelocityRequest> _velocityRequests;



    #endregion

    #region Methods

    /// <summary>
    /// On awake, we want to check if we have a rigidbody to work with, otherwise, the script won't run.
    /// </summary>
    private void Awake()
    {
        InitDependencies();
    }

    /// <summary>
    /// Checks for rb, and then initializes the velocity request list
    /// </summary>
    private void InitDependencies()
    {
        // Check for rigidbody
        if (_rb2d == null)
        {
            Debug.LogError("No Rigidbody2D found, script will not work.");
        }

        _velocityRequests = new List<VelocityRequest>();
    }

    #region Velocity Methods

    /// <summary>
    /// This is used by movement managers to submit velocity requests
    /// </summary>
    /// <param name="velocityRequest"></param>
    public void SubmitVelocityRequest(VelocityRequest velocityRequest)
    {
        _velocityRequests.Add(velocityRequest);
    }

    /// <summary>
    /// FixedUpdate is called at a fixed interval and is used for physics calculations.
    /// </summary>
    private void FixedUpdate()
    {
        HandleVelocityApplication();
    }

    /// <summary>
    /// Handles the application of the velocity to the rigidbody.
    /// </summary>
    private void HandleVelocityApplication()
    {
        // Only run if we have a rigidbody
        if (_rb2d == null)
        {
            Debug.LogError("No Rigidbody2D found, script will not work.");
            return;
        }

        // Check if we have any velocity requests.
        if (_velocityRequests.Count == 0)
        {
            return;
        }

        // We want to get the highest priority we need in the list
        VelocityPriority highestPriority = _velocityRequests.Max(r => r.priority);

        // We need to get every single request at the highest priority
        var topPriorityRequests = _velocityRequests.Where(r => r.priority == highestPriority).ToList();

        // Start with an initial velocity of zero
        Vector2 totalVelocity = Vector2.zero;

        // Get each value added into the total
        foreach (var request in topPriorityRequests)
        {
            totalVelocity += request.vector;

            // Reset the y-velocity when a jump occurs. This is just a personal preference
            if (request.tag == "Jump")
            {
                _rb2d.linearVelocityY = 0;
            }
        }

        // Set the velocity
        ApplyVelocity(totalVelocity);
        _velocityRequests.Clear();
    }

    /// <summary>
    /// This is the direct applicator of the velocity after it's been calculated.
    /// </summary>
    /// <param name="velocity"></param>
    private void ApplyVelocity(Vector2 velocity)
    {
        // Always apply the x velocity
        _rb2d.linearVelocityX = velocity.x;

        // If we have a velocity of 0 and the gravity scale isn't 0, we set it.
        if (velocity.y == 0 && _rb2d.gravityScale != 0)
        {
            return;
        }

        _rb2d.linearVelocityY = velocity.y;
    }

    #endregion

    #endregion
}
