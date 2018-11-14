using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTime : MonoBehaviour {
    #region enums
    public enum TimeLayer
    {
        Player,
        Enemy,
        World,
        UnscaledDeltaTime,
        DeltaTime,
    }
    #endregion enums

    #region main variables
    private static Dictionary<TimeLayer, float> timeScaleDictionary = new Dictionary<TimeLayer, float>();
    #endregion main variables

    /// <summary>
    /// Gets the scaled time based on the category that was passed in
    /// </summary>
    /// <param name="timeCategory"></param>
    /// <returns></returns>
    public static float GetScaledTime(TimeLayer timeCategory)
    {
        switch (timeCategory)
        {
            case TimeLayer.UnscaledDeltaTime:
                return Time.unscaledDeltaTime;
            case TimeLayer.DeltaTime:
                return Time.deltaTime;
            default:
                return Time.unscaledDeltaTime * timeScaleDictionary[timeCategory];

        }
    }


    /// <summary>
    /// Set the scaled time for the passed in time category
    /// </summary>
    /// <param name="timeCategory"></param>
    /// <param name="scaledTime"></param>
    public static void SetScaledTime(TimeLayer timeCategory, float scaledTime)
    {
        switch (timeCategory)
        {
            case TimeLayer.UnscaledDeltaTime:
            case TimeLayer.DeltaTime:
                Debug.LogWarning("You can not change the scaled time of " + timeCategory.ToString());
                return;
            default:
                timeScaleDictionary[timeCategory] = scaledTime;
                break;
        }
    }
}
