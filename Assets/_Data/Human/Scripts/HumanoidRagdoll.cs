using com.cyborgAssets.inspectorButtonPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanoidRagdoll : SaiBehaviour
{
    [SerializeField] protected HumanoidCtrl ctrl;
    [SerializeField] List<Rigidbody> ragdollBodies;
    [SerializeField] List<Collider> ragdollColliders;
    [SerializeField] List<Rigidbody> disableBodies;
    [SerializeField] List<Collider> disableColliders;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
        this.LoadRagdoll();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.GetComponent<HumanoidCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void LoadRagdoll()
    {
        if (this.ragdollBodies.Count > 0) return;
        this.ragdollBodies = this.ctrl.Model.GetComponentsInChildren<Rigidbody>().ToList<Rigidbody>();
        this.ragdollColliders = this.ctrl.Model.GetComponentsInChildren<Collider>().ToList<Collider>();
        this.disableBodies = this.ctrl.GetComponents<Rigidbody>().ToList<Rigidbody>();
        this.disableColliders = this.ctrl.GetComponents<Collider>().ToList<Collider>();
        Debug.LogWarning(transform.name + ": LoadRagdoll", gameObject);
    }

    [ProButton]
    public void EnableRagdoll()
    {
        this.ctrl.Animator.enabled = false;
        SetRagdollState(true);
    }

    [ProButton]
    public void DisableRagdoll()
    {
        this.ctrl.Animator.enabled = true;
        SetRagdollState(false);
    }

    void SetRagdollState(bool state)
    {
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }

        foreach (Collider collider in this.ragdollColliders)
        {
            collider.enabled = state;
        }

        foreach (Collider collider in this.disableColliders)
        {
            collider.enabled = !state;
        }

        foreach (Rigidbody rb in this.disableBodies)
        {
            rb.isKinematic = state;
        }
    }

    public void AddForce(Vector3 direction, float forceAmount = 500f)
    {
        this.EnableRagdoll();
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.AddForce(direction * forceAmount, ForceMode.Impulse);
        }
    }

}
