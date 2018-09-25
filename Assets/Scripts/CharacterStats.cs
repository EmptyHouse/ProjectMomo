using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    public MovementMechanics movementMechanics { get; set; }

    private void Awake()
    {
        movementMechanics = GetComponent<MovementMechanics>();
    }
}
