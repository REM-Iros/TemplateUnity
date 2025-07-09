using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the parent script for all of the movement scripts. All versions of this
/// should only need to implement moving the player and should include anything that
/// it might need that isn't included in the current script. Components will also be checked
/// for to enable more features for the script.
/// 
/// REM-i
/// </summary>
public class PMMovementManager : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the list of movement interface you will end up using!")]
    [SerializeField, Header("Move Components")]
    private IMovementInterface _moveComponent;

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
        if (_moveComponent == null)
        {
            Debug.LogError("Movement Manager attached but no move component, player will not be able to move.");
        }
    }

    #region Event Methods

    /*
     * Attach these scripts to the input script from the parent
     */
    public void GetMovementPressed(Vector2 MoveVector)
    {
        ExecuteMove(MoveVector);
    }

    public void GetMovementReleased(Vector2 MoveVector)
    {
        ExecuteMove(MoveVector);
    }

    #endregion

    #region Move Methods

    private void ExecuteMove(Vector2 MoveVector)
    {
        // Check if we even have moves to be able to do
        if (_moveComponent == null)
        {
            Debug.LogError("No move component found");
            return;
        }

        _moveComponent.Move(MoveVector);
    }

    #endregion

    #endregion
}
