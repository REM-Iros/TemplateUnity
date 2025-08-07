using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Jump manager acts as the intermediary between the player controller and all
/// of the possible jump components so that multiple jump types can be implemented
/// at once. It handles jumps via priority, so the order they are attached is extremely
/// important.
/// 
/// REM-i
/// </summary>
public class PMJumpManager : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the list of jumps you want to be able to use. Order matters!")]
    [SerializeField, Header("Jump Components")]
    private List<InterfaceWrapper<IJumpInterface>> _jumpComponentList;

    [Tooltip("This is the list of jumps that need to sub to the button being released.")]
    [SerializeField, Header("Listener Components")]
    private List<InterfaceWrapper<IJumpReleaseListener>> _jumpReleaseListenerList;

    [Tooltip("This is the velocity manager we want to submit to.")]
    [SerializeField, Header("Velocity Manager")]
    private PMVelocityController _velocityController;

    // This is a list of timed velocity requests that get passed into the velocity manager.
    private List<TimedVelocityRequest> _requestList;

    // This bool is for the freeze movement functionality
    private bool _freezeMovement = false;

    #endregion

    #region Methods

    /// <summary>
    /// On awake, we want to check if we even have a list to work with, otherwise,
    /// we need to throw an error because this script either shouldn't be here, or needs
    /// to have the list of jumps applied. (Editor application should be the standard
    /// because otherwise the order doesn't really work).
    /// </summary>
    private void Awake()
    {
        // If our jump component list is null, we need to throw an error
        if (_jumpComponentList == null)
        {
            Debug.LogError("Jump Manager attached but no jump components, player will not be able to jump.");
        }

        _requestList = new List<TimedVelocityRequest>();
    }

    #region Event Methods

    /*
     * Attach these scripts to the input script from the parent
     */
    public void GetJumpPressed()
    {
        ExecuteJumpList();
    }

    public void GetJumpReleased()
    {
        NotifyJumpReleased();
    }

    #endregion

    #region Jump Methods

    /// <summary>
    /// This script will handle the execution of jumps, running through each
    /// script to check if it can jump and then allowing it to jump.
    /// Priority should be: Regular Jump, Wall Jump, then Multi Jump or Double Jump
    /// </summary>
    private void ExecuteJumpList()
    {
        // Freeze the movement if we are frozen
        if (_freezeMovement)
        {
            return;
        }

        // Check if we even have jumps to be able to do
        if (_jumpComponentList == null)
        {
            Debug.LogError("No Jumping components found");
            return;
        }

        // Run through each jump in the list until you have one whose conditions is met.
        foreach (var jump in _jumpComponentList)
        {
            if (jump.Value.CanJump())
            {
                _velocityController.SubmitVelocityRequest(jump.Value.Jump());

                if (jump.Value.IsPersistantForce())
                {
                    _requestList.Add(jump.Value.PersistantJump());
                }

                if (jump.Value.HasOverride())
                {
                    _velocityController.ActivateOverride(jump.Value.GetOverrideDuration());
                }

                break;
            }
        }
    }

    /// <summary>
    /// This script is really for notifying any type of jump (hover, variable height, etc)
    /// that the button has been released and to stop behaviors if the player didn't expend
    /// the full time.
    /// </summary>
    private void NotifyJumpReleased()
    {
        // Clear out the variable height list if they are present
        for (int i = _requestList.Count - 1; i >= 0; i--)
        {
            if (_requestList[i].request.tag == "VariableHeight")
            {
                _requestList.RemoveAt(i);
            }
        }

        // Check if we even have any listeners we need to notify
        if (_jumpReleaseListenerList == null)
        {
            return;
        }

        // Just blanket notify them all. Only one should be even running
        foreach (var listener in _jumpReleaseListenerList)
        {
            listener.Value.JumpRelease();
        }
    }

    /// <summary>
    /// This will apply any persistant forces that we need to until the duration ends
    /// </summary>
    private void FixedUpdate()
    {
        // If we are frozen, we don't want to do anything
        if (_freezeMovement)
        {
            return;
        }

        // If we don't have any requests, we don't need to do anything
        if (_requestList.Count == 0)
        {
            return;
        }

        // Iterate through the request list in reverse order
        for (int i = _requestList.Count - 1; i >= 0; i--)
        {
            var request = _requestList[i];

            // Decrement the timer
            request.duration -= Time.fixedDeltaTime;

            // Submit the velocity request again this frame
            _velocityController.SubmitVelocityRequest(request.request);

            // Remove it if the time has expired
            if (request.duration <= 0f)
            {
                _requestList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Disables user input by freezing movement.
    /// </summary>
    public void FreezeInput()
    {
        _freezeMovement = true;
    }

    /// <summary>
    /// Enables user input by unfreezing movement.
    /// </summary>
    public void UnfreezeInput()
    {
        _freezeMovement = false;
    }

    #endregion

    #endregion
}
