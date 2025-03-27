public class TxtItemDistance : TextUpdate
{
    protected override void ShowingText()
    {
        this.textPro.text = ItemScanner.Instance.GetCurrentTargetDistance(); ;
    }
}
