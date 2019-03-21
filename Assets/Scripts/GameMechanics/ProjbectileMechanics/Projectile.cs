using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomPhysics2D))]
public class Projectile : MonoBehaviour
{
    [Tooltip("The speed at which our projectile will launch when it has fired")]
    public float speedToLaunchProjectile = 5f;


    private CustomPhysics2D rigid;
    /// <summary>
    /// The direction that this projectile will launch when the Launch projectile value has been set
    /// </summary>
    private Vector2 directionToFireProjectile = Vector2.one;
    /// <summary>
    /// The associated character stats that are assigned to this projectile
    /// </summary>
    public CharacterStats associatedCharacterStats { get; private set; }


    protected virtual void Awake()
    {
        rigid = GetComponent<CustomPhysics2D>();
    }

    protected virtual void Update()
    {
        UpdateRotationOfProjectileToVelocity();
    }


    public void SetupProjectile(CharacterStats associatedCharacterStats)
    {
        this.associatedCharacterStats = associatedCharacterStats;
    }

    public void LaunchProjectile(Vector2 directionToLaunchProjectile)
    {
        rigid.velocity = directionToLaunchProjectile * speedToLaunchProjectile;
        UpdateRotationOfProjectileToVelocity();
    }

    protected virtual void UpdateRotationOfProjectileToVelocity()
    {
        float x = rigid.velocity.x;
        float y = rigid.velocity.y;

        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(y, x));
    }
}
