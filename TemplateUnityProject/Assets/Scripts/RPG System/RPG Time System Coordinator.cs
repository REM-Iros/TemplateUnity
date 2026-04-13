using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the system that coordinates actions between actors using their time coordinators. This helps
/// determine which actor acts at the time.
/// 
/// REM-i
/// </summary>
public class RPGTimeSystemCoordinator : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the list of player actors that are ready to attack.")]
    private List<int> _playerPriorityList;

    [Tooltip("This is the list of enemy actors that are ready to attack.")]
    private List<int> _enemyPriorityList;

    #endregion

    #region Methods

    private void SubscribePlayerActor(int actorID)
    {
        _playerPriorityList.Add(actorID);
    }

    #endregion
}
