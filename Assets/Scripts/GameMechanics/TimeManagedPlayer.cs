using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagedPlayer : TimeManagedObject {

    public bool isTimeSlowed = false;

    public float scaleForPlayers = .9f;
    public float scaledTimeForEverythingElse = .1f;
    public float timeControlMeter = 100;

    /// <summary>
    /// Although this method is found here, the only character that should have control of time is
    /// the player. The reason it is in the generic TimeManagedObject is in case there are other NPCs that potentially
    /// may have access to time control
    /// </summary>
    public virtual void OnTimeControlToggled()
    {
        if (isTimeSlowed)
        {
            CustomTime.ResetAllScaledTime();
        }
        else
        {
            //CustomTime.SetScaledTime(CustomTime.TimeLayer.Player, scaleForPlayers);
            //CustomTime.SetScaledTime(CustomTime.TimeLayer.Enemy, scaledTimeForEverythingElse);
            //CustomTime.SetScaledTime(CustomTime.TimeLayer.World, scaledTimeForEverythingElse);
            StartCoroutine(MoveTowardSlowerTime());
        }
        isTimeSlowed = !isTimeSlowed;
    }

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
            timeThatHasPassed += CustomTime.GetTimeLayerAdjustedDeltaTime(CustomTime.TimeLayer.DeltaTime);
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
