using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a reference to the character UI that should be applied to any character that will display UI elements, such as health bars, icons, etc.
/// </summary>
public class CharacterWorldUI : MonoBehaviour
{
    public Slider associatedHealthBar;
    public Transform damageTextSpawnPoint;
    public Transform characterWorldCanvasReference;
    public CharacterStats associatedCharacterStats { get; set; }


    public void UpdateHealthBarToReflectCurrentHealth()
    {
        associatedHealthBar.value = associatedCharacterStats.currentHealth / associatedCharacterStats.maxHealth;
    }
}
