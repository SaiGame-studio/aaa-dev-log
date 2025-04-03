using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PortalCameraView : SaiBehaviour
{
    public Transform playerCamera;

    public Transform portalIn;
    public Transform portalOut;

    public Vector3 playerOffset;

    void LateUpdate()
    {
        this.UpdatingCameraView();
        this.UpdatingCameraPosition();
    }

    protected virtual void UpdatingCameraView()
    {
        Vector3 euler = transform.eulerAngles;
        euler.y = this.playerCamera.eulerAngles.y;
        transform.eulerAngles = euler;
    }

    protected virtual void UpdatingCameraPosition()
    {
        this.playerOffset = this.playerCamera.position - this.portalIn.position;
        transform.position = this.portalOut.position + this.playerOffset;
    }
}
