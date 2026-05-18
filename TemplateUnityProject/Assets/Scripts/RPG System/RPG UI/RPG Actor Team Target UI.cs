using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the targeting system manager. It handles instantiating the targeting instances and linking the target icons
/// to the characters.
/// 
/// REM-i
/// </summary>
public class RPGActorTeamTargetUI : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the target object prefab that ")]
    [SerializeField, Header("Target Prefab")]
    private GameObject _targetPrefab;

    [Tooltip("This is the parent transform for the targeting objects.")]
    [SerializeField, Header("Parent Transforms")]
    private Transform _parentTransform;

    /*
    [Tooltip("This is the list of player targets.")]
    [SerializeField, Header("Actor Target Lists")]
    private List<>
    */

    #endregion

    #region Methods

    /// <summary>
    /// Instantiates the target, converts it's position from world space to canvas space.
    /// </summary>
    /// <param name="actorTransform"></param>
    /// <param name="isPlayer"></param>
    public void InitializeTarget(Transform actorTransform)
    {
        GameObject targetObj = Instantiate(_targetPrefab, _parentTransform);

        // Maybe bind this using ui?
    }

    #endregion
}
