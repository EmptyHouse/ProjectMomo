using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anything that can have time manipulated should include this method. There may be certain methods that need to be handled
/// when time is changed
/// </summary>
public class TimeManagedObject : MonoBehaviour {
    [Tooltip("The time layer of this GameObject")]
    public CustomTime.TimeLayer timeLayer = CustomTime.TimeLayer.DeltaTime;
    public delegate void TimeLayerUpdatedEvent();
    public event TimeLayerUpdatedEvent TimeLayerUpdated;

    public void OnTimeLayerScaleUpdated()
    {
        TimeLayerUpdated();
    }
}
