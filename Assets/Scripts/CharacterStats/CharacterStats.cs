using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that hold references to important behviaours and stats that an object may need to know to interact with a character
/// </summary>
public class CharacterStats : MonoBehaviour {
    [Tooltip("The characters associated time layer. If there is a mechanic dealing with time, this will be applied to all components in our character stats")]
    public CustomTime.TimeLayer characterTimeLayer;
    [Tooltip("The maximum health that this character will have. Upon starting up, this character will begin with this health")]
    public float maxHealth = 100;
    private float currentHealth;

    public MovementMechanics movementMechanics { get; set; }
    public CombatMechanics combatMechanics { get; set; }
    public HitboxManager associatedHitboxManager { get; set; }
    public CustomPhysics2D customPhysics { get; set; }


    #region monobehaivour methods
    private void Awake()
    {
        movementMechanics = GetComponent<MovementMechanics>();
        combatMechanics = GetComponent<CombatMechanics>();
        associatedHitboxManager = GetComponent<HitboxManager>();
        if (associatedHitboxManager)
        {
            associatedHitboxManager.associatedCharacterStats = this;
        }
        customPhysics = GetComponent<CustomPhysics2D>();

        currentHealth = maxHealth;
    }
    #endregion monobheaviour methods

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
