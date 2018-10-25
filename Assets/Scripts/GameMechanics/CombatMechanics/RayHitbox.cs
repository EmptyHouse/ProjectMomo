using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// There may be some instances where we may want an object to move very quickly through the world and
/// do damage to another object. With a typical hitbox, if they are moving fast enough, it is possible
/// that the hitboxes never interesct. For cases like this, it is best to use a ray hitbox
/// </summary>
public class RayHitbox : Hitbox {
    #region main variables
    [Tooltip("The number of rays that we will fire to see what we interact with")]
    public int rayCount = 2;
    [Tooltip("The length of our raycasts.")]
    public float rayDistance;
    #endregion main variables
    #region monobehaviour methods
    private void Update()
    {
        
    }

    private void OnValidate()
    {
        if (rayCount < 1)
        {
            rayCount = 1;
        }
    }
    #endregion monobehaviour methods

    /// <summary>
    /// 
    /// </summary>
    public void RayCheckHitboxEntered()
    {
        RaycastHit2D hit;
        Vector3 position = transform.position;
        Vector3 forwardDirection = transform.right;
        Ray2D ray = new Ray2D(new Vector2(position.x, position.y), forwardDirection);

        hit = Physics2D.Raycast(ray.origin, ray.direction, rayDistance);
        if (hit)
        {
            Hitbox hitBox = hit.collider.GetComponent<Hitbox>();
            Hurtbox hurtBox = hit.collider.GetComponent<Hurtbox>();

            if (hitBox)
            {
                OnEnteredHitbox(hitBox);
            }
            if (hurtBox)
            {
                OnEnteredHurtbox(hurtBox);
            }
        }
    }

    #region debug methods

    #endregion debug methods
}
