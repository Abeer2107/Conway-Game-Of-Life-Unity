using UnityEngine;
using UnityEngine.UI;

public class CounterUI : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] Text counterText;
    [SerializeField] float rate = 1f;
    #endregion

    #region Private Fields
    int value = 0;
    bool isActive;
    float timer = 0;
    #endregion

    #region MonoBehaviour Messages
    private void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;

            if(timer >= rate)
            {
                timer = 0;
                value++;

                counterText.text = value.ToString();
            }
        }
    }
    #endregion

    #region Public Methods
    public void ResetCounter()
    {
        value = 0;
        counterText.text = value.ToString();
    }

    public void ToggleStatus()
    {
        isActive = !isActive;

        if (!isActive) ResetCounter();
    }
    #endregion
}
