using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the player team controller. This controls all of the actors for the player party,
/// coordinating their attacks.
/// 
/// REM-i
/// </summary>
public class TeamController : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the list of RPGActors on the team. Set by a global manager but hacked in as serialize for now.")]
    [SerializeField, Header("Actor Stat Components")]
    private List<RPGActor> _actors;

    [Tooltip("This is the public getter for rpg actors.")]
    public List<RPGActor> Actors => _actors;

    [Tooltip("This is the prefab template game actor sprite that is initialized when creating a character.")]
    [SerializeField, Header("Actor GameObject Components")]
    private GameObject _actorPrefab;

    [Tooltip("This is the parent transform for instantiating actors. Devs to decide how big they want their parties.")]
    [SerializeField]
    private List<Transform> _actorPrefabParentTransforms;

    #endregion

    #region Methods

    /// <summary>
    /// On startup, this is called to initialize the necessary components for actor teams
    /// </summary>
    public void InitializeActorTeam(List<CharacterStats> teamActorStats)
    {
        // No character stats found for the team
        if (teamActorStats == null || teamActorStats.Count == 0)
        {
            Debug.LogError("No character stats found for team creator. Stopping.");
            return;
        }

        // Don't run if we are missing something
        if (_actorPrefab == null || _actors == null)
        {
            Debug.LogError("No actors found for team creator. Stopping.");
            return;
        }

        // Set the index for creating actors through 
        int index = 0;

        // Run through each actor and initialize it
        foreach (CharacterStats stats in teamActorStats)
        {
            InitializeActor(stats, index);
            index++;
        }
    }

    /// <summary>
    /// This runs through the list of player actors and initializes them.
    /// </summary>
    private void InitializeActor(CharacterStats stats, int index)
    {
        // Instantiate the gameobject and get the actor component
        GameObject obj = Instantiate(_actorPrefab, 
                                     _actorPrefabParentTransforms[index].position, 
                                     _actorPrefabParentTransforms[index].rotation, 
                                     _actorPrefabParentTransforms[index]);

        RPGActor actor = obj.GetComponent<RPGActor>();

        // Initialize the actor with the stats and add it to the list
        actor.InitializeActor(stats);
        _actors.Add(actor);
    }

    /// <summary>
    /// This method is used by the team controller
    /// </summary>
    /// <param name="actor"></param>
    private void SubscribeToActorEvents(RPGActor actor)
    {
        /*
        actor.OnActorKO += UnSubscribeToPriorityList;
        actor.OnActorActionAvailable += SubscribeToPriorityList;
        */
    }

    /*
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
    */

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

    /*
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
    */

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

        //int randomIndex = Random.Range(0, aliveIndices.Count);

        return 0;

        //return aliveIndices[randomIndex];
    }

    #endregion
}
