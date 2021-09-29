using UnityEngine;
using UnityEngine.UI;

public class ActivationStatusUI : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] Image statusImage;
    [SerializeField] Text statusText;
    #endregion

    #region Private Fields
    bool isActive = false;
    #endregion

    #region Public Methods
    public void StatusOn()
    {
        statusImage.color = Color.green;
        statusText.text = "ON";
    }

    public void StatusOff()
    {
        statusImage.color = Color.red;
        statusText.text = "OFF";
    }

    public void ToggleStatus()
    {
        isActive = !isActive;

        if (isActive) StatusOn();
        else StatusOff();
    }
    #endregion
}
