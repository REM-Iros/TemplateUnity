using UnityEngine;

/// <summary>
/// This is the time bar component that is used to track time for player actions, such as attacks. It fills up over time and indicates when an action is ready to be performed.
/// This doesn't handle time logic, however.
/// 
/// REM-i
/// </summary>
public class TimeBar : UISliderParent
{
    #region Methods

    /// <summary>
    /// This method is called when some effect changes the maximum time bar value. Updates the max value accordingly.
    /// </summary>
    /// <param name="newMaxValue"></param>
    public void SetMaxTimeBarValue(float newMaxValue)
    {
        _uiSlider.maxValue = newMaxValue;
        _uiSlider.value = Mathf.Clamp(_uiSlider.value, 0, newMaxValue);
    }

    #endregion
}
