using UnityEngine;

/// <summary>
/// This is the time bar component that is used to track time for player actions, such as attacks. It fills up over time and indicates when an action is ready to be performed.
/// This doesn't handle time logic, however.
/// 
/// REM-i
/// </summary>
public class RPGTimeBar : UISliderParent, RPGIUIInterface
{
    [Tooltip("This is the Time Coordinator we are binding to.")]
    private RPGActorTimeCoordinator _actorTimeCoordinator;

    #region Methods

    /// <summary>
    /// Bind gets the time coordinator, updates values as needed, and subscribes to events.
    /// </summary>
    /// <param name="actor"></param>
    public void Bind(RPGActor actor)
    {
        _actorTimeCoordinator = actor.TimeCoordinator;

        // Set Max value of slider
        Initialize(_actorTimeCoordinator.MaxTime);

        // Set Current value of slider
        UpdateSliderValue(_actorTimeCoordinator.CurrentTime);

        // Sub to events
        _actorTimeCoordinator.OnTimeUpdate += UpdateSliderValue;
        _actorTimeCoordinator.OnMaxTimeUpdate += SetMaxTimeBarValue;
    }

    /// <summary>
    /// Unbind unsubs from events, and removes the current time coordinator.
    /// </summary>
    public void Unbind()
    {
        // Unsub events
        _actorTimeCoordinator.OnTimeUpdate -= UpdateSliderValue;
        _actorTimeCoordinator.OnMaxTimeUpdate -= SetMaxTimeBarValue;

        // Null out time coordinator
        _actorTimeCoordinator = null;
    }

    /// <summary>
    /// This method is called when some effect changes the maximum time bar value. Updates the max value accordingly.
    /// </summary>
    /// <param name="newMaxValue"></param>
    public void SetMaxTimeBarValue(float newMaxValue)
    {
        // Update max slider value
        _uiSlider.maxValue = newMaxValue;

        // Update current slider value if it is out of range
        UpdateSliderValue(_uiSlider.value);
    }

    #endregion
}
