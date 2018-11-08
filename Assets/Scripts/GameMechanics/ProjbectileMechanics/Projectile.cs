using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    #region main variables
    [Tooltip("The desired launch speed of our projectile")]
    public float launchSpeed = 15f;
    /// <summary>
    /// A reference to the character stats of the character that laumched this projectile
    /// </summary>
    public CharacterStats associatedCharacterStats { get; set; }
    private CustomPhysics2D rigid;
    #endregion main variables

    #region monobehaviour methods
    private void Start()
    {
        
    }
    #endregion monobehaviour methods
    #region setup methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="characterStats"></param>
    public void SetupProjectile(CharacterStats characterStats)
    {
        this.associatedCharacterStats = characterStats;
        rigid = GetComponent<CustomPhysics2D>();
    }
    /// <summary>
    /// This will send our projectile to move forward based on the desired speed and the rotation of our projectile
    /// </summary>
    public virtual void LaunchProjectile()
    {
        bool isRight = associatedCharacterStats.movementMechanics.isFacingRight;
        Vector2 forwardVectorNormalized = new Vector2((isRight ? 1 : -1) * this.transform.right.x, this.transform.right.y);
        rigid.velocity = forwardVectorNormalized * launchSpeed;

    }
    #endregion setup methods

}
