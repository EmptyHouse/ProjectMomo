using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTime : MonoBehaviour{
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

    #region static references
    private static CustomTime instance;
    public static CustomTime Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<CustomTime>();
            }
            return instance;
        }
    }
    #endregion static references

    #region main variables
    private static Dictionary<TimeLayer, float> timeScaleDictionary = new Dictionary<TimeLayer, float>();
    #endregion main variables

    #region monobehaviour methods
    private void Awake()
    {
        
    }
    #endregion monobehaviour methods

    static CustomTime()
    {
        foreach (TimeLayer timeLayer in System.Enum.GetValues(typeof(TimeLayer)))
        {
            timeScaleDictionary.Add(timeLayer, 1);
        }
    }

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
                return Time.deltaTime * timeScaleDictionary[timeCategory];

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

    public static float GetDeltaTimeScale(TimeLayer timeLayer)
    {
        switch (timeLayer)
        {
            case TimeLayer.DeltaTime:
                return Time.timeScale;
            case TimeLayer.UnscaledDeltaTime:
                return 1;
            default:
                return Time.timeScale * timeScaleDictionary[timeLayer];

        }
    }

    /// <summary>
    /// Sets the scale of the real delta time. Real delta time referring to Unity's delta time
    /// </summary>
    public static void SetRealDeltaTimeScale(float deltaTimeScale)
    {
        Time.timeScale = deltaTimeScale;
    }

    private IEnumerator ScaleDeltaTimeToGoalTime()
    {
        yield break;
    }
}
