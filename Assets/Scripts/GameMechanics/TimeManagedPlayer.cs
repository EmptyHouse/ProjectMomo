using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagedPlayer : TimeManagedObject {
    public float scaleForPlayers = .9f;
    public float scaledTimeForEverythingElse = .1f;

    /// <summary>
    /// Although this method is found here, the only character that should have control of time is
    /// the player. The reason it is in the generic TimeManagedObject is in case there are other NPCs that potentially
    /// may have access to time control
    /// </summary>
    public virtual void OnTimeControlToggled()
    {
        CustomTime.SetScaledTime(CustomTime.TimeLayer.Player, scaleForPlayers);
        CustomTime.SetScaledTime(CustomTime.TimeLayer.Enemy, scaledTimeForEverythingElse);
        CustomTime.SetScaledTime(CustomTime.TimeLayer.World, scaledTimeForEverythingElse);
    }
}
