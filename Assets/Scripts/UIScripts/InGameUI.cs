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
    #endregion main variables

    #region monbehaviour methods
    private void Awake()
    {
        if (instance)
        {
            Destroy(this.transform.parent);
            return;
        }
        DontDestroyOnLoad(this.transform.parent);
        instance = this;
    }
    #endregion monobehaviour methods

    public void SetTimeMeterText(float textValue)
    {
        timeMeterText.text = "Time Meter: " + ((int)textValue).ToString();
    }


}
