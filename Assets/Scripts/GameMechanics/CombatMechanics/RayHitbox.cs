using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// There may be some instances where we may want an object to move very quickly through the world and
/// do damage to another object. With a typical hitbox, if they are moving fast enough, it is possible
/// that the hitboxes never interesct. For cases like this, it is best to use a ray hitbox
/// </summary>
public class RayHitbox : Hitbox {

    #region monobehaviour methods
    private void Update()
    {
        
    }
    #endregion monobehaviour methods

    public void CheckHitboxEntered()
    {

    }
}
