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
    [Tooltip("The distance between all vector points in the ray origins")]
    public float raySpread;
    private CustomPhysics2D rigid;
    #endregion main variables
    #region monobehaviour methods
    private void Start()
    {
        rigid = GetComponentInParent<CustomPhysics2D>();
    }

    private void Update()
    {
        RayCheckHitboxEntered();
    }

    private void OnValidate()
    {
        if (rayCount < 2)
        {
            rayCount = 2;
        }
    }

    /// <summary>
    /// Provices a visual display of our ray hitbox
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Vector3 originPoint = transform.position;

        Vector3 topPoint = originPoint + this.transform.up * (raySpread / 2);
        Vector3 bottomPoint = originPoint - this.transform.up * (raySpread / 2);

        DebugSettings.DrawLine(topPoint, bottomPoint, Color.red);

        Vector3 incrementVector = (bottomPoint - topPoint) / rayCount;
        for (int i = 0; i < rayCount; i++)
        {
            DebugSettings.DrawLineDirection(topPoint + (incrementVector * i), this.transform.right, rayDistance, Color.red);
        }
    }
    #endregion monobehaviour methods
    /// <summary>
    /// 
    /// </summary>
    public void RayCheckHitboxEntered()
    {
        Vector3 originPoint = transform.position;
        Vector3 bottomPoint = originPoint - this.transform.up * (raySpread / 2);
        Vector3 topPoint = originPoint + this.transform.up * (raySpread / 2);

        Vector3 incrementVector = (bottomPoint - topPoint) / rayCount;

        RaycastHit2D hit;
        Vector3 position = transform.position;
        Vector3 forwardDirection = transform.right;
        Ray2D ray = new Ray2D(new Vector2(position.x, position.y), rigid.velocity.normalized);

        hit = Physics2D.Raycast(ray.origin, ray.direction, rigid.velocity.magnitude * Time.deltaTime);
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
            HitboxEnteredEvent(null, hit.point);
        }
    }

    

    #region debug methods

    #endregion debug methods
}
