using System;
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
    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";

    public const string SELECT_BUTTON = "Select";
    public const string CANCEL_BUTTON = "Cancel";

    /// <summary>
    /// This is the time before we begin autoscrolling in our menu. We will always move to a new item immediately,
    /// but we may want a delay before scrolling to different optins automatically
    /// </summary>
    public const float TIME_BEFORE_AUTO_SCROLLING = .4f;
    /// <summary>
    /// The time between selecting the next item when we begin autoscrolling.
    /// </summary>
    public const float TIME_BETWEEN_AUTO_SCROLL_ITEM = .1f;


    #endregion const variables
    #region main variables
    [Tooltip("All connected selectable UI elements")]
    public SelectableUI[] allSelectableUI;
    [Tooltip("The menu item that we will select upon opening the menu. If resetToInitialUIUponOpening is set to false, we will remain on the previous menu option whenever this menu is opened again")]
    public SelectableUI initiallySelectedUI;
    [Tooltip("This will make it so that we begin our menu on the initiallySelectedUI object whenever we open our menu")]
    public bool resetToInitialUIUponOpening;
    //[Tooltip("The transform parent that contains all the UI elements related to this menu")]
    public bool allowMenuToWrap = false;
    #endregion main variables
    
    
    public SelectableUI currentlySelectedUI { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    protected bool isCurrentlyAutoScrolling
    {
        get
        {
            return isAutoScrollingHorizontally || isAutoScrollingVertically;
        }
    }

    private bool isAutoScrollingVertically;
    private bool isAutoScrollingHorizontally;

    #region monobehaviour methods

    private void Awake()
    {
        SetNextSelectableUIOption(initiallySelectedUI);
        ConnectSelectableUIElements();
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

    protected virtual void OnEnable()
    {
        isAutoScrollingHorizontally = false;
        isAutoScrollingVertically = false;
    }
    #endregion monobehaviour methods

    protected virtual void ConnectSelectableUIElements()
    {
        if (allSelectableUI.Length <= 1) return;
        for (int i = 1; i < allSelectableUI.Length; i++)
        {
            if (allSelectableUI[i - 1] != null)
                allSelectableUI[i - 1].southSelectableUI = allSelectableUI[i];
            if (allSelectableUI[i] != null)
                allSelectableUI[i].northSelectableUI = allSelectableUI[i - 1];
        }
        if (allowMenuToWrap)
        {
            if (allSelectableUI[0] != null) 
                allSelectableUI[0].northSelectableUI = allSelectableUI[allSelectableUI.Length - 1];
            if (allSelectableUI[allSelectableUI.Length - 1])
                allSelectableUI[allSelectableUI.Length - 1].southSelectableUI = allSelectableUI[0];
        }
    }

    #region input methods
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool SelectButtonDown()
    {
        return Input.GetButtonDown(SELECT_BUTTON);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool CancelButtonDown()
    {
        return Input.GetButtonDown(CANCEL_BUTTON);
    }

    /// <summary>
    /// Returns the vertical axis
    /// </summary>
    /// <returns></returns>
    public float VerticalAxis()
    {
        return Input.GetAxisRaw(VERTICAL_AXIS);
    }

    /// <summary>
    /// Returns the horizontal axis
    /// </summary>
    /// <returns></returns>
    public float HorizontalAxis()
    {
        return Input.GetAxisRaw(HORIZONTAL_AXIS);
    }

    /// <summary>
    /// Returns whether or not the axis value that is passed in is above the threhold
    /// </summary>
    /// <param name="axisInput"></param>
    /// <returns></returns>
    protected bool JoystickAboveThreshold(float axisInput)
    {
        return Mathf.Abs(axisInput) > JOYSTICK_THRESHOLD;
    }

    /// <summary>
    /// Returns whether or not our joystick is above the joystick threshold based on its original sign. This
    /// is primarily used strictly 
    /// </summary>
    /// <param name="axisInput"></param>
    /// <param name="originalSign"></param>
    /// <returns></returns>
    protected bool JoystickAboveThresholdSign(float axisInput, float originalSign)
    {
        return (originalSign * axisInput) > JOYSTICK_THRESHOLD;
    }
    #endregion input methods
    private SelectableUI GetNextOption(SelectableUI.UIDirection directionToCheck)
    {
        SelectableUI optionToCheck = currentlySelectedUI.GetUIInDirection(directionToCheck);
        return optionToCheck;
        //while (optionToCheck != null && !currentlyCheckedOptions.Contains(optionToCheck))
        //{

        //}
    }

    protected virtual void SelectSettingsUIElementInDirection(int x, int y)
    {
        if (x > 0)
        {

        }
        else if (x < 0)
        {

        }

        if (y > 0)
        {

        }
        else if (y < 0)
        {

        }
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
    private IEnumerator BeginAutoScrollingVertical(float verticalInput)
    {
        int direction = (int)Mathf.Sign(verticalInput);
        isAutoScrollingVertically = true;
        float timeThatHasPassed = 0;
        while (timeThatHasPassed > TIME_BEFORE_AUTO_SCROLLING)
        {
            if (JoystickAboveThresholdSign(VerticalAxis(), direction))
            {
                isAutoScrollingVertically = false;
                yield break;
            }

            timeThatHasPassed += CustomTime.UnscaledDeltaTime;
            yield return null;
        }


        timeThatHasPassed = 0;
        while (JoystickAboveThresholdSign(VerticalAxis(), direction))
        {
            if (timeThatHasPassed > TIME_BETWEEN_AUTO_SCROLL_ITEM)
            {
                timeThatHasPassed = 0;

            }
        }
        isAutoScrollingVertically = false;
    }

    private IEnumerator BeginAutoScrollingHorizontal(float horizontalInput)
    {
        int direction = (int)Mathf.Sign(horizontalInput);
        isAutoScrollingHorizontally = true;
        float timeThatHasPassed = 0;
        while (timeThatHasPassed > TIME_BEFORE_AUTO_SCROLLING)
        {
            if (JoystickAboveThresholdSign(HorizontalAxis(), direction))
            {
                isAutoScrollingHorizontally = false;
                yield break;
            }

            timeThatHasPassed += CustomTime.UnscaledDeltaTime;
            yield return null;
        }


        timeThatHasPassed = 0;
        while (JoystickAboveThresholdSign(HorizontalAxis(), direction))
        {
            if (timeThatHasPassed > TIME_BETWEEN_AUTO_SCROLL_ITEM)
            {
                timeThatHasPassed = 0;

            }
        }
        isAutoScrollingHorizontally = false;
    }

    /// <summary>
    /// Due to a lot of UI menus opening immediately and using similar button presses to perform actions, it is a good idea to
    /// allow a one frame buffer between opening menus to allow button down inputs to clear
    /// </summary>
    /// <returns></returns>
    private IEnumerator PerformActionAfterOneFrame(Action actionToPerformAfterOneFrame)
    {
        yield return null;
        actionToPerformAfterOneFrame.Invoke();
    }
}
