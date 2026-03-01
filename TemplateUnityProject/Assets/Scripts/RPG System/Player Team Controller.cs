using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This is the player team controller. This controls all of the actors for the player party,
/// coordinating their attacks.
/// 
/// REM-i
/// </summary>
public class PlayerTeamController : MonoBehaviour
{
    #region Vars

    // Player input ref
    // Remove this eventually when I have an overall combat manager.
    [Tooltip("This is the Player Input Script from the new Input System for passing current control scheme to actions")]
    private PlayerInput _playerInput;

    // Actor team info - This should be set by a global manager but is currently set as serialize for testing purposes
    public List<RPGCharacterStats> characterStats;

    [Tooltip("This is the list of RPGActors on the team. Set by a global manager but hacked in as serialize for now.")]
    [SerializeField, Header("Actor Components")] 
    private List<RPGActor> _teamMembers;

    [Tooltip("This is the prefab template game actor that is initialized when creating a character.")]
    [SerializeField]
    private GameObject _actorPrefab;

    [Tooltip("This is the parent transform for instantiating actors.")]
    [SerializeField]
    private Transform _actorParentTransform;

    [Tooltip("This is the action UI for the player character's actions. This UI follows the player.")]
    [SerializeField, Header("UI Components")] 
    private PlayerActionsUI _playerActionsUI;

    [Tooltip("This is the larger action UI that is stationary.")]
    [SerializeField]
    private PlayerActionsUI _playerActionsStationaryUI;

    [Tooltip("This is the priority list for actions, it stores references to actor indexes and then ")]
    private List<int> priorityList;

    #endregion

    #region Methods

    /// <summary>
    /// On startup, this is called to initialize the necessary components for actor teams
    /// </summary>
    private void InitializeActorTeam()
    {
        // Initialize priority list
        priorityList = new List<int>();

        InitializePlayerActors();
    }

    /// <summary>
    /// This runs through the list of player actors and initializes them.
    /// </summary>
    private void InitializePlayerActors()
    {
        // Don't run if we are missing something
        if (_actorPrefab == null || _teamMembers == null)
        {
            Debug.LogError("No actors found for team creator. Stopping.");
            return;
        }

        int index = 0;

        // Run through each actor and initialize it
        foreach (RPGCharacterStats stats in characterStats)
        {
            //TODO: This shit is super hacky but for now it will work
            GameObject obj = Instantiate(_actorPrefab, _actorParentTransform);
            RPGActor actor = obj.GetComponent<RPGActor>();
            actor.InitializeActor(index, characterStats[index]);
            _teamMembers.Add(actor);

        }

        // Initialize the two player action UI's
        _playerActionsUI.Init(_playerInput);
        _playerActionsStationaryUI.Init(_playerInput);
    }

    /// <summary>
    /// Called by a character when they are ready to act, adds them to the priority list.
    /// </summary>
    /// <param name="value"></param>
    public void SubscribeToPriorityList(int value)
    {
        // Don't add duplicates
        if (!priorityList.Contains(value))
        {
            priorityList.Add(value);
        }

        // If the priority list isn't empty, show the top priority menu
        if (priorityList.Count != 0)
        {
            ShowTopPriorityMenu();
        }
    }

    /// <summary>
    /// Called when a character becomes unable to act, removes them from the priority list.
    /// </summary>
    /// <param name="value"></param>
    public void UnSubscribeToPriorityList(int value)
    {
        // If the character isn't in the priority list, do nothing
        if (!priorityList.Contains(value))
        {
            return;
        }

        // If the character being removed is at the top of the priority list, clean up the attack
        if (priorityList[0] == value)
        {
            CleanupAttack();
        }

        // Remove the character from the priority list
        priorityList.Remove(value);
    }

    /// <summary>
    /// Called when a character is ready to act, shows the first character's action menu in the priority list.
    /// </summary>
    private void ShowTopPriorityMenu()
    {
        // Don't run this if there are no characters in the priority list
        if (priorityList.Count == 0)
            return;

        // Set the action names for the top priority character
        for (int i = 0; i < _teamMembers[priorityList[0]].ActionCount; i++)
        {
            _playerActionsUI.SetMenuElementAtIndex(i, _teamMembers[priorityList[0]].GetActionNameAtIndex(i));
            _playerActionsStationaryUI.SetMenuElementAtIndex(i, _teamMembers[priorityList[0]].GetActionNameAtIndex(i));
        }

        // Activate the action menu UI
        _playerActionsUI.ActivateActionMenu();
        _playerActionsStationaryUI.ActivateActionMenu();
    }

    /// <summary>
    /// Run through each actor in the team and check if they are all KOed.
    /// </summary>
    /// <returns></returns>
    public bool IsTeamKOed()
    {
        // Check each team member
        foreach (RPGActor member in _teamMembers)
        {
            if (!member.IsKOed)
                return false;
        }

        // All members are KOed
        return true;
    }

    /// <summary>
    /// Handles damage for the players for now, called by the bullets that hit the player movement. Stops player movement
    /// if the whole team is KOed.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="damage"></param>
    public void TakeDamage(int index, int damage)
    {
        // Update health for the specific team member
        _teamMembers[index].UpdateHealth(damage);

        // If the whole team is KOed, kill the player
        if (!IsTeamKOed())
        {
            return;
        }
    }

    /// <summary>
    /// Returns true if there are characters in the priority list ready to attack.
    /// </summary>
    /// <returns></returns>
    public bool IsAttackReady()
    {
        return priorityList.Count > 0;
    }

    /// <summary>
    /// Called when an attack sequence is finished to clean up the priority list. Either turns off the action menu or
    /// enables the next member in the list.
    /// </summary>
    public void CleanupAttack()
    {
        // Finish the attack for the current attacker
        _teamMembers[priorityList[0]].FinishAction();    

        // Remove the current attacker
        priorityList.RemoveAt(0);

        // If there are still members in the priority list, show the next menu
        if (priorityList.Count > 0)
        {
            Debug.Log("Continue Showing Menu");
            ShowTopPriorityMenu();
        }
        else
        {
            Debug.Log("Stop Showing Menu");
            // Update the action UI
            _playerActionsUI.DeactivateActionMenu();
            _playerActionsStationaryUI.DeactivateActionMenu();
        }
    }

    #endregion

    #region Hacky Attack Handlers

    public int GetRandomTarget()
    {
        List<int> aliveIndices = new List<int>();

        for (int i = 0; i < _teamMembers.Count; i++)
        {
            if (!_teamMembers[i].IsKOed)
            {
                aliveIndices.Add(i);
            }
        }

        if (aliveIndices.Count == 0)
        {
            return -1; // No alive targets
        }

        int randomIndex = Random.Range(0, aliveIndices.Count);

        return aliveIndices[randomIndex];
    }

    #endregion
}
