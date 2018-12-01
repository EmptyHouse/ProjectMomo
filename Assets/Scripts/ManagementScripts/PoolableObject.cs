using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PoolableObject {
    /// <summary>
    /// Any setup and preparation that should be taken care of when a poolable object is spawned should take place here
    /// </summary>
    void OnObjectSpawned();

    /// <summary>
    /// Any clean up that should occur when our object is despawned should occur here. Essentially can act as the destroy method, but
    /// be sure to keep in mind that this object may be reused so be careful when running clean up
    /// </summary>
    void OnObjectDespawned();

    /// <summary>
    /// Spawnable objects must be monobehaviour objects. We will need to have access to the attached gameobject for poolable objects
    /// to function correctly
    /// </summary>
    MonoBehaviour attachedMonobehaviour { get; }
}
