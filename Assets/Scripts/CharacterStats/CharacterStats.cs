using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that hold references to important behviaours and stats that an object may need to know to interact with a character
/// </summary>
public class CharacterStats : MonoBehaviour {
    #region const variables
    private const string DEAD_ANIMATOR_TRIGGER = "Dead";
    #endregion const variables

    #region enums
    public enum HitboxLayer
    {
        Enemy,
        Player,
        Environment,
    }
    #endregion enums
    #region statistics variables
    [Tooltip("The layer of our hitbox. This will be used to make sure that our hitbox does not hit other hitboxes that are on the same team")]
    public HitboxLayer hitboxLayer;
    [Tooltip("The maximum health that this character will have. Upon starting up, this character will begin with this health")]
    public float maxHealth = 100;
    /// <summary>
    /// The player's current Health
    /// </summary>
    private float currentHealth;
    #endregion statistics variables

    public MovementMechanics movementMechanics { get; set; }
    public CombatMechanics combatMechanics { get; set; }
    public MeleeMechanics associatedHitboxManager { get; set; }
    public CustomPhysics2D customPhysics { get; set; }
    public TimeManagedObject timeManagedObject { get; set; }
    public Animator characterAnimator { get; set; }


    #region monobehaivour methods
    private void Start()
    {
        movementMechanics = GetComponent<MovementMechanics>();
        combatMechanics = GetComponent<CombatMechanics>();
        associatedHitboxManager = GetComponent<MeleeMechanics>();
        timeManagedObject = GetComponent<TimeManagedPlayer>();
        if (timeManagedObject != null)
        {
            timeManagedObject.TimeLayerUpdated += OnTimeLayerUpdated;
        }
        characterAnimator = GetComponent<Animator>();

        if (associatedHitboxManager)
        {
            associatedHitboxManager.associatedCharacterStats = this;
        }
        customPhysics = GetComponent<CustomPhysics2D>();
        if (customPhysics != null)
        {
            customPhysics.associatedCharacterStats = this;
        }

        currentHealth = maxHealth;
        GameOverseer.Instance.AddTimeMangedObjectToList(timeManagedObject);
    }
    

    private void OnDestroy()
    {
        if (timeManagedObject != null)
        {
            timeManagedObject.TimeLayerUpdated -= OnTimeLayerUpdated;
        }
    }
    #endregion monobheaviour methods

    /// <summary>
    /// This method will be called any time the time scale in our time layer is changed. Any changes needed
    /// </summary>
    protected virtual void OnTimeLayerUpdated()
    {
        if (characterAnimator != null)
        {
            characterAnimator.speed = CustomTime.GetTimeLayerScale(timeManagedObject.timeLayer);
        }
    }

    #region health methods
    /// <summary>
    /// Whenever our character takes damage this method should be caled
    /// </summary>
    /// <param name="damageToTake"></param>
    public virtual void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        if (currentHealth <= 0)
        {
            OnCharacterKilled();
        }
    }

    /// <summary>
    /// Adds health to the associated character
    /// </summary>
    /// <param name="healthToAdd"></param>
    public virtual void AddHealth(float healthToAdd)
    {
        this.currentHealth = Mathf.Min(maxHealth, currentHealth + healthToAdd);
    }

    /// <summary>
    /// When a character's health has been 
    /// </summary>
    public virtual void OnCharacterKilled()
    {
        if (characterAnimator)
        {
            characterAnimator.SetTrigger(DEAD_ANIMATOR_TRIGGER);
        }
        Destroy(this.gameObject);
    }
    #endregion health methods

}
