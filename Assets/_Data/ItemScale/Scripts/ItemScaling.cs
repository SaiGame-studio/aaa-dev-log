using System.Collections.Generic;
using UnityEngine;

public class ItemScaling : SaiSingleton<ItemScaling>
{
    [SerializeField] protected ItemScan itemScan;
    [SerializeField] protected bool scaleUp = false;
    [SerializeField] protected float scaleStep = 0.1f;
    [SerializeField] protected float bounceForce = 0.1f;
    [SerializeField] protected Color colorScaleUp = Color.red;
    [SerializeField] protected Color colorScaleDown = Color.blue;

    [SerializeField] protected List<ItemScalable> items;

    protected virtual void LateUpdate()
    {
        this.ToogleScaleMode();
    }

    protected virtual void FixedUpdate()
    {
        this.UpdateScanColor();
        this.UpdateItemsScale();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemScan();
    }

    protected virtual void LoadItemScan()
    {
        if (this.itemScan != null) return;
        this.itemScan = GameObject.FindAnyObjectByType<ItemScan>();
        Debug.LogWarning(transform.name + ": LoadItemScan", gameObject);
    }

    protected virtual void ToogleScaleMode()
    {
        if (InputManager.Instance.IsRightMouseClicked) this.scaleUp = !this.scaleUp;
    }

    public virtual void AddItem(Transform item)
    {
        ItemScalable itemScalable = ItemScalableManager.Instance.FindItem(item);
        if (itemScalable == null) return;
        if (this.items.Contains(itemScalable)) return;
        this.items.Add(itemScalable);
    }

    public virtual void RemoveItem(Transform item)
    {
        ItemScalable itemScalable = this.FindItem(item);
        this.items.Remove(itemScalable);
    }

    public virtual ItemScalable FindItem(Transform item)
    {
        ItemScalable itemScalable = ItemScalableManager.Instance.FindItem(item);
        if (itemScalable != null && this.items.Contains(itemScalable)) return itemScalable;
        return null;
    }

    protected virtual void UpdateScanColor()
    {
        Color color = this.colorScaleDown;
        if (this.scaleUp) color = this.colorScaleUp;
        if (this.itemScan == null) return;
        this.itemScan.SetColor(color);
    }

    protected virtual void UpdateItemsScale()
    {
        if (!InputManager.Instance.IsLeftMouseHeld) return;

        foreach(ItemScalable item in this.items)
        {
            if (this.scaleUp) this.ScaleUpItem(item);
            else this.ScaleDownItem(item);
            item.Rigidbody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }

    protected virtual void ScaleUpItem(ItemScalable item)
    {
        if (item == null) return;
        Vector3 current = item.transform.localScale;
        item.transform.localScale = current + Vector3.one * scaleStep;
    }

    protected virtual void ScaleDownItem(ItemScalable item)
    {
        if (item == null) return;
        Vector3 current = item.transform.localScale;
        Vector3 newScale = current - Vector3.one * scaleStep;
        if (newScale.x < 0.1f) newScale = Vector3.one * 0.1f;
        item.transform.localScale = newScale;
    }

}
