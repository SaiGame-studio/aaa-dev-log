using UnityEngine;

public class ItemScanBySpotLight: ItemScan 
{
    [SerializeField] protected Light _light;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpotLight();
    }

    protected virtual void LoadSpotLight()
    {
        if (this._light != null) return;
        this._light = GetComponentInChildren<Light>();
        Debug.LogWarning(transform.name + ": LoadSpotLight", gameObject);
    }


    public override void SetColor(Color color)
    {
        this._light.color = color;
    }
}
