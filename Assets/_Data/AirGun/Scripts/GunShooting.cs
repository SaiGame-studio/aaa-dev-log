using System.Collections.Generic;
using UnityEngine;

public class GunShooting : SaiBehaviour
{
    [SerializeField] protected Transform markerPrefab;
    [SerializeField] protected string markerName = "AirGunMarker";
    [SerializeField] protected float pushSorce = 700f;
    [SerializeField] protected Vector3 markerLocalPosition = new Vector3(0,2.5f,0);
    [SerializeField] protected List<HumanoidCtrl> targets;

    protected virtual void LateUpdate()
    {
        this.Shooting();
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        this.AddTarget(collider);
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        this.RemoveTarget(collider);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMarker();
    }

    protected virtual void AddTarget(Collider collider)
    {
        HumanoidCtrl humanoidCtrl = collider.GetComponent<HumanoidCtrl>();
        if (humanoidCtrl == null) return;
        if (this.targets.Contains(humanoidCtrl)) return;
        this.targets.Add(humanoidCtrl);
        this.AddMarker(humanoidCtrl.transform);
    }

    protected virtual void AddMarker(Transform target)
    {
        Transform newMarker = Instantiate(this.markerPrefab);
        newMarker.parent = target;
        newMarker.localPosition = this.markerLocalPosition;
        newMarker.gameObject.SetActive(true);
        newMarker.name = this.markerName;
    }

    protected virtual void RemoveTarget(Collider collider)
    {
        HumanoidCtrl humanoidCtrl = collider.GetComponent<HumanoidCtrl>();
        if (humanoidCtrl == null) return;
        this.targets.Remove(humanoidCtrl);
        this.RemoveMarker(collider.transform);
    }

    protected virtual void RemoveMarker(Transform target)
    {
        Transform exitMarker = target.Find(this.markerName);
        if (exitMarker == null) return;
        Destroy(exitMarker.gameObject);
    }

    protected virtual void LoadMarker()
    {
        if (this.markerPrefab != null) return;
        this.markerPrefab = transform.Find(this.markerName);
        this.markerPrefab.gameObject.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadMarker", gameObject);
    }

    protected virtual void Shooting()
    {
        if (!InputManager.Instance.IsLeftMouseClicked) return;

        Vector3 direction;
        foreach (HumanoidCtrl target in this.targets)
        {
            direction = (target.transform.position - transform.position).normalized;
            target.Ragdoll.AddForce(direction, this.pushSorce);
        }
    }
}
