using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that hold references to important behviaours and stats that an object may need to know to interact with a character
/// </summary>
public class CharacterStats : MonoBehaviour {
    #region enums
    public enum HitboxLayer
    {
        Player,
        Enemy,
    }
    #endregion enums
    [Tooltip("The layer of our hitbox. This will be used to make sure that our hitbox does not hit other hitboxes that are on the same team")]
    public HitboxLayer hitboxLayer;
    [Tooltip("The maximum health that this character will have. Upon starting up, this character will begin with this health")]
    public float maxHealth = 100;
    private float currentHealth;

    public MovementMechanics movementMechanics { get; set; }
    public CombatMechanics combatMechanics { get; set; }
    public MeleeMechanics associatedHitboxManager { get; set; }
    public CustomPhysics2D customPhysics { get; set; }
    public TimeManagedPlayer timeManagedObject { get; set; }
    public Animator anim;


    #region monobehaivour methods
    private void Start()
    {
        movementMechanics = GetComponent<MovementMechanics>();
        combatMechanics = GetComponent<CombatMechanics>();
        associatedHitboxManager = GetComponent<MeleeMechanics>();
        timeManagedObject = GetComponent<TimeManagedPlayer>();
        timeManagedObject.TimeLayerUpdated += OnTimeLayerUpdated;
        anim = GetComponent<Animator>();

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
        timeManagedObject.TimeLayerUpdated -= OnTimeLayerUpdated;
    }
    #endregion monobheaviour methods

    private void OnTimeLayerUpdated()
    {
        if (anim != null)
        {
            anim.speed = CustomTime.GetTimeLayerScale(timeManagedObject.timeLayer);
        }
    }

    #region health methods
    public void TakeDamage(float damageToTake)
    {

    }

    /// <summary>
    /// Adds health to the associated character
    /// </summary>
    /// <param name="healthToAdd"></param>
    public void AddHealth(float healthToAdd)
    {
        this.currentHealth = Mathf.Min(maxHealth, currentHealth + healthToAdd);
    }
    #endregion health methods

}
