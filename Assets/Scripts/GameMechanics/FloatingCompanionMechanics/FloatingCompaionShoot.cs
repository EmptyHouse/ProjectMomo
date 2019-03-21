using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCompaionShoot : MonoBehaviour {
    public Projectile bulletToFire;
    private const float JOYSTICK_THRESHOLD = .55f;
    private const string FIRE_PROJECTILE = "FireProjectile";
    public float delayBetweenShots = .2f;
    public float distanceAwayFromCenterArrow;
    private bool isArrowActive = false;

    public SpriteRenderer arrowSpriteRenderer;
    private bool isFiring;
    private Vector2 currentDirectionToFire;
    private Vector2 rawInputFromPlayer;
    FloatingCompanionMovement movement;

    private void Awake()
    {
        arrowSpriteRenderer.color = new Color(1, 1, 1, 0);
        isArrowActive = false;
        movement = GetComponent<FloatingCompanionMovement>();
    }

    private void Update()
    {
        if (!isFiring)
        {
            if (FireButtonDown())
            {
                StartCoroutine(FireProjectileRepeated());
            }
        }
        UpdateDirectionToFire();
    }


    private void FireBullet()
    {
        Projectile createdBullet = Instantiate<Projectile>(bulletToFire);
        createdBullet.transform.position = this.transform.position;
        createdBullet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(currentDirectionToFire.y, currentDirectionToFire.x));
        createdBullet.SetupProjectile(GameOverseer.Instance.playerCharacterStats);
        createdBullet.LaunchProjectile(currentDirectionToFire);

        
    }

    private void UpdateDirectionToFire()
    {
        float rX = Input.GetAxisRaw("RStickHorizontal");
        float rY = Input.GetAxisRaw("RStickVertical");
        rawInputFromPlayer = new Vector2(rX, rY);

        if (rawInputFromPlayer.magnitude > JOYSTICK_THRESHOLD)
        {
            if (!isArrowActive)
            {
                isArrowActive = true;
                arrowSpriteRenderer.color = new Color(1, 1, 1, 1);
            }
            currentDirectionToFire = rawInputFromPlayer.normalized;
            arrowSpriteRenderer.transform.position = new Vector2(transform.position.x, transform.position.y) + (currentDirectionToFire * distanceAwayFromCenterArrow);
            arrowSpriteRenderer.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(currentDirectionToFire.x, -currentDirectionToFire.y) * Mathf.Rad2Deg);
        }
        else
        {
            if (isArrowActive)
            {
                isArrowActive = false;
                StartCoroutine(FadeOutArrow());
            }
            if (!isFiring)
                currentDirectionToFire = new Vector2(movement.isFacingRight ? 1 : -1, 0);
        }
    }

    public bool FireButtonDown()
    {
        return Input.GetAxisRaw(FIRE_PROJECTILE) > JOYSTICK_THRESHOLD || Input.GetButton(FIRE_PROJECTILE);
    }


    private IEnumerator FireProjectileRepeated()
    {
        isFiring = true;
        FireBullet();
        float timeThatHasPassed = 0;
        while (FireButtonDown())
        {
            timeThatHasPassed += CustomTime.DeltaTime;
            yield return null;
            if (timeThatHasPassed > delayBetweenShots)
            {
                FireBullet();
                timeThatHasPassed = 0;
            }
        }
        isFiring = false;
    }

    private IEnumerator FadeOutArrow()
    {
        float timeToFadeArrow = 2;
        float initialTimeToFade = timeToFadeArrow;
        while (timeToFadeArrow > 0)
        {
            arrowSpriteRenderer.color = new Color(1, 1, 1, timeToFadeArrow / initialTimeToFade);
            timeToFadeArrow -= CustomTime.DeltaTime;

            yield return null;
            if (rawInputFromPlayer.magnitude > JOYSTICK_THRESHOLD)
            {
                yield break;
            }
        }
    }
}
