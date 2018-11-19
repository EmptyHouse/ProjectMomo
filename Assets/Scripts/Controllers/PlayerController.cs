using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerController : MonoBehaviour {
    #region const inputs
    private const string MELEE_INPUT = "Melee";
    private const string PROJECTILE_INPUT = "FireProjectile";
    private const string JUMP_INPUT = "Jump";
    private const string TIME_CONTROL_INPUT = "TimeControl";
    #endregion const inputs

    public const string HorizontalInput = "Horizontal";
    public const string VerticalInput = "Vertical";

    [Tooltip("Reference to the player stats component. This will hold all reference of our character")]
    private CharacterStats characterStats;

    #region monobehaviour methods
    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        characterStats.movementMechanics.SetHorizontalInput(Input.GetAxisRaw(HorizontalInput));
        characterStats.movementMechanics.SetVerticalInput(Input.GetAxisRaw(VerticalInput));
        if (Input.GetButtonDown(PROJECTILE_INPUT))
        {
            characterStats.combatMechanics.FireArrowAnimation();
        }
        if (Input.GetButtonDown(MELEE_INPUT))
        {
            characterStats.combatMechanics.PerformMeleeAnimation();
        }
        if (Input.GetButtonDown(JUMP_INPUT))
        {
            characterStats.movementMechanics.Jump();
        }
        if (Input.GetButtonDown(TIME_CONTROL_INPUT))
        {
            characterStats.timeManagedObject.OnTimeControlToggled();
        }
    }
    #endregion monobehaiovur methods
}
