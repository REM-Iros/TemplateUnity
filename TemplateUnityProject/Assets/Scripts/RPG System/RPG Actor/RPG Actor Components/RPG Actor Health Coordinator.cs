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

    [Tooltip("This is the max HP of the actor.")]
    private int _maxHP = 5;

    [Tooltip("This is the current HP for the actor.")]
    private int _currHP = 5;

    [Tooltip("This determines whether the character is KOed or not.")]
    private bool _isKOed = false;

    [Tooltip("This is the public getter for KO status.")]
    public bool IsKOed => _isKOed;

    [Tooltip("This event is called when the player is KOed.")]
    public event Action OnKO;

    [Tooltip("This is the health bar component for the health coordinator.")]
    [SerializeField, Header("UI Components")]
    private HealthBar _healthBar;

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the health bar component for the actor.
    /// </summary>
    /// <param name="maxHP"></param>
    public void Initialize(int maxHP, int index)
    {
        _maxHP = maxHP;
        _currHP = _maxHP;
    }

    /// <summary>
    /// This method is called when damage or healing occurs in combat.
    /// </summary>
    /// <param name="valueToChange"></param>
    public void UpdateHealth(int valueToChange)
    {
        // Update the current hp clamped within max and 0
        _currHP = Mathf.Clamp(_currHP + valueToChange, 0, _maxHP);

        // Update the visual
        _healthBar.UpdateSliderValue(_currHP);

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
        OnKO?.Invoke();
    }

    #endregion
}
