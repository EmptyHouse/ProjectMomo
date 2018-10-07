using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that hold references to important behviaours and stats that an object may need to know to interact with a character
/// </summary>
public class CharacterStats : MonoBehaviour {
    public float maxHealth = 100;
    private float currentHealth;

    public MovementMechanics movementMechanics { get; set; }
    public HitboxManager associateMeleeMechanics { get; set; }


    #region monobehaivour methods
    private void Awake()
    {
        movementMechanics = GetComponent<MovementMechanics>();
        //associateMeleeMechanics = GetComponent<MeleeMechanics>();
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
