using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherited class of Hitbox. This hitbox will cast rays instead of use a trigger box like a normal hitbox. 
/// </summary>
public class RayHitbox : Hitbox
{
    [Tooltip("The width of our ray hitbox object. Keep in mind that you may need more rays to shoot the wider you make the hitbox")]
    public float widthOfRaycast;
    [Tooltip("The distance from the origin that we will send our raycast. Keep in mind that raycast will always be sent forward based on the tranform")]
    public float distanceOfRaycast;
    [Tooltip("The number of rays we will fire")]
    public float rayCount;


    private void OnValidate()
    {
        if (rayCount < 2) rayCount = 2;
    }

    private void Update()
    {
        
    }

    public bool CheckRayIntersectHitbox(Vector3 originPoint)
    {
        Ray2D ray = new Ray2D(originPoint, transform.forward);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, distanceOfRaycast, LayerMask.GetMask(HITBOX_LAYER));
        if (!rayHit)
        {
            return false;
        }
        Collider2D collider = rayHit.collider;
        Hitbox hitbox = collider.GetComponent<Hitbox>();
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();

        if (hitbox != null)
        {
            //if (hitbox.associatedCharacterStats == null)
            //{
            //    Debug.LogError(hitbox.name + " contains a null associated CharacterStats reference");
            //    return;
            //}
            if (hitbox.associatedCharacterStats.hitboxLayer == this.associatedCharacterStats.hitboxLayer) { return false; }
            onHitboxEnteredEvent.Invoke(hitbox);
            return true;
        }
        if (hurtbox != null)
        {
            //if (hurtbox.associatedCharacterStats == null)
            //{
            //    Debug.LogError(hurtbox.name + " contains a null associated CharacterStats reference");
            //    return;
            //}
            if (hurtbox.associatedCharacterStats.hitboxLayer == this.associatedCharacterStats.hitboxLayer) { return false; }
            onHurtboxEnteredEvent.Invoke(hurtbox);
            return true;
        }
        onCollisionEvent.Invoke(collider);
        return true;
    }

    private void DebugDrawRayLines()
    {

    }
}
