using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Hurtbox : MonoBehaviour
{
    public CharacterStats associatedCharacterStats { get; private set; }
    public Collider2D associatedCollider { get; private set; }

    public UnityEvent<Hurtbox> onHurtboxEnteredEvent = new HurtboxOnHurtboxEvent();
    public UnityEvent<Hitbox> onHitboxEnteredEvent = new HurtboxOnHitboxEvent();

    private void Awake()
    {
        this.associatedCharacterStats = GetComponentInParent<CharacterStats>();
        this.associatedCollider = GetComponent<Collider2D>();
        associatedCollider.isTrigger = true;
        this.gameObject.layer = LayerMask.NameToLayer("Hitbox");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Hitbox hitbox = collider.GetComponent<Hitbox>();
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();

        if (hitbox != null)
        {
            //if (hitbox.associatedCharacterStats == null)
            //{
            //    Debug.LogError(hitbox.name + ": Hitbox missing an associated character stats reference");
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
            //    Debug.LogError(hurtbox.name + ": Hurtbox missing associated character stats reference");
            //    return;
            //}
            if (hurtbox.associatedCharacterStats.hitboxLayer == this.associatedCharacterStats.hitboxLayer) { return; }
            onHurtboxEnteredEvent.Invoke(hurtbox);
            return;
        }
    }

    #region hurtbox evnets
    public class HurtboxOnHurtboxEvent : UnityEvent<Hurtbox>
    {

    }

    public class HurtboxOnHitboxEvent : UnityEvent<Hitbox>
    {

    }
    #endregion hurtbox events
}
