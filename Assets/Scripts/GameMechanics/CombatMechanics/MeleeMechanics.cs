using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMechanics : MonoBehaviour
{
    public const string MELEE_TRIGGER = "MeleeTrigger";
    [Tooltip("This is the leeway time we will give our player to use the attack functionality. For example if we set a 1 second buffer, the player will then have 1 whole second to activate an attack animation after they press the attack button.")]
    public float attackBufferedTime;
    /// <summary>
    /// This is a timer that is used to deactivate an attack trigger if it has been longer than the attackBufferedTime.
    /// </summary>
    private float bufferedAttackTriggerTimer;
    public CharacterStats associatedCharacterStats;
    public Hitbox[] allConnectedHitboxes;
    private List<CharacterStats> characterStatsThatHaveBeenHitSinceAttackAnimation;

    private Animator anim
    {
        get
        {
            return associatedCharacterStats.characterAnimator;
        }
    }
    #region monobehaviour methods
    private void Awake()
    {
        associatedCharacterStats = GetComponent<CharacterStats>();
        foreach (Hitbox hitbox in allConnectedHitboxes)
        {
            hitbox.onHurtboxEnteredEvent.AddListener(EnemyHit);
        }
    }

    private void OnDestroy()
    {
        foreach(Hitbox hitbox in allConnectedHitboxes)
        {
            hitbox.onHurtboxEnteredEvent.RemoveListener(EnemyHit);
        }
    }
    #endregion monobehaviuor methods



    /// <summary>
    /// This will set an attack trigger if the player has pressed the attack button
    /// </summary>
    public void AttackButtonPressed()
    {
        if (bufferedAttackTriggerTimer <= 0)
        {
            bufferedAttackTriggerTimer = attackBufferedTime;
            StartCoroutine(DeactivateAttackTriggerAfterBufferedTimeHasPassed());
        }
    }

    /// <summary>
    /// Whenever an enemy hurtbox is hit, we should call this method
    /// </summary>
    /// <param name="hurtboxOfEnemy"></param>
    public void EnemyHit(Hurtbox hurtboxOfEnemy)
    {
        if (hurtboxOfEnemy.associatedCharacterStats.hitboxLayer != associatedCharacterStats.hitboxLayer)
        {

        }
    }

    /// <summary>
    /// Coroutine used for buffered inputs. This will disable a trigger if we have gone over the amount of time
    /// indicated by the value attackBufferTime. This can be reset
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeactivateAttackTriggerAfterBufferedTimeHasPassed()
    {
        while (bufferedAttackTriggerTimer > 0)
        {
            bufferedAttackTriggerTimer -= CustomTime.DeltaTime;
            if (bufferedAttackTriggerTimer <= 0)
            {

            }
            yield return null;
        }

    }
}
