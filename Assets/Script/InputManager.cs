using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    #region Serialized Fields
    [Header("Key Setting")]
    [SerializeField] KeyCode startButton = KeyCode.Space;
    [SerializeField] KeyCode clearButton = KeyCode.Alpha1;
    [SerializeField] KeyCode fillButton = KeyCode.Alpha2;
    [SerializeField] KeyCode randomizeButton = KeyCode.Alpha3;
    [SerializeField] KeyCode quitButton = KeyCode.Escape;

    [Header("Events")]
    public UnityEvent OnStartPressed;
    public UnityEvent OnClearPressed;
    public UnityEvent OnFillPressed;
    public UnityEvent OnRandomizePressed;
    #endregion

    #region MonoBehaviour Messages
    private void Update()
    {
        if (Input.GetKeyDown(startButton))
        {
            OnStartPressed?.Invoke();
        }

        if (Input.GetKeyDown(clearButton))
        {
            OnClearPressed?.Invoke();
        }
        else if (Input.GetKeyDown(fillButton))
        {
            OnFillPressed?.Invoke();
        }
        else if (Input.GetKeyDown(randomizeButton))
        {
            OnRandomizePressed?.Invoke();
        }

        if (Input.GetKeyDown(quitButton))
        {
            Application.Quit(0);
        }
    }
    #endregion
}
