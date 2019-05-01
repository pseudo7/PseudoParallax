using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GyroTranslator : MonoBehaviour
{
    [SerializeField] float editorDampening = 15;
    [SerializeField] float deviceDampening = 10;

    static float zPos, dampening;

    Vector3 lastPos, delta;
    float yRotation, xRotation;
    bool isEditor;

    void Start()
    {
        zPos = transform.position.z;
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
            if (Input.GetMouseButtonDown(0)) ResetParallax();
            else if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

            xRotation += -Input.gyro.rotationRateUnbiased.x;
            yRotation += -Input.gyro.rotationRateUnbiased.y;
            transform.position = new Vector3(yRotation * dampening, xRotation * dampening, zPos);
        }
    }

    void ResetParallax()
    {
        xRotation = yRotation = 0;
    }
}