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
    [Tooltip("The health bar slider that indicates the remaining health of our character")]
    private UnityEngine.UI.Slider healthSlider;
    [Tooltip("The layer of our hitbox. This will be used to make sure that our hitbox does not hit other hitboxes that are on the same team")]
    public HitboxLayer hitboxLayer;
    [Tooltip("The maximum health that this character will have. Upon starting up, this character will begin with this health")]
    public float maxHealth = 100;
    /// <summary>
    /// The player's current Health
    /// </summary>
    protected float currentHealth;
    #endregion statistics variables

    public MovementMechanics movementMechanics { get; set; }
    
    public CustomPhysics2D customPhysics { get; set; }
    public TimeManagedObject timeManagedObject { get; set; }
    public Animator characterAnimator { get; set; }


    #region monobehaivour methods
    protected virtual void Awake()
    {
        healthSlider = GetComponentInChildren<UnityEngine.UI.Slider>();
        
        movementMechanics = GetComponent<MovementMechanics>();
        timeManagedObject = GetComponent<TimeManagedObject>();
        if (timeManagedObject != null)
        {
            timeManagedObject.TimeLayerUpdated += OnTimeLayerUpdated;
        }
        characterAnimator = GetComponent<Animator>();

        customPhysics = GetComponent<CustomPhysics2D>();
        if (customPhysics != null)
        {
            customPhysics.associatedCharacterStats = this;
        }

        currentHealth = maxHealth;
        GameOverseer.Instance.AddTimeMangedObjectToList(timeManagedObject);

        UpdateHealthBarSlider();
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

    /// <summary>
    /// Updates the health bar slider that is found above our enemies to show their remaining health
    /// </summary>
    private void UpdateHealthBarSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
    #endregion health methods

}
