using System;
using UnityEngine;

/// <summary>
/// This component handles health for an actor.
/// 
/// REM-i
/// </summary>
public class RPGActorHealthCoordinator : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the actor that owns this component. Passed on event invokes.")]
    private RPGActor _owner;

    [Tooltip("This is the max HP of the actor.")]
    private int _maxHP = 5;

    [Tooltip("This is the getter for the max HP of the actor.")]
    public int MaxHP => _maxHP;

    [Tooltip("This is the current HP for the actor.")]
    private int _currHP = 5;

    [Tooltip("This is the getter for the current HP of the actor.")]
    public int CurrHP => _currHP;

    [Tooltip("This determines whether the character is KOed or not.")]
    private bool _isKOed = false;

    [Tooltip("This is the public getter for KO status.")]
    public bool IsKOed => _isKOed;

    [Tooltip("This is the event that triggers when hp changes.")]
    public event Action<float> OnHPUpdate;

    [Tooltip("This is the event that triggers when max hp changes.")]
    public event Action<float> OnMaxHPUpdate;

    [Tooltip("This event is called when the player is KOed.")]
    public event Action<RPGActor> OnKO;

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the health bar component for the actor.
    /// </summary>
    /// <param name="maxHP"></param>
    public void Initialize(RPGActor owner, int maxHP, int currHp)
    {
        _owner = owner;
        _maxHP = maxHP;
        _currHP = currHp;
    }

    /// <summary>
    /// This method is called when damage or healing occurs in combat.
    /// </summary>
    /// <param name="valueToChange"></param>
    public void UpdateHealth(int valueToChange)
    {
        // Update the current hp clamped within max and 0
        _currHP = Mathf.Clamp(_currHP + valueToChange, 0, _maxHP);

        OnHPUpdate?.Invoke(_currHP);

        // If hp is greater than 0, don't continue
        if (_currHP > 0)
        {
            return;
        }

        // Ko the actor
        KOActor();
    }

    /// <summary>
    /// Called when an actor goes to 0 hp, KOes the actor and notifies other scripts.
    /// </summary>
    private void KOActor()
    {
        // Set ko and invoke event
        _isKOed = true;
        OnKO?.Invoke(_owner);
    }

    #endregion
}
