using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class ItemLocalManager : SaiBehaviour
{
    [SerializeField] protected List<ItemCtrl> items;
    public ReadOnlyCollection<ItemCtrl> Items => items.AsReadOnly();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItems();
    }

    protected virtual void LoadItems()
    {
        if (this.items.Count > 0) return;
        items = new List<ItemCtrl>(GetComponentsInChildren<ItemCtrl>(includeInactive: true));
        Debug.LogWarning(transform.name + ": LoadItems", gameObject);
    }


}
