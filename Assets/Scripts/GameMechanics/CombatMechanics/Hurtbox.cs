using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will contain all the logic necessary for our character's hurtbox.
/// 
/// Hurtbox is a bit different from our traditional hitbox. this class will only look to take damage rather than dealing damage
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Hurtbox : MonoBehaviour {
    public CharacterStats associatedCharacterstats;
}
