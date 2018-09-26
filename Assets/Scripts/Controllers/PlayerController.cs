using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerController : MonoBehaviour {
    public const string HorizontalInput = "Horizontal";

    [Tooltip("Reference to the player stats component. This will hold all reference of our character")]
    private CharacterStats characterStats;

    #region monobehaviour methods
    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        characterStats.movementMechanics.SetHorizontalInput(Input.GetAxisRaw(HorizontalInput));
        if (Input.GetButtonDown("Jump"))
        {
            characterStats.movementMechanics.Jump();
        }
    }
    #endregion monobehaiovur methods
}
