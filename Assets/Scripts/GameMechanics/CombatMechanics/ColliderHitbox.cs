using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
/// <summary>
/// Inherited script of hitbox that uses a collider to detect when hitbox event should take place
/// </summary>
public class ColliderHitbox : Hitbox
{
    public Collider2D associatedCollider { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        associatedCollider = GetComponent<Collider2D>();
        associatedCollider.isTrigger = true;
    }

    /// <summary>
    /// Sets off the appropritate triggers when our hitboxes enter a hitbox, hurtbox or generic collider2D
    /// </summary>
    /// <param name="collider"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Hitbox hitbox = collider.GetComponent<Hitbox>();
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();

        if (hitbox != null)
        {
            //if (hitbox.associatedCharacterStats == null)
            //{
            //    Debug.LogError(hitbox.name + " contains a null associated CharacterStats reference");
            //    return;
            //}
            if (hitbox.associatedCharacterStats.hitboxLayer == this.associatedCharacterStats.hitboxLayer) { return; }
            onHitboxEnteredEvent.Invoke(hitbox);
            return;
        }
        if (hurtbox != null)
        {
            //if (hurtbox.associatedCharacterStats == null)
            //{
            //    Debug.LogError(hurtbox.name + " contains a null associated CharacterStats reference");
            //    return;
            //}
            if (hurtbox.associatedCharacterStats.hitboxLayer == this.associatedCharacterStats.hitboxLayer) { return; }
            onHurtboxEnteredEvent.Invoke(hurtbox);
            return;
        }
        onCollisionEvent.Invoke(collider);

    }

    private void OnTriggerExit2D(Collider2D collider)
    {

    }
}
