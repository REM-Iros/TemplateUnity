using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the overall ui manager for RPG battles. It will control individual UI units and handle subscription of UI units to actors.
/// 
/// REM-i
/// </summary>
public class RPGBattleUIManager : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the prefab ui object for player actors.")]
    [SerializeField, Header("Actor UI Prefabs")]
    private GameObject _playerActorUIPrefab;

    [Tooltip("This is the prefab ui object for enemy actors.")]
    [SerializeField]
    private GameObject _enemyActorUIPrefab;

    [Tooltip("This is the parent transform to instantiate UI under for player actors.")]
    [SerializeField, Header("Parent UI Transforms")]
    private Transform _playerActorUIParentTransform;

    [Tooltip("This is the parent transform to instantiate UI under for enemy actors")]
    [SerializeField]
    private Transform _enemyActorUIParentTransform;

    [Tooltip("This is the action menu ui object, reused for all actors in a scene.")]
    [SerializeField, Header("Action Menu UI")]
    private PlayerActionsUI _actionMenuUI;

    [Tooltip("This is the player team target menu.")]
    [SerializeField, Header("Target UI")]
    private RPGActorTeamTargetUI _playerTeamTargetUI;

    [Tooltip("This is the enemy team target menu.")]
    [SerializeField]
    private RPGActorTeamTargetUI _enemyTeamTargetUI;

    [Tooltip("This is a dictionary of all ui objects registered to actors. I don't know if I'll ever need this but just in case.")]
    private Dictionary<RPGActor, RPGActorUIBinder> _actorBindingDict = new();

    #endregion

    #region Methods

    /// <summary>
    /// This is used to create binders between actors and their UI.
    /// </summary>
    /// <param name="actor"></param>
    /// <param name="isPlayer"></param>
    public void RegisterActor(RPGActor actor, bool isPlayer)
    {
        // Instantiate based on if it is a player or not
        GameObject uiPrefab = 
            Instantiate(isPlayer ? _playerActorUIPrefab : _enemyActorUIPrefab, 
                        isPlayer ? _playerActorUIParentTransform : _enemyActorUIParentTransform);

        // Create a target icon to go with the actor

        // Add the binder and activate it
        var uiBinder = uiPrefab.GetComponent<RPGActorUIBinder>();
        uiBinder.Bind(actor);

        _actorBindingDict.Add(actor, uiBinder);
    }

    /// <summary>
    /// Unbinds actor and ui and removes them from the dictionary (idk if I'll ever need this but yuh)
    /// </summary>
    /// <param name="actor"></param>
    public void UnregisterActor(RPGActor actor)
    {
        if (!_actorBindingDict.ContainsKey(actor))
        {
            Debug.LogError($"Trying to unregister actor {actor.name} from the battle UI manager but it isn't registered!");
            return;
        }

            
        _actorBindingDict[actor].Unbind();
        _actorBindingDict[actor].gameObject.SetActive(false);
        _actorBindingDict.Remove(actor);
    }

    /// <summary>
    /// Sets the action menu up for use
    /// </summary>
    public void InitializeActionMenu()
    {
        //_actionMenuUI.Init(player input)
        //TODO: Need to buff this out, still haven't figured out the whole player input thing
    }

    /// <summary>
    /// Binds the action menu to the current actor that needs to go.
    /// </summary>
    public void BindActionMenu(RPGActor actor)
    {
        _actionMenuUI.Bind(actor);
    }

    #endregion
}
