using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles all the logic and contains references for our custom UI buttons
/// </summary>
public class SelectableButton : SelectableUI {
    [Tooltip("This event is called when our select button is pressed while the option is selected")]
    public ButtonEvent onButtonPressedEvent;
    [Tooltip("This event is called when our select button is released while this option is selected")]
    public ButtonEvent onButtonReleasedEvent;
    [Tooltip("The associated button's background image")]
    public UnityEngine.UI.Image buttonBackgroundImage;

    private void Update()
    {
        if (Input.GetButtonDown(SelectableUI.SELECT_BUTTON))
        {
            ButtonPressed();
        }
        if (Input.GetButtonUp(SelectableUI.SELECT_BUTTON))
        {
            ButtonReleased();
        }
        
    }

    /// <summary>
    /// If our button was pressed, this method should be called in order to trigger the UnityEvent that should occur
    /// </summary>
    public void ButtonPressed()
    {
        if (onButtonPressedEvent != null)
        {
            onButtonPressedEvent.Invoke(this);
        }
    }

    public void ButtonReleased()
    {
        if (onButtonReleasedEvent != null)
        {
            onButtonReleasedEvent.Invoke(this);
        }
    }

    [System.Serializable]
    public class ButtonEvent : UnityEvent<SelectableButton>
    {

    }
}
