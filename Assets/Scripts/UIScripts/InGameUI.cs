using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {
    #region static values
    private static InGameUI instance;

    public static InGameUI Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject.FindObjectOfType<InGameUI>();
            }
            return instance;
        }
    }
    #endregion static values

    


    #region main variables
    [Header("UI References")]
    public Text timeMeterText;
    [Tooltip("Slider that displays the amount of health that the player has remaining")]
    public Slider playerHealthBarSlider;

    [Tooltip("The animator object that will be called when loading into a new level")]
    public Animator levelTransitionUIAnimator;

    [Header("Important Prefab References")]
    public CharacterDamageText damageTextPrefab;
    #endregion main variables

    #region monbehaviour methods
    private void Awake()
    {
        GameOverseer.Instance.AddObjectToDontDestroyOnLoad(this.transform.parent.gameObject);
        instance = this;
    }
    #endregion monobehaviour methods

    #region update ui display methods

    public void SetTimeMeterText(float textValue)
    {
        timeMeterText.text = "Time Meter: " + ((int)textValue).ToString();
    }

    
    public void SetPlayerHealthBar(float healthPercentage) 
    {
        playerHealthBarSlider.value = healthPercentage;
    }

    #endregion update ui display methods
}
