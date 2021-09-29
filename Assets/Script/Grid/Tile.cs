using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Tile : MonoBehaviour
{
    #region Private Fields
    private bool isActive = false;
    private int value = 0;
    private SpriteRenderer spRenderer;
    #endregion

    #region MonoBehaviour Messages
    private void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject()){
            isActive = !isActive;

            if (isActive) SetValue(1);
            else SetValue(0);
        }
    }
    #endregion

    #region Public Methods
    public void Activate()
    {
        isActive = true;
        spRenderer.color = Color.black;
    }

    public void Deactivate()
    {
        isActive = false;
        spRenderer.color = Color.white;
    }

    public void SetValue(int val)
    {
        value = val;

        switch (value)
        {
            case 0:
                Deactivate();
                break;
            case 1:
                Activate();
                break;
        }
    }

    public int GetValue()
    {
        return value;
    }
    #endregion
}
