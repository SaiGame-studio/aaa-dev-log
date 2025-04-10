using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemScalableManager : SaiSingleton<ItemScalableManager>
{
    [SerializeField] protected List<ItemScalable> items;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemScables();
    }

    protected virtual void LoadItemScables()
    {
        if (this.items.Count > 0) return;
        ItemScalable[] itemScalables = GameObject.FindObjectsByType<ItemScalable>(FindObjectsSortMode.None);
        this.items = itemScalables.ToList();
        Debug.LogWarning(transform.name + ": LoadItemScables", gameObject);
    }

    public virtual ItemScalable FindItem(Transform itemFind)
    {
        foreach (ItemScalable itemScalable in this.items)
        {
            if (itemFind == itemScalable.transform) return itemScalable;
        }
        return null;
    }
}
