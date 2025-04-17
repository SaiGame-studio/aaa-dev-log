using UnityEngine;

public class HumanoidMoving : SaiBehaviour
{
    [SerializeField] protected Point pointToGo;
    [SerializeField] protected HumanoidCtrl ctrl;
    [SerializeField] protected bool isWalking = false;
    [SerializeField] protected float targetDistance = 0;
    [SerializeField] protected float stopDistance = 1;
    [SerializeField] protected bool isReachTarget = false;

    protected void LateUpdate()
    {
        this.MoveToTarget();
        this.UpdateIsWaking();
        this.UpdateAnimator();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<HumanoidCtrl>();
        Debug.LogWarning(transform.name + ": LoadEnemyCtrl", gameObject);
    }

    protected virtual void MoveToTarget()
    {
        if (this.pointToGo == null) return;

        Vector3 postion = this.pointToGo.transform.position;

        this.targetDistance = Vector3.Distance(transform.position, this.pointToGo.transform.position);
        if (this.targetDistance < this.stopDistance)
        {
            this.ctrl.Agent.isStopped = true;
            this.LoadNextPoint();
        }
        else
        {
            this.ctrl.Agent.isStopped = false;
            this.ctrl.Agent.SetDestination(postion);
        }
    }

    protected virtual void UpdateAnimator()
    {
        this.ctrl.Animator.SetBool("IsWalking", this.isWalking);
    }

    protected virtual void LoadNextPoint()
    {
        this.pointToGo = this.pointToGo.NextPoint;
    }

    protected virtual void UpdateIsWaking()
    {
        bool isWalking = true;
        if (this.pointToGo == null) isWalking = false;
        else if (this.ctrl.Agent.isStopped) isWalking = false;
        this.isWalking = isWalking;
    }
}
