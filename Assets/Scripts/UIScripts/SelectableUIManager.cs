using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableUIManager : MonoBehaviour {
    #region const variables
    /// <summary>
    /// The minimum absolute value of our joystick axis, before it is registered
    /// as an input in our settings manager
    /// </summary>
    protected const float JOYTICK_THRESHOLD = .65f;
    #endregion const variables
    #region main variables
    [Tooltip("This is the time before we begin autoscrolling in our menu. We will always move to a new item immediately, but we may want a delay before scrolling to different optins automatically")]
    public float timeBeforeAutoScrolling = .4f;
    [Tooltip("The time between selecting the next item when we begin autoscrolling.")]
    public float timeToScrollToNextOption = .1f;
    [Tooltip("The menu item that we will select upon opening the menu. If resetToInitialUIUponOpening is set to false, we will remain on the previous menu option whenever this menu is opened again")]
    public SelectableUI initiallySelectedUI;
    [Tooltip("This will make it so that we begin our menu on the initiallySelectedUI object whenever we open our menu")]
    public bool resetToInitialUIUponOpening;
    #endregion main variables

    private SelectableUI currentlySelectedUI;
    private bool isCurrentlyAutoScrolling;

    #region monobehaviour methods

    private void Start()
    {
        currentlySelectedUI = initiallySelectedUI;
    }

    private void Update()
    {
        if (isCurrentlyAutoScrolling)
        {
            return;
        }
    }
    #endregion monobehaviour methods

    private IEnumerator BeginAutoScrollingVertial()
    {
        yield break;
    }

    private IEnumerator BeginAutoScrollingHorizontal()
    {
        yield break;
    }
}
