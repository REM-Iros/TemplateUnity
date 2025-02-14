using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Simple script that updates the text of the slider to represent the current
/// percentage of the slider.
/// </summary>
public class SliderValueUpdate : MonoBehaviour
{
    #region Vars

    // The text object that you need for percent text
    [SerializeField]
    private TextMeshProUGUI _percentText;

    [SerializeField]
    private Slider _slider;

    #endregion

    #region Methods

    /// <summary>
    /// Sets the percentage to be equal to the slider currently.
    /// </summary>
    private void Awake()
    {
        _slider = GetComponent<Slider>();

        _percentText.text = _slider.value + "%";
    }

    /// <summary>
    /// Method called by the slider to update the percentage text
    /// </summary>
    public void UpdatePercentageText()
    {
        _percentText.text = _slider.value + "%";
    }

    #endregion
}
