using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic Melee mechanics. This will contain all the properties necessary when a characer attacks an enemy
/// </summary>
public class MeleeMechanics : MonoBehaviour {
    [Tooltip("A list of melee properties that will be referenced when our hitbox interacts with an enemy hurtbox")]
    public MeleeProperties[] meleePropertyList = new MeleeProperties[0];
    /// <summary>
    /// A list of all the hitboxes connected to this object
    /// </summary>
    [HideInInspector]
    public List<Hitbox> allAssociatedHitboxes = new List<Hitbox>();
    /// <summary>
    /// This is a list of all the character stats that have been hit by our hitboxes. This is to ensure
    /// we don't hit an object multiple times unless it is intentional
    /// </summary>
    public CharacterStats associatedCharacterStats { get; set; }

    /// <summary>
    /// A collection of all the hitbox managers that have been intereacted with. This is to ensure that we
    /// do not hit an object multiple times from just one attack by interacting with multiple hurtboxes
    /// </summary>
    private List<MeleeMechanics> hitboxManagerCollection = new List<MeleeMechanics>();

    [Tooltip("The current melee attack that is being used. This is not allowed to be changed in the editor. It is only ")]
    public int currentMeleeState = -1;
    #region monobehaviour methods
    private void Start()
    {
        foreach (Hitbox hitbox in allAssociatedHitboxes)
        {
                    }
    }

    private void OnValidate()
    {
        currentMeleeState = -1;
        for (int i = 0; i < meleePropertyList.Length; i++)
        {
            if (meleePropertyList[i] == null)
            {
                meleePropertyList[i] = new MeleeProperties();
            }
            if (!meleePropertyList[i].propertyIsInitialized)
            {
                InitializeMeleeProperties(meleePropertyList[i]);
            }
            meleePropertyList[i].attackIDValue = i;
        }
    }
    #endregion monobehaviour methods

    #region adding and removing hitbox/hurtboxes
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hitbox"></param>
    public void AddAssociatedHitbox(Hitbox hitbox)
    {
        if (allAssociatedHitboxes.Contains(hitbox))
        {
            Debug.LogWarning("This hitbox was already added.", hitbox);
            return;
        }
        allAssociatedHitboxes.Add(hitbox);
    }

    public void SetMeleeStats(int meleeStatsToUse)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hitbox"></param>
    public void RemoveAssociatedHitbox(Hitbox hitbox)
    {
        if (!allAssociatedHitboxes.Contains(hitbox))
        {
            Debug.LogWarning("This hitbox was not found in the list. Perhaps it was already removed", hitbox);
            return;
        }
        allAssociatedHitboxes.Remove(hitbox);
    }
    #endregion adding and removing hitbox/hurtboxes

    #region hitbox manager collection methods
    /// <summary>
    /// Before the beginning of a new attack this method should be called to clear
    /// any previously hit character. That way they can be hit again
    /// </summary>
    public void ResetHitboxManagerCollection()
    {
        hitboxManagerCollection.Clear();
    }

    /// <summary>
    /// Adds a hitbox manager to the the hitboxManagerCollection list. This will be used to ensure that
    /// we do not hit a target multiple time with the same attack
    /// </summary>
    /// <param name="hitboxManager"></param>
    public void AddHitboxManagerToCollection(MeleeMechanics hitboxManager)
    {
        if (!hitboxManager)
        {
            Debug.LogWarning("The hitbox manager that was passed in was null or inactive. Please check what went wrong here.");
        }
        if (hitboxManagerCollection.Contains(hitboxManager))
        {
            return;
        }
        hitboxManagerCollection.Add(hitboxManager);
    }

    public void RemoveHitboxManagerFromCollection(MeleeMechanics hitboxManagerToRemove)
    {
        if (!hitboxManagerToRemove)
        {
            Debug.LogWarning("The hitbox manager that you are trying to remove is null or inactive. This should not be the case. Please double check what may have happened.");
        }
        if (hitboxManagerCollection.Contains(hitboxManagerToRemove))
        {
            hitboxManagerCollection.Remove(hitboxManagerToRemove);
        }
    }

    public bool HitboxManagerCollectionContainsHitboxManager(MeleeMechanics managerToCheck)
    {
        if (!managerToCheck)
        {
            Debug.LogWarning("The manager you are trying to check is either null or inactive. This should not be the case. Please check what may have gone wrong.");
        }
        return hitboxManagerCollection.Contains(managerToCheck);
    }
    #endregion hitbox manager collection methods

    #region melee mechanic events
    /// <summary>
    /// In the event that our hitbox interacts with an enemy hurtbox this method should be called in order ensure that we do properly
    /// hit our enemy the desiired amount of times and apply the appropriate damage
    /// </summary>
    /// <param name="myHitbox"></param>
    /// <param name="hurtbox"></param>
    public void OnHitboxOnEnemyHurtbox(Hitbox myHitbox, Hurtbox hurtbox)
    {

    }
    #endregion melee mechanic events

    #region structs
    public void InitializeMeleeProperties(MeleeProperties meleePropertiesToInitialize)
    {
        meleePropertiesToInitialize.propertyIsInitialized = true;
        meleePropertiesToInitialize.attackName = "Attack_00";
        meleePropertiesToInitialize.attackIDValue = 0;
        meleePropertiesToInitialize.damageToBeDealt = 5;
        meleePropertiesToInitialize.hitStunInSeconds = 1;
        
    }

    [System.Serializable]
    public class MeleeProperties
    {
        [Tooltip("This is only used visually for players to read or for developers to organize what the name of a move is")]
        public string attackName = "Attack_00";
        [Tooltip("This is used to identify the value")]
        public int attackIDValue = 0;
        [Tooltip("The amount of damage we will apply to the enemy that we hit")]
        public float damageToBeDealt = 5;
        [Tooltip("The amount of time in seconds that a character can not carry out any other action")]
        public float hitStunInSeconds = 1;
        [Tooltip("The animation curve that will handle the velocity ")]
        public AnimationCurve velocityAnimationCurve;
        [HideInInspector]
        public bool propertyIsInitialized = false;
        
    }
    #endregion structs
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveCharacterWhileAnimating(MeleeProperties currentMeleeProperties)
    {
        AnimationCurve velocityAnimationCurve = currentMeleeProperties.velocityAnimationCurve;
        if (velocityAnimationCurve.length <= 0)
        {
            yield break;
        }

        float totalTime = velocityAnimationCurve.keys[velocityAnimationCurve.length - 1].time;
        float timeThatHasPassed = 0;
        while (timeThatHasPassed < totalTime)
        {
            associatedCharacterStats.customPhysics.velocity.x = velocityAnimationCurve.Evaluate(timeThatHasPassed);
            timeThatHasPassed += CustomTime.GetTimeLayerAdjustedDeltaTime(associatedCharacterStats.timeManagedObject.timeLayer);
            yield return null;
        }
    }

    /// <summary>
    /// Sometimes if a hit is really impactful we may want to stop time for a bit to give the player a sense of power behind
    /// a hit
    /// </summary>
    /// <param name="timeInSecondsToStop"></param>
    /// <returns></returns>
    protected virtual IEnumerator StopTimeForHitImpact(float timeInSecondsToStop)
    {
        yield break;
    }
}
