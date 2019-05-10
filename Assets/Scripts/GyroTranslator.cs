using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GyroTranslator : MonoBehaviour
{
    [Header("Constraints")]
    [SerializeField] Vector2 lowerLeftBound = new Vector2(-4.95f, -2.4f);
    [SerializeField] Vector2 upperRightBound = new Vector2(4.95f, 2.4f);

    [Header("")]
    [SerializeField] float editorDampening = 15;
    [SerializeField] float deviceDampening = 10;
    [SerializeField] float resetSpeed = 5;

    static float dampening;

    Vector3 lastPos, delta;
    float yRotation, xRotation;
    bool isEditor;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Input.gyro.enabled = true;
        isEditor = Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor;
        dampening = 1 / (isEditor ? editorDampening : deviceDampening);
    }

    void Update()
    {
        if (isEditor)
        {
            if (Input.GetMouseButtonDown(0))
                lastPos = Input.mousePosition;
            else if (Input.GetMouseButton(0))
            {
                delta = Input.mousePosition - lastPos;

                transform.Translate(delta * dampening);

                lastPos = Input.mousePosition;
            }
        }
        else
        {
            if (Input.touchCount == 2) ResetParallax();

            xRotation += Input.gyro.rotationRateUnbiased.x;
            yRotation += Input.gyro.rotationRateUnbiased.y;
            transform.position = CheckBounds(new Vector3(yRotation * dampening, -xRotation * dampening, transform.position.z));
        }
    }

    void ResetParallax()
    {
        xRotation = Mathf.LerpUnclamped(xRotation, 0, Time.deltaTime * resetSpeed);
        yRotation = Mathf.LerpUnclamped(yRotation, 0, Time.deltaTime * resetSpeed);
    }

    Vector3 CheckBounds(Vector3 targetPos)
    {
        Vector3 currentPos = targetPos;

        if (currentPos.x < lowerLeftBound.x)
            currentPos.x = lowerLeftBound.x;
        if (currentPos.y < lowerLeftBound.y)
            currentPos.y = lowerLeftBound.y;
        //if (currentPos.z < lowerLeftBound.z)
        //    currentPos.z = lowerLeftBound.z;

        if (currentPos.x > upperRightBound.x)
            currentPos.x = upperRightBound.x;
        if (currentPos.y > upperRightBound.y)
            currentPos.y = upperRightBound.y;
        //if (currentPos.z > upperRightBound.z)
        //    currentPos.z = upperRightBound.z;

        return currentPos;
    }
}