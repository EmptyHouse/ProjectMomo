﻿using System.Collections;
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
    private const string DASH_INPUT = "Dash";
    private const string TIME_CONTROL_INPUT = "TimeControl";
    private const string USE_ITEM = "Item";
    #endregion const inputs

    public const string HorizontalInput = "Horizontal";
    public const string VerticalInput = "Vertical";

    [Tooltip("Reference to the player stats component. This will hold all reference of our character")]
    private PlayerCharacterStats characterStats;

    #region monobehaviour methods
    private void Start()
    {
        characterStats = (PlayerCharacterStats)GetComponent<PlayerCharacterStats>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void LateUpdate()
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
        if (Input.GetButtonUp(JUMP_INPUT))
        {
            characterStats.movementMechanics.SetCharacterFastFalling(true);
        }
        if (Input.GetButtonDown(TIME_CONTROL_INPUT))
        {
            ((TimeManagedPlayer)characterStats.timeManagedObject).OnTimeControlToggled();
        }
        if (Input.GetButtonDown(DASH_INPUT))
        {
            characterStats.movementMechanics.Dash();
        }
        if(Input.GetButtonDown(USE_ITEM))
        {
            characterStats.inventoryManager.UseItem();
        }
    }
    #endregion monobehaiovur methods
}
