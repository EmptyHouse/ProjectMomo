using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagedPlayer : TimeManagedObject {

    public bool isTimeSlowed = false;

    public float scaleForPlayers = .9f;
    public float scaledTimeForEverythingElse = .1f;
    [Tooltip("The maximum value that our time control can be set to")]
    public float maxTimeControlMeter = 100;

    [Tooltip("The rate at which our")]
    public float refreshRate = 10f;
    [Tooltip("The rate at which our time control meter will deplete. Measured in Units per second")]
    public float depleteRate = 35f;

    private float currentTimeControlMeter = 0;

    #region monobehaviour methods
    private void Awake()
    {
        currentTimeControlMeter = maxTimeControlMeter;
    }
    #endregion monobehaviour methods

    /// <summary>
    /// Although this method is found here, the only character that should have control of time is
    /// the player. The reason it is in the generic TimeManagedObject is in case there are other NPCs that potentially
    /// may have access to time control
    /// </summary>
    public virtual void OnTimeControlToggled()
    {
        
        isTimeSlowed = !isTimeSlowed;
        if (!isTimeSlowed)
        {
            CustomTime.ResetAllScaledTime();
            StartCoroutine(RefreshTimeControlMeter());

        }
        else
        {
            //CustomTime.SetScaledTime(CustomTime.TimeLayer.Player, scaleForPlayers);
            //CustomTime.SetScaledTime(CustomTime.TimeLayer.Enemy, scaledTimeForEverythingElse);
            //CustomTime.SetScaledTime(CustomTime.TimeLayer.World, scaledTimeForEverythingElse);
            StartCoroutine(MoveTowardSlowerTime());
            StartCoroutine(DepleteTimeControlMeter());
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator DepleteTimeControlMeter()
    {
        while (isTimeSlowed && currentTimeControlMeter > 0)
        {
            currentTimeControlMeter -= CustomTime.DeltaTime * depleteRate;
            if (currentTimeControlMeter <= 0)
            {
                OnTimeControlToggled();
                currentTimeControlMeter = 0;
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator RefreshTimeControlMeter()
    {
        while (!isTimeSlowed && currentTimeControlMeter < maxTimeControlMeter)
        {
            currentTimeControlMeter += CustomTime.DeltaTime * refreshRate;
            if (currentTimeControlMeter >= maxTimeControlMeter)
            {
                currentTimeControlMeter = maxTimeControlMeter;
                yield break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Gradually moves toward the specified rate that we want to slow our time scale to.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveTowardSlowerTime()
    {
        float timeToReachDesiredSlowTime = .35f;
        float timeThatHasPassed = 0;

        float currentScaleForPlayer = 1;
        float currentScaleForEverythingElse = 1;
        float differenceBetweenScaleForPlayer = 1 - scaleForPlayers;
        float differnceBetweenScaleForWorld = 1 - scaledTimeForEverythingElse;
        while (timeThatHasPassed < timeToReachDesiredSlowTime)
        {
            timeThatHasPassed += CustomTime.DeltaTime;
            currentScaleForPlayer = 1 - (timeThatHasPassed / timeToReachDesiredSlowTime) * differenceBetweenScaleForPlayer;
            currentScaleForEverythingElse = 1 - (timeThatHasPassed / timeToReachDesiredSlowTime) * differnceBetweenScaleForWorld;
            CustomTime.SetScaledTime(CustomTime.TimeLayer.Player, currentScaleForPlayer);
            CustomTime.SetScaledTime(CustomTime.TimeLayer.Enemy, currentScaleForEverythingElse);
            CustomTime.SetScaledTime(CustomTime.TimeLayer.World, currentScaleForEverythingElse);
            yield return null;

        }
        CustomTime.SetScaledTime(CustomTime.TimeLayer.Player, scaleForPlayers);
        CustomTime.SetScaledTime(CustomTime.TimeLayer.Enemy, scaledTimeForEverythingElse);
        CustomTime.SetScaledTime(CustomTime.TimeLayer.World, scaledTimeForEverythingElse);
    }
}
