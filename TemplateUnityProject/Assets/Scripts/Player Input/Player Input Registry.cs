using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the service that gets stored in the Service Locator. It contains a registry of all player input routers for ease of access.
/// 
/// REM-i
/// </summary>
public class PlayerInputRegistry : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the dictionary that we store input routers in.")]
    private static readonly Dictionary<int, PlayerInputRouter> _routers = new();

    #endregion

    #region Methods

    /// <summary>
    /// Registers an input router with the appropriate player index.
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <param name="router"></param>
    public void Register(int playerIndex, PlayerInputRouter router)
    {
        _routers.Add(playerIndex, router);
    }

    /// <summary>
    /// Unregisters an input router via the player index.
    /// </summary>
    /// <param name="playerIndex"></param>
    public void Unregister(int playerIndex)
    {
        _routers.Remove(playerIndex);
    }

    /// <summary>
    /// Attempts to get a router at player index and return it.
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <returns></returns>
    public PlayerInputRouter Get(int playerIndex)
    {
        if(_routers.TryGetValue(playerIndex, out var router))
        {
            return router;
        }

        return null;
    }

    /// <summary>
    /// We want to clear the dictionary on destroy to avoid memory leaks. This is a static dictionary, so it will persist across scenes unless cleared.
    /// </summary>
    private void OnDestroy()
    {
        _routers.Clear();
    }

    #endregion
}
