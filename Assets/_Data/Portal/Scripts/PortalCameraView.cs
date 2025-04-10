using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PortalCameraView : SaiBehaviour
{
    public Camera currentCamera;
    public Transform playerCamera;

    public Transform portalIn;
    public Transform portalOut;

    public Vector3 playerOffset;
    public bool syncRotationX = true;
    public bool syncRotationY = true;
    public bool syncRotationZ = false;
    public bool syncPosition = true;

    void LateUpdate()
    {
        this.UpdatingCameraView();
        this.UpdatingCameraPosition();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LazyLoad();
    }

    protected virtual void LazyLoad()
    {
        this.playerCamera = Camera.main.transform;
        this.portalIn = GameObject.Find("GateA").transform;
        this.portalOut = GameObject.Find("GateB").transform;
        this.currentCamera = GetComponent<Camera>();
    }

    protected virtual void UpdatingCameraView()
    {
        Vector3 euler = transform.eulerAngles;
        if (this.syncRotationY) euler.y = this.playerCamera.eulerAngles.y;
        if (this.syncRotationX) euler.x = this.playerCamera.eulerAngles.x;
        if (this.syncRotationZ) euler.z = this.playerCamera.eulerAngles.z;
        transform.eulerAngles = euler;
    }

    protected virtual void UpdatingCameraPosition()
    {
        if (!this.syncPosition) return;
        this.playerOffset = this.playerCamera.position - this.portalIn.position;
        transform.position = this.portalOut.position + this.playerOffset;
    }
}
