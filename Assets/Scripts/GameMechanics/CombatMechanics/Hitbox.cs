using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script that handles any type of hitbox functionality. This will check for interactions with hitboxes or hurtboxes
/// </summary>
public abstract class Hitbox : MonoBehaviour
{
    public CharacterStats associatedCharacterStats { get; set; }
    public UnityEvent<Hurtbox> onHurtboxEnteredEvent = new HitboxOnHurtboxEvent();
    public UnityEvent<Hitbox> onHitboxEnteredEvent = new HitboxOnHitboxEvent();
    public UnityEvent<Collider2D> onCollisionEvent = new HitboxOnCollider2DEvent();

    #region monobehaviour methods
    protected virtual void Awake()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Hitbox");
    }

    
    #endregion monobehaviour methods

    private void SetupHitbox(CharacterStats associatedCharacterStats)
    {
        this.associatedCharacterStats = associatedCharacterStats;
    }

    #region hitbox events
    public class HitboxOnHitboxEvent : UnityEvent<Hitbox>
    {

    }

    public class HitboxOnHurtboxEvent : UnityEvent<Hurtbox>
    {

    }

    public class HitboxOnCollider2DEvent: UnityEvent<Collider2D>
    {

    }
    #endregion hitbox events
}
