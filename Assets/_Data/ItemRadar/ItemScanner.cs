using UnityEngine;

public class ItemScanner : SaiSingleton<ItemScanner>
{
    [SerializeField] protected ItemLocalManager currentZone;

    [SerializeField] protected float currentTargetDistance = 0f;
    [SerializeField] protected float lookFactor = 0f;
    [SerializeField] protected float lookWeight = 1f;
    [SerializeField] protected ItemCtrl currentTarget;
    public ItemCtrl CurrentItem => currentTarget;

    [SerializeField] protected float nearestTargetDistance = 0f;
    [SerializeField] protected ItemCtrl nearestTarget;

    [SerializeField] protected float scanRadius = 27f;
    [SerializeField] protected float forceTargetRadius = 7f;

    private void FixedUpdate()
    {
        this.ScanningNewTarget();
        this.TrackingCurrentTarget();
        this.TrackingNearestTarget();
        this.GetLookAtTargetFactor();
    }

    protected virtual void TrackingCurrentTarget()
    {
        if (!this.IsCurrentTargetAvailable()) return;
        this.currentTargetDistance = this.DistanceTo(this.currentTarget.transform);
        if (this.currentTargetDistance > this.scanRadius) this.RemoveCurrentTarget();
    }

    protected virtual void TrackingNearestTarget()
    {
        if (this.nearestTargetDistance > this.scanRadius) this.nearestTarget = null;
        if (!this.IsNearestTargetAvailable()) return;

        if (!this.IsCurrentTargetAvailable() ||
            (this.nearestTargetDistance < this.forceTargetRadius &&
             this.nearestTargetDistance < this.currentTargetDistance))
        {
            this.currentTarget = this.nearestTarget;
        }
    }

    protected virtual void RemoveCurrentTarget()
    {
        this.currentTarget = null;
    }

    protected virtual void ScanningNewTarget()
    {
        float distance;
        float minDistance = Mathf.Infinity;
        foreach (ItemCtrl itemCtrl in this.currentZone.Items)
        {
            distance = this.DistanceTo(itemCtrl.transform);
            if (distance > this.scanRadius) continue;
            if (distance >= minDistance) continue;
            minDistance = distance;
            this.nearestTarget = itemCtrl;

            if (!this.IsCurrentTargetAvailable()) this.currentTarget = itemCtrl;

            //Debug.Log(itemCtrl.name);
        }
        this.nearestTargetDistance = minDistance;
    }

    protected virtual float DistanceTo(Transform target)
    {
        return Vector3.Distance(target.position, transform.position);
    }

    protected virtual bool IsCurrentTargetAvailable()
    {
        bool avaiable = true;
        if (this.currentTarget == null || !this.currentTarget.gameObject.activeSelf) avaiable = false;
        return avaiable;
    }

    protected virtual bool IsNearestTargetAvailable()
    {
        bool avaiable = true;
        if (this.nearestTarget == null || !this.nearestTarget.gameObject.activeSelf) avaiable = false;
        return avaiable;
    }

    public virtual string GetCurrentTargetName()
    {
        string name = "#no-target";
        if (this.IsCurrentTargetAvailable()) name = this.currentTarget.name;
        return name;
    }

    public virtual string GetCurrentTargetDistance()
    {
        string disString = "0.0";
        if (this.IsCurrentTargetAvailable()) disString = this.currentTargetDistance.ToString("N2");
        return disString + "m";
    }

    public virtual string GetCurrentLookFactor()
    {
        return this.lookFactor.ToString("N1") + "°";
    }

    public float GetLookAtTargetFactor()
    {
        if (this.IsCurrentTargetAvailable())
        {
            Vector3 toTarget = (this.currentTarget.transform.position - transform.position).normalized;
            Vector3 forward = transform.forward;

            float angle = Vector3.Angle(forward, toTarget);
            this.lookFactor = (180f - angle) * 2f;
        }

        return 360 - this.lookFactor;
    }

    public float GetItemSensor()
    {
        float sensor = 0;
        float maxDistance = this.scanRadius;
        float distance = this.currentTargetDistance;
        float lookFactor = this.lookFactor;

        float distanceFactor = Mathf.Clamp01(1f - (distance / maxDistance));
        float lookFactorNormalized = Mathf.Clamp01(lookFactor / 360f);
        float weightedLookFactor = Mathf.Clamp01(lookFactorNormalized * lookWeight);
        sensor = distanceFactor * weightedLookFactor;

        return sensor * 100;
    }


}
