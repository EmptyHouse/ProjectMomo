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
    private float bufferedAttackTriggerTimer = .2f;
    public CharacterStats associatedCharacterStats { get; set; }
    public Hitbox[] allConnectedHitboxes;
    private List<CharacterStats> characterStatsThatHaveBeenHitSinceAttackAnimation = new List<CharacterStats>();
    public MeleeAttackInformation[] meleeAttackInformation = new MeleeAttackInformation[1];
    [Tooltip("This will be the attack index that we will use ")]
    private int currentSelectedMeleeAttackID;

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
            hitbox.associatedCharacterStats = associatedCharacterStats;
        }
    }

    private void OnValidate()
    {
        if (meleeAttackInformation.Length < 1)
        {
            meleeAttackInformation = new MeleeAttackInformation[1];
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
    /// This method should be called any time we set the melee attack information, but you may also call this
    /// if there is a multi hit move in an attack animation.
    /// </summary>
    private void OnResetMeleeAttack()
    {
        characterStatsThatHaveBeenHitSinceAttackAnimation.Clear();
    }

    /// <summary>
    /// This will set what attack information to use from our MeleeAttackInformation list.
    /// </summary>
    /// <param name="meleeAttackID"></param>
    public void OnSetMeleeAttackInformationToUse(int meleeAttackID)
    {
        OnResetMeleeAttack();
        if (meleeAttackID < 0 || meleeAttackID >= meleeAttackInformation.Length)
        {
            meleeAttackID = 0;
            Debug.LogError("Melee Attack ID was set out of range from our attack information list");
        }
        currentSelectedMeleeAttackID = meleeAttackID;
    }

    /// <summary>
    /// This will set an attack trigger if the player has pressed the attack button
    /// </summary>
    public void AttackButtonPressed()
    {
        float previousBufferedTime = bufferedAttackTriggerTimer;
        bufferedAttackTriggerTimer = attackBufferedTime;
        anim.SetTrigger(MELEE_TRIGGER);
        if (previousBufferedTime <= 0)
        {
            StartCoroutine(DeactivateAttackTriggerAfterBufferedTimeHasPassed());
        }
        
    }

    /// <summary>
    /// Whenever an enemy hurtbox is hit, we should call this method
    /// </summary>
    /// <param name="hurtboxOfEnemy"></param>
    public void EnemyHit(Hurtbox hurtboxOfEnemy)
    {
        if (characterStatsThatHaveBeenHitSinceAttackAnimation.Contains(hurtboxOfEnemy.associatedCharacterStats))
        {
            return;
        }
        characterStatsThatHaveBeenHitSinceAttackAnimation.Add(hurtboxOfEnemy.associatedCharacterStats);
        hurtboxOfEnemy.associatedCharacterStats.TakeDamage(meleeAttackInformation[currentSelectedMeleeAttackID].baseDamageToDeal);
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

    /// <summary>
    /// This will contain all the information for any given attack type
    /// </summary>
    [System.Serializable]
    public class MeleeAttackInformation
    {
        [Tooltip("A reference to the ID of this attack information. Can also be used as the index")]
        private int attackID;
        [Tooltip("The damage that will be dealt to an enemy that is hit during this attack animation")]
        public int baseDamageToDeal = 5;
        [Tooltip("The direction that we will hit enemies when they are hit by a specific attack")]
        public Vector2 directionOfAttack;
        [Tooltip("The launch force that we will send our enemy upon being hit")]
        public float forceOfAttack;
        [Tooltip("The amount of time that our enemy will be in hit stun. During this time, they can not make any moves and can be comboed")]
        public float timeInHitStun = 0;
    }
}
