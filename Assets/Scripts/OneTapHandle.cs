using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTapHandle : MonoBehaviour
{
    public Transform target;
    public float speed = 10;
    public float deviceDampening = 25;
    public float lowerBound = -50f;
    public float upperBound = 7f;

    void Update()
    {
#if UNITY_EDITOR
        target.transform.position = CheckConstraints(target.transform.position + new Vector3(0, 0, Time.deltaTime * Input.GetAxisRaw("Vertical") * speed));
#else
        if (Input.touchCount == 1)
            target.transform.position = CheckConstraints(target.transform.position + new Vector3(0, 0, Time.deltaTime * speed * (Input.GetTouch(0).deltaPosition.y / deviceDampening)));
#endif
    }

    Vector3 CheckConstraints(Vector3 pos)
    {
        Vector3 targetPos = pos;

        if (targetPos.z < lowerBound)
            targetPos.z = lowerBound;
        if (targetPos.z > upperBound)
            targetPos.z = upperBound;

        return targetPos;
    }
}
