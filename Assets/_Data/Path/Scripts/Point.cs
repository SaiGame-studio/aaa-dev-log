using UnityEngine;

public class Point : SaiBehaviour
{
    [SerializeField] protected Point nextPoint;
    public Point NextPoint => nextPoint;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNextPoint();
    }

    protected virtual void LoadNextPoint()
    {
        Transform parent = transform.parent;
        if (parent == null) return;

        int index = transform.GetSiblingIndex();
        int siblingCount = parent.childCount;

        if (index + 1 < siblingCount)
        {
            Transform nextTransform = parent.GetChild(index + 1);
            Point next = nextTransform.GetComponent<Point>();
            this.nextPoint = next;
        }
    }
}
