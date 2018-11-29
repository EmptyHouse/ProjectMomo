using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectableSlider : SelectableUI {
    #region main variables
    public Slider sliderUIReference;
    [Tooltip("Set this value to true to round all values to integer values")]
    public bool useIntValue = false;
    [Tooltip("The minimum value that our slider element can be")]
    public float minSliderValue = 0;
    [Tooltip("The maximum value that our slider element can be")]
    public float maxSliderValue = 1;
    [Tooltip("The value that will ")]
    public float incrementValue = .01f;

    private float currentlyDisplayedValue = 0;
    #endregion main variables
    #region monobehaviour methods
    private void Update()
    {
        
    }

    private void OnValidate()
    {
        if (incrementValue < 0)
        {
            incrementValue = 0;
        }

        if (useIntValue)
        {
            if (incrementValue < 1)
            {
                incrementValue = 1;
            }
            incrementValue = (int)incrementValue;
        }
    }
    #endregion monobehaviour methods

    /// <summary>
    /// Set the slider value
    /// </summary>
    /// <param name="valueToSetSlider"></param>
    public void SetValue(float valueToSetSlider)
    {
        if (valueToSetSlider < minSliderValue)
        {
            sliderUIReference.value = 0;
        }
        else if (valueToSetSlider > maxSliderValue)
        {
            sliderUIReference.value = 1;
        }
    }

}
