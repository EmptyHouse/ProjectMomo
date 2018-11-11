using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Generic class for Selectable UI objects. This can include buttons, sliders, and dropdown menus
/// that a player can actively select in the game.
/// </summary>
public class SelectableUI : MonoBehaviour {
    #region enums
    public enum UIDirection
    {
        North,
        South,
        East,
        West,
    }
    #endregion enums


    [Tooltip("UI object that will be selected if the player is pointing in the 'up' direction.")]
    public SelectableUI northSelectableUI;
    [Tooltip("UI object that will be selected if the player is pointing in the 'down' direction.")]
    public SelectableUI southSelectableUI;
    [Tooltip("UI object that will be selected if the player is pointing in the 'right' direction.")]
    public SelectableUI eastSelectableUI;
    [Tooltip("UI object that will be selected if the player is pointing in the 'left' direction.")]
    public SelectableUI westSelectableUI;


}
