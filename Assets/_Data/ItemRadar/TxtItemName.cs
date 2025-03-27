using UnityEngine;

public class TxtItemName : TextUpdate
{
    protected override void ShowingText()
    {
        this.textPro.text = ItemScanner.Instance.GetCurrentTargetName(); ;
    }
}
