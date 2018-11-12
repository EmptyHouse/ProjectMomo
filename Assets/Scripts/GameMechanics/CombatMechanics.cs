using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Logic that relates to a character performing combat related mechanics should be
/// located in this script
/// </summary>
public class CombatMechanics : MonoBehaviour {
    #region const variables
    private const string PROJECTILE_ANIMATION_TRIGGER = "ProjectileTrigger";
    private const string MELEE_ANIMATION_TRIGGER = "MeleeTrigger";
    private const float TIME_TO_BUFFER = 12f * (1f / 60f);
    #endregion const variables

    #region main variables
    public Projectile currentlySelectedProjectile;
    public Transform projectileLaunchTransform;

    private CharacterStats associatedCharacterStats;

    /// <summary>
    /// Associated animator for our character
    /// </summary>
    private Animator anim;
    /// <summary>
    /// This dictionary holds the remaining time left for a buffered input to be active as it 'value' and uses the name of teh animation trigger as its 'key'
    /// </summary>
    private Dictionary<string, float> bufferInputTimer = new Dictionary<string, float>();

    #endregion main variables

    #region monobehaviour methods
    private void Awake()
    {
        anim = GetComponent<Animator>();
        bufferInputTimer.Add(PROJECTILE_ANIMATION_TRIGGER, 0);
        bufferInputTimer.Add(MELEE_ANIMATION_TRIGGER, 0);
        associatedCharacterStats = GetComponent<CharacterStats>();
        associatedCharacterStats.combatMechanics = this;
    }
    #endregion monobehaviour methods

    #region projectile based combat
    /// <summary>
    /// Call this method to begin the animation for firing our arrow
    /// </summary>
    public void FireArrowAnimation()
    {
        anim.SetTrigger(PROJECTILE_ANIMATION_TRIGGER);
        StartCoroutine(BufferCombatAnimationInput(PROJECTILE_ANIMATION_TRIGGER));
    }

    public void ActuallyLaunchProjectile()
    {
        Projectile newlyCreatedProjectiled = Instantiate<Projectile>(currentlySelectedProjectile, projectileLaunchTransform.position, projectileLaunchTransform.rotation);
        newlyCreatedProjectiled.SetupProjectile(associatedCharacterStats);
        
        newlyCreatedProjectiled.LaunchProjectile();
    }
    #endregion projectile based combat

    #region melee combat methods
    /// <summary>
    /// This method should be called whenever our player uses the melee command
    /// </summary>
    public void PerformMeleeAnimation()
    {
        anim.SetTrigger(MELEE_ANIMATION_TRIGGER);
        StartCoroutine(BufferCombatAnimationInput(MELEE_ANIMATION_TRIGGER));
    }
    #endregion melee combat methods
    /// <summary>
    /// This coroutine is used to buffer certain combat commands. This will allow the player seem leniency when trying to read in their
    /// inputs so that it does not require perfect timing. For example a melee combo attack would be very difficult if it required perfect timing
    /// But would also feel unresponsive if the player did not intendo for the 2nd or 3rd hit in the combo to come through.
    /// </summary>
    /// <returns></returns>
    private IEnumerator BufferCombatAnimationInput(string nameOfAttackToBuffer)
    {
        if (bufferInputTimer[nameOfAttackToBuffer] > 0)
        {
            yield break;
        }
        bufferInputTimer[nameOfAttackToBuffer] = TIME_TO_BUFFER;

        while (bufferInputTimer[nameOfAttackToBuffer] > 0)
        {
            yield return null;
            bufferInputTimer[nameOfAttackToBuffer] -= Time.deltaTime;
        }
        anim.ResetTrigger(nameOfAttackToBuffer);
    }
}
