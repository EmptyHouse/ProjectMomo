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
    private RayHitbox rayHitbox;
    private SpriteRenderer spriteRenderer;
    #endregion main variables

    #region monobehaviour methods
    private void Start()
    {
        rayHitbox = GetComponentInChildren<RayHitbox>();
        rayHitbox.onHitboxCollisionEnteredEvent += OnProjectileCollision;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (!spriteRenderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        rayHitbox.onHitboxCollisionEnteredEvent -= OnProjectileCollision;
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
        Vector3 currentScale = this.transform.localScale;
        this.transform.localScale = new Vector3((isRight ? 1 : -1) * Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        rigid.velocity = forwardVectorNormalized * launchSpeed;

    }
    #endregion setup methods

    private void OnProjectileCollision(CharacterStats characterThatWasHit, Vector3 pointOfImpact)
    {
        if (!characterThatWasHit)
        {
            
        }
        this.transform.position = pointOfImpact - (rayHitbox.transform.position - this.transform.position);
        rigid.enabled = false;
        rayHitbox.enabled = false;
        StartCoroutine(FadeOutAndDestroy());
    }


    private IEnumerator FadeOutAndDestroy()
    {
        float timeToFadeOut = 1;
        float timeThatHasPassed = 0;
        Color originalColor = spriteRenderer.color;
        while (timeThatHasPassed < timeToFadeOut)
        {
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - (timeThatHasPassed / timeToFadeOut));
            timeThatHasPassed += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = originalColor;
        Destroy(this.gameObject);
    }
}
