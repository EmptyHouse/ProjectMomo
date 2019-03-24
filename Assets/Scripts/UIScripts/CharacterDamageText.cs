using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamageText : MonoBehaviour
{ 
    public enum DamageTextType
    {
        HealingText,
        DamageTakenText,
    }

    private DamageTextType damageTextType;
    public void SetupDamageText(DamageTextType typeOfText, float valueToDisplay)
    {

    }
}
