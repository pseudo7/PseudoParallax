using UnityEngine;

public class Trombone : MonoBehaviour
{
    public Transform target;
    public Camera mainCam;
    public float initialDistance = 15;

    private float initHeightAtDist;
    void Start()
    {
        Setup();
        ApplyFOV();
    }

    void LateUpdate()
    {
#if UNITY_EDITOR
        if (Input.anyKey)
            ApplyFOV();
#else
        if (Input.touchCount == 1)
            ApplyFOV();
#endif
    }

    float FrustumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(mainCam.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    float FOVForHeightAndDistance(float height, float distance)
    {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    void Setup()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        initHeightAtDist = FrustumHeightAtDistance(distance);
        mainCam.transform.position += Vector3.forward * initialDistance;
    }

    void ApplyFOV()
    {
        float currDistance = Vector3.Distance(transform.position, target.position);
        mainCam.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);
    }
}
