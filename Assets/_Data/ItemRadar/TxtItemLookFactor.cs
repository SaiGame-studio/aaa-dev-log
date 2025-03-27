public class TxtItemLookFactor : TextUpdate
{
    protected override void ShowingText()
    {
        this.textPro.text = ItemScanner.Instance.GetCurrentLookFactor(); ;
    }
}
