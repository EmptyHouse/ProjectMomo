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
    //[Tooltip("The transform parent that contains all the UI elements related to this menu")]
    
    #endregion main variables

    public SelectableUI currentlySelectedUI { get; private set; }
    private bool isCurrentlyAutoScrolling;

    #region monobehaviour methods

    private void Awake()
    {
        SetNextSelectableUIOption(initiallySelectedUI);
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
            StartCoroutine(BeginAutoScrollingVertical(verticalInput));
        }
        else if (Mathf.Abs(horizontalInput) > JOYSTICK_THRESHOLD)
        {

        }
    }
    #endregion monobehaviour methods


    private SelectableUI GetNextOption(SelectableUI.UIDirection directionToCheck)
    {
        SelectableUI optionToCheck = currentlySelectedUI.GetUIInDirection(directionToCheck);
        return optionToCheck;
        //while (optionToCheck != null && !currentlyCheckedOptions.Contains(optionToCheck))
        //{

        //}
    }

    private void SetNextSelectableUIOption(SelectableUI selectableUI)
    {
        if (selectableUI == null) return;
        if (this.currentlySelectedUI != null)
        {
            this.currentlySelectedUI.enabled = false;
        }
        this.currentlySelectedUI = selectableUI;
        this.currentlySelectedUI.enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="initialVerticalInput"></param>
    /// <returns></returns>
    private IEnumerator BeginAutoScrollingVertical(float initialVerticalInput)
    {
        isCurrentlyAutoScrolling = true;
        float direction = Mathf.Sign(initialVerticalInput);

        float timeThatHasPassed = 0;

        ///TO-DO Add code that will change to the next item in the ui menu
        SelectableUI nextOption = GetNextOption(direction < 0 ? SelectableUI.UIDirection.South : SelectableUI.UIDirection.North);
        if (nextOption != null)
        {
            SetNextSelectableUIOption(nextOption);
        }
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
                nextOption = GetNextOption(direction < 0 ? SelectableUI.UIDirection.South : SelectableUI.UIDirection.North);
                if (nextOption != null)
                {
                    SetNextSelectableUIOption(nextOption);
                }
            }

            timeThatHasPassed += Time.unscaledDeltaTime;
            yield return null;
        }
        this.isCurrentlyAutoScrolling = false;
        yield break;
    }
}
