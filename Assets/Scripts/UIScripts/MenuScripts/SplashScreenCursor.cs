using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script handles placing a cursor over the currently selected UI option. This indicates which option is available to the player when
/// they press the submit button
/// </summary>
public class SplashScreenCursor : MonoBehaviour {
    public float lerpSpeed;
    public SelectableUIManager menuManager;
    public Image cursorRight;
    public Image cursorLeft;

    private void Start()
    {
        cursorRight.rectTransform.position = GetGoalPositionRight();
        cursorLeft.rectTransform.position = GetGoalPositionLeft();
    }

    private void Update()
    {
        cursorRight.rectTransform.position = Vector3.Lerp(cursorRight.rectTransform.position, GetGoalPositionRight(), CustomTime.UnscaledDeltaTime * lerpSpeed);
        cursorLeft.rectTransform.position = Vector3.Lerp(cursorLeft.rectTransform.position, GetGoalPositionLeft(), CustomTime.UnscaledDeltaTime * lerpSpeed);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Vector3 GetGoalPositionRight()
    {
        Vector3 goalPositionRight = menuManager.currentlySelectedUI.rectTransform.position ;
        goalPositionRight.x += menuManager.currentlySelectedUI.rectTransform.sizeDelta.x / 2;
        return goalPositionRight;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Vector3 GetGoalPositionLeft()
    {
        Vector3 goalPositionLeft = menuManager.currentlySelectedUI.rectTransform.position;
        goalPositionLeft.x -= menuManager.currentlySelectedUI.rectTransform.sizeDelta.x / 2;
        return goalPositionLeft;
    }
}
