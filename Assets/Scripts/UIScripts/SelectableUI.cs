using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Generic class for Selectable UI objects. This can include buttons, sliders, and dropdown menus
/// that a player can actively select in the game.
/// </summary>
public class SelectableUI : MonoBehaviour {
    #region const
    public const string SELECT_BUTTON = "Submit";
    public const string CANCEL_BUTTON = "Cancel";
    #endregion const
    #region enums
    public enum UIDirection
    {
        North,
        South,
        East,
        West,
    }
    #endregion enums

    public RectTransform rectTransform
    {
        get
        {
            if (cachedRectTransform == null)
            {
                cachedRectTransform = GetComponent<RectTransform>();
            }
            return cachedRectTransform;
        }
    }

    private RectTransform cachedRectTransform;


    [Tooltip("UI object that will be selected if the player is pointing in the 'up' direction.")]
    public SelectableUI northSelectableUI;
    [Tooltip("UI object that will be selected if the player is pointing in the 'down' direction.")]
    public SelectableUI southSelectableUI;
    [Tooltip("UI object that will be selected if the player is pointing in the 'right' direction.")]
    public SelectableUI eastSelectableUI;
    [Tooltip("UI object that will be selected if the player is pointing in the 'left' direction.")]
    public SelectableUI westSelectableUI;
    [Tooltip("A reference to the text field that contains the title of the UI Element")]
    public Text uiTitleText;


    private void Awake()
    {
        this.enabled = false;
    }

    public SelectableUI GetUIInDirection(UIDirection direction)
    {
        switch (direction)
        {
            case UIDirection.North:

                return northSelectableUI;
            case UIDirection.South:

                return southSelectableUI;
            case UIDirection.East:

                return eastSelectableUI;
            case UIDirection.West:

                return westSelectableUI;
        }
        return null;
    }


}
