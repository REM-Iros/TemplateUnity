using UnityEngine;
using TMPro;

/// <summary>
/// This is the health bar component that is universally used for all actors (minus perhaps boss health bars). This
/// should display the health of an actor in combat, and update when damage is taken. This does not handle HP logic, however.
/// 
/// REM-i
/// </summary>
public class RPGHealthBar : UISliderParent, RPGIUIInterface
{
    #region Vars

    [Tooltip("This is the actor we want to bind this UI to")]
    private RPGActorHealthCoordinator _actorHealthCoordinator;

    [Tooltip("This is the text component that displays the health value.")]
    [SerializeField] 
    private TextMeshProUGUI _hpText;

    #endregion

    #region Methods

    /// <summary>
    /// On bind, we need to set max value and current value of slider, and subscribe to events.
    /// </summary>
    /// <param name="actor"></param>
    public void Bind(RPGActor actor)
    {
        _actorHealthCoordinator = actor.HealthCoordinator;

        // Set Max value of slider
        Initialize(_actorHealthCoordinator.MaxHP);

        // Set Current value of slider
        UpdateSliderValue(_actorHealthCoordinator.CurrHP);

        _actorHealthCoordinator.OnHPUpdate += UpdateSliderValue;
    }

    /// <summary>
    /// On unbind, we need to unsub from events and remove reference to health coordinator.
    /// </summary>
    public void Unbind()
    {
        _actorHealthCoordinator.OnHPUpdate -= UpdateSliderValue;

        _actorHealthCoordinator = null;
    }

    /// <summary>
    /// Override to add health text update functionality.
    /// </summary>
    /// <param name="newValue"></param>
    public override void UpdateSliderValue(float newValue)
    {
        base.UpdateSliderValue(newValue);

        // Update the health text if it exists
        if (_hpText != null)
        {
            UpdateHealthSliderText();
        }
    }

    /// <summary>
    /// Called when the health value is updated, updates the text display accordingly.
    /// </summary>
    private void UpdateHealthSliderText()
    {
        _hpText.text = _uiSlider.value + "/" + _uiSlider.maxValue;
    }

    #endregion
}
