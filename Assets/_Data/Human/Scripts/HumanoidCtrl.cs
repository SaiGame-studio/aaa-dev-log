using UnityEngine;
using UnityEngine.AI;

public class HumanoidCtrl : SaiBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform model;
    [SerializeField] protected HumanoidRagdoll ragdoll;

    public NavMeshAgent Agent => agent;
    public Animator Animator => animator;
    public Transform Model => model;
    public HumanoidRagdoll Ragdoll => ragdoll;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAgent();
        this.LoadAnimator();
        this.LoadRagdoll();
    }

    protected virtual void LoadAgent()
    {
        if (this.agent != null) return;
        this.agent = GetComponent<NavMeshAgent>();
        Debug.LogWarning(transform.name + ": LoadAgent", gameObject);
    }

    protected virtual void LoadRagdoll()
    {
        if (this.ragdoll != null) return;
        this.ragdoll = GetComponent<HumanoidRagdoll>();
        Debug.LogWarning(transform.name + ": LoadRagdoll", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null && this.model != null) return;
        this.model = transform.Find("Model");
        this.animator = this.model.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }
}
