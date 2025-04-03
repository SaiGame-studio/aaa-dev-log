using UnityEngine;

public class DynamicFOVController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform portalGate;
    public Camera portalCamera;

    [Header("FOV Settings")]
    public float minFOV = 60f;
    public float maxFOV = 100f;
    public float minDistance = 1f;
    public float maxDistance = 10f;
    public float smoothSpeed = 5f;

    void Update()
    {
        if (player == null || portalGate == null || portalCamera == null) return;

        float distance = Vector3.Distance(player.position, portalGate.position);

        // Normalize khoảng cách từ 0 (gần) đến 1 (xa)
        float t = Mathf.InverseLerp(minDistance, maxDistance, distance);

        // Tính FOV tương ứng
        float targetFOV = Mathf.Lerp(maxFOV, minFOV, t);

        // Smooth transition
        portalCamera.fieldOfView = Mathf.Lerp(portalCamera.fieldOfView, targetFOV, Time.deltaTime * smoothSpeed);
    }
}
