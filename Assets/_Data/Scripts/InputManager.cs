using UnityEngine;

public class InputManager : SaiSingleton<InputManager>
{
    [SerializeField] protected bool isLeftMouseHeld;
    [SerializeField] protected bool isLeftMouseClicked;
    [SerializeField] protected bool isRightMouseClicked;

    public bool IsLeftMouseHeld => isLeftMouseHeld;
    public bool IsLeftMouseClicked => isLeftMouseClicked;
    public bool IsRightMouseClicked => isRightMouseClicked;

    protected void Update()
    {
        this.CheckMouseButtons();
    }

    protected virtual void CheckMouseButtons()
    {
        this.isLeftMouseHeld = Input.GetMouseButton(0);
        this.isLeftMouseClicked = Input.GetMouseButtonDown(0);
        this.isRightMouseClicked = Input.GetMouseButtonDown(1);
    }
}
