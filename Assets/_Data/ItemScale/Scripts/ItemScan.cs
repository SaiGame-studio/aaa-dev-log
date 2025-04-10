using UnityEngine;

public abstract class ItemScan : SaiBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        ItemScaling.Instance.AddItem(other.transform);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        ItemScaling.Instance.RemoveItem(other.transform);
    }

    public abstract void SetColor(Color color);
}
