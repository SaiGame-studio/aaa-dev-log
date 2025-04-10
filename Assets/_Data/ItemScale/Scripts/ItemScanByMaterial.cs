using UnityEngine;

public class ItemScanByMaterial: ItemScan 
{
    [SerializeField] private Renderer targetRenderer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRenderer();
    }

    protected virtual void LoadRenderer()
    {
        if (this.targetRenderer != null) return;
        this.targetRenderer = GetComponent<Renderer>();
        Debug.LogWarning(transform.name + ": LoadRenderer", gameObject);
    }


    public override void SetColor(Color color)
    {
        this.targetRenderer.material.SetColor("_EmissionColor", color);
    }
}
