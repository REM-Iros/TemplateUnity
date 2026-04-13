using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This is the player team controller. This controls all of the actors for the player party,
/// coordinating their attacks.
/// 
/// REM-i
/// </summary>
public class TeamController : MonoBehaviour
{
    #region Vars

    // Actor team info - This should be set by a global manager but is currently set as serialize for testing purposes
    public List<CharacterStats> characterStats;

    [Tooltip("This is the list of RPGActors on the team. Set by a global manager but hacked in as serialize for now.")]
    [SerializeField, Header("Actor Stat Components")]
    private List<RPGActor> _actors;

    [Tooltip("This is the public getter for rpg actors.")]
    public List<RPGActor> Actors => _actors;

    [Tooltip("This is the prefab template game actor sprite that is initialized when creating a character.")]
    [SerializeField, Header("Actor GameObject Components")]
    private GameObject _actorPrefab;

    [Tooltip("This is the parent transform for instantiating actors.")]
    [SerializeField]
    private Transform _actorPrefabParentTransform;

    /*
    Note, remove the UI stuff from the team controller, it should simply handle each actor's stats. UI will be controlled by
    a separate manager.
     */

    [Tooltip("This is the actor status UI prefab that is instantiated for each character.")]
    [SerializeField, Header("Actor UI Components")]
    private GameObject _actorStatusUIPrefab;

    [Tooltip("This is the parent transform for instantiating actor status UIs.")]
    [SerializeField]
    private Transform _actorStatusUIParentTransform;

    [Tooltip("This is the action menu prefab that is instantiated for each character.")]
    [SerializeField]
    private GameObject _actionMenuPrefab;

    [Tooltip("This is the parent transform for instantiating action menus.")]
    [SerializeField]
    private Transform _actionMenuParentTransform;

    [Tooltip("This is the priority list for actions, it stores references to actor indexes and then ")]
    private List<int> priorityList;

    #endregion

    #region Methods

    /// <summary>
    /// On startup, this is called to initialize the necessary components for actor teams
    /// </summary>
    public void InitializeActorTeam(List<CharacterStats> teamActorStats)
    {
        // Initialize priority list
        priorityList = new List<int>();

        InitializePlayerActors(teamActorStats);
    }

    /// <summary>
    /// This runs through the list of player actors and initializes them.
    /// </summary>
    private void InitializePlayerActors(List<CharacterStats> teamActorStats)
    {
        // Don't run if we are missing something
        if (_actorPrefab == null || _actors == null)
        {
            Debug.LogError("No actors found for team creator. Stopping.");
            return;
        }

        int index = 0;

        // Run through each actor and initialize it
        foreach (CharacterStats stats in teamActorStats)
        {
            //TODO: This shit is super hacky but for now it will work
            GameObject obj = Instantiate(_actorPrefab, _actorPrefabParentTransform);
            RPGActor actor = obj.GetComponent<RPGActor>();
            actor.InitializeActor(index, stats);
            _actors.Add(actor);

            SubscribeToActorEvents(actor);

            index++;
        }
    }

    /// <summary>
    /// This method is used by the team controller
    /// </summary>
    /// <param name="actor"></param>
    private void SubscribeToActorEvents(RPGActor actor)
    {
        actor.OnActorKO += UnSubscribeToPriorityList;
        actor.OnActorActionAvailable += SubscribeToPriorityList;
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

        //_actors[priorityList[0]].ActivateActionMenu();
    }

    /// <summary>
    /// Run through each actor in the team and check if they are all KOed.
    /// </summary>
    /// <returns></returns>
    public bool IsTeamKOed()
    {
        // Check each team member
        foreach (RPGActor member in _actors)
        {
            if (!member.HealthCoordinator.IsKOed)
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
        _actors[index].HealthCoordinator.UpdateHealth(damage);

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
        _actors[priorityList[0]].FinishAction();    

        // Remove the current attacker
        priorityList.RemoveAt(0);

        // If there are still members in the priority list, show the next menu
        if (priorityList.Count <= 0)
        {
            return;
        }

        ShowTopPriorityMenu();
    }

    /// <summary>
    /// Called by the team controller to initialize the action menu subscribing to the input controller.
    /// </summary>
    /// <param name="playerInput"></param>
    public void InitializeActionMenuInputController(PlayerInput playerInput)
    {
        foreach(RPGActor member in _actors)
        {
            //member.InitializeActionMenu(playerInput);
        }
    }

    /// <summary>
    /// Unsubscribe to the actor events on destroy.
    /// </summary>
    private void OnDestroy()
    {
        foreach (RPGActor member in _actors)
        {
            member.OnActorKO -= UnSubscribeToPriorityList;
            member.OnActorActionAvailable -= SubscribeToPriorityList;
        }
    }

    #endregion

    #region Hacky Attack Handlers

    public int GetRandomTarget()
    {
        List<int> aliveIndices = new List<int>();

        for (int i = 0; i < _actors.Count; i++)
        {
            if (!_actors[i].HealthCoordinator.IsKOed)
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
