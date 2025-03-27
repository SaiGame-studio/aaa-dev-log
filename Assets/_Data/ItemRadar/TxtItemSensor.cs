using UnityEngine;

public class TxtItemSensor : TextUpdate
{
    protected override void ShowingText()
    {
        this.textPro.text = ItemScanner.Instance.GetItemSensor().ToString("N1")+"%";
    }
}
