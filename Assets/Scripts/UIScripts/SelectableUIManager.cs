using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that will act as the manager script for all attached selectable UI elements
/// This will open and close the menu as well as carry out any actions required when selectint
/// UI elements
/// </summary>
public class SelectableUIManager : MonoBehaviour {
    #region const variables
    /// <summary>
    /// The minimum absolute value of our joystick axis, before it is registered
    /// as an input in our settings manager
    /// </summary>
    protected const float JOYSTICK_THRESHOLD = .65f;
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
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(verticalInput) > JOYSTICK_THRESHOLD)
        {

        }
        else if (Mathf.Abs(horizontalInput) > JOYSTICK_THRESHOLD)
        {

        }
    }
    #endregion monobehaviour methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="initialVerticalInput"></param>
    /// <returns></returns>
    private IEnumerator BeginAutoScrollingVertial(float initialVerticalInput)
    {
        isCurrentlyAutoScrolling = true;
        float direction = Mathf.Sign(initialVerticalInput);

        float timeThatHasPassed = 0;

        ///TO-DO Add code that will change to the next item in the ui menu

        while (timeThatHasPassed < timeBeforeAutoScrolling)
        {
            if (direction * Input.GetAxisRaw("Vertical") < JOYSTICK_THRESHOLD)
            {
                this.isCurrentlyAutoScrolling = false;
                yield break;
            }
            timeThatHasPassed += Time.unscaledDeltaTime;
            yield return null;
        }

        timeThatHasPassed = 0;
        while (direction * Input.GetAxisRaw("Vertical") > JOYSTICK_THRESHOLD)
        {
            if (timeThatHasPassed > timeToScrollToNextOption)
            {
                timeThatHasPassed = 0;
                ///TO-DO Add code that will change to the next item in the ui menu

            }

            timeThatHasPassed += Time.unscaledDeltaTime;
            yield return null;
        }
        yield break;
    }
}
