using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This registry should contain all the current active player rigs for reference. This registry is injected into the service locator.
/// 
/// REM-i
/// </summary>
public class PlayerInputRigRegistry : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the dictionary that we store input rigs in.")]
    private static readonly SortedDictionary<int, PlayerInputRig> _rigs = new();

    [Tooltip("This is our rig prefab we generate when we need a new rig.")]
    private GameObject _rigPrefab;

    [Tooltip("This is the max amount of players you want to have in your game.")]
    private const int _maxPlayerCount = 4;

    #endregion

    #region Methods

    /// <summary>
    /// On start, we need to create a basic player rig to bind to the registry, and then register it.
    /// </summary>
    private void Start()
    {
        _rigPrefab = Resources.Load<GameObject>("Prefabs/Player Input/Player Input Rig");

        RegisterRig();
    }

    /// <summary>
    /// Generates a rig on call via service locator. Gets the lowest index, and then generates the rig.
    /// </summary>
    public void RegisterRig()
    {
        // Get the index for the rig we generate
        int rigIndex = GetNextEmptySlot();

        // If we have no more slots available, end method.
        if (rigIndex < 0)
        {
            Debug.LogWarning("Max number of rigs created and dictionary full.");
            return;
        }

        // Create the first rig
        GameObject rig = Instantiate(_rigPrefab);

        // Get component and register it to the dictionary
        PlayerInputRig rigScript = rig.GetComponent<PlayerInputRig>();
        rigScript.GenerateRig(rigIndex);

        // Add the rig to the dictionary
        _rigs.Add(rigIndex, rigScript);
    }

    /// <summary>
    /// Removes rig at index from the dictionary and destroys the rig.
    /// </summary>
    /// <param name="index"></param>
    public void UnregisterRig(int index)
    {
        if (!_rigs.ContainsKey(index))
        {
            return;
        }

        // Remove the rig from the dictionary and destroy it.
        _rigs[index].RemoveRig();

        // Remove the rig from the dictionary.
        _rigs.Remove(index);
    }

    /// <summary>
    /// This script runs through the dictionary at indexes and finds the next available slot, if none are available, return -1.
    /// </summary>
    /// <returns></returns>
    private int GetNextEmptySlot()
    {
        // Iterate through the dictionary for an empty slot.
        for (int i = 0; i < _maxPlayerCount; i++)
        {
            if (!_rigs.ContainsKey(i))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// On destroy, we want to clear the dictionary to avoid memory leaks. This is a static dictionary, so it will persist across scenes unless cleared.
    /// </summary>
    private void OnDestroy()
    {
        _rigs.Clear();
    }

    #endregion
}
