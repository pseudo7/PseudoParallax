using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoRotator : MonoBehaviour
{
    public Transform[] transforms;
    public Vector3 eulerAngle = new Vector3(0, 60, 0);
    public float deviceDampening = 15;

    void Update()
    {
#if UNITY_EDITOR

        foreach (Transform item in transforms)
            item.Rotate(eulerAngle * Time.deltaTime * Input.GetAxisRaw("Horizontal"));
#else
        foreach (Transform item in transforms)
            item.Rotate(eulerAngle * Time.deltaTime * Input.GetTouch(0).deltaPosition.x / -deviceDampening);
#endif
    }
}
