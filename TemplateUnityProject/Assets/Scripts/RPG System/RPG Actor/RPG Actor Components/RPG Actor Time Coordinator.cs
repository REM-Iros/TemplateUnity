using System;
using UnityEngine;

/// <summary>
/// Coordinates the time mechanic for an RPG actor, notifying when they can take actions.
/// 
/// REM-i
/// </summary>
public class RPGActorTimeCoordinator : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the max time value for the actor's time bar.")]
    private float _maxTime;

    [Tooltip("This is the public getter for max time.")]
    public float MaxTime => _maxTime;

    [Tooltip("This is the current time value for the actor's time bar.")]
    private float _currentTime;

    [Tooltip("This is the public getter for current time.")]
    public float CurrentTime => _currentTime;

    [Tooltip("This is the rate at which the actor's time bar fills up.")]
    private float _timeFillRate;

    [Tooltip("This indicates whether the actor is currently able to take an action.")]
    private bool _canAct;

    [Tooltip("This indicates whether the time coordinator should be running.")]
    private bool _isActive = false;

    [Tooltip("This event runs when the time value changes.")]
    public event Action<float> OnTimeUpdate;

    [Tooltip("This event runs when the max time value changes.")]
    public event Action<float> OnMaxTimeUpdate;

    [Tooltip("This is the event that is triggered when the actor can take an action.")]
    public event Action OnCanAct;

    #endregion

    #region Methods

    /// <summary>
    /// Initializes this component with the given maximum time value. This shouldn't change often, with the
    /// fill rate being a better component to modify for different effects.
    /// </summary>
    /// <param name="newMaxTimeValue"></param>
    public void Initialize(float newMaxTimeValue)
    {
        // Set max time and zero out current time
        _maxTime = newMaxTimeValue;
        _currentTime = 0f;
        _timeFillRate = 1f;

        // Start the timer
        _isActive = true;
        _canAct = false;
    }

    /// <summary>
    /// Update runs the time fill logic each frame.
    /// </summary>
    private void Update()
    {
        // Only run if the timer is active and the actor can't act yet
        if (!_isActive || _canAct)
        {
            return;
        }

        // Increment the current time based on fill rate and delta time
        _currentTime += _timeFillRate * Time.deltaTime;

        OnTimeUpdate?.Invoke(_currentTime);

        // Check if the actor can now act
        if (_currentTime < _maxTime)
        {
            return;
        }

        // Actor can act now
        _currentTime = _maxTime;
        _canAct = true;

        // Notify that the actor can act
        OnCanAct?.Invoke();
    }

    /// <summary>
    /// Called generally when an action completes or the time bar is reset for any reason.
    /// </summary>
    public void ResetTimer()
    {
        _currentTime = 0f;
        _canAct = false;

        OnTimeUpdate?.Invoke(_currentTime);
    }

    /// <summary>
    /// Called by the RPG actor, updates the time modifier value
    /// </summary>
    /// <param name="newValue"></param>
    public void UpdateTimeModifier(float newValue)
    {
        _timeFillRate = newValue;
    }

    /// <summary>
    /// Called when an actor is defeated
    /// </summary>
    public void DeactivateAndResetTimer()
    {
        // Set time to 0 and deactivate
        _currentTime = 0f;
        _canAct = false;
        _isActive = false;

        // Reset visual
        OnTimeUpdate?.Invoke(_currentTime);
    }

    #endregion
}
