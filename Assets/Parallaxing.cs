using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    static Camera mainCam;

    static float AspectRatio
    {
        get { return Screen.width / (float)Screen.height; }
    }

    #region Constants

    const float NEAR_CLIP_PLANE = 0.1F;
    const float FAR_CLIP_PLANE = 15.0F;
    const float VERTICAL_OBLIQUE = .05F;

    static float HORIZONTAL_OBLIQUE;
    static float PARALLAX_STEP;

    #endregion

    static float TOP, BOTTOM, LEFT, RIGHT;

    void Start()
    {
        SetupCamera();
    }

    void LateUpdate()
    {
        Vector2 camPos = mainCam.transform.position;

        //bool isXNegative = camPos.x < 0;
        //bool isYNegative = camPos.y < 0;

        LEFT = -HORIZONTAL_OBLIQUE + camPos.x * -PARALLAX_STEP;
        RIGHT = HORIZONTAL_OBLIQUE + camPos.x * -PARALLAX_STEP;

        TOP = VERTICAL_OBLIQUE + camPos.y * -PARALLAX_STEP;
        BOTTOM = -VERTICAL_OBLIQUE + camPos.y * -PARALLAX_STEP;

        //Debug.LogFormat("Left: {0}, Right: {1}, Top: {2}, Bottom: {3}, iSY: {4}, iSX: {5}", LEFT, RIGHT, TOP, BOTTOM, isYNegative, isXNegative);

        mainCam.projectionMatrix = PerspectiveOffCenter(LEFT, RIGHT, BOTTOM, TOP, NEAR_CLIP_PLANE, FAR_CLIP_PLANE);
    }

    void SetupCamera()
    {
        mainCam = Camera.main;
        mainCam.orthographic = false;
        mainCam.nearClipPlane = NEAR_CLIP_PLANE;
        mainCam.farClipPlane = FAR_CLIP_PLANE;
        HORIZONTAL_OBLIQUE = VERTICAL_OBLIQUE * AspectRatio;
        PARALLAX_STEP = NEAR_CLIP_PLANE / FAR_CLIP_PLANE;
    }

    static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
    {
        float x = 2.0F * near / (right - left);
        float y = 2.0F * near / (top - bottom);
        float a = (right + left) / (right - left);
        float b = (top + bottom) / (top - bottom);
        float c = -(far + near) / (far - near);
        float d = -(2.0F * far * near) / (far - near);
        float e = -1.0F;
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = x;
        m[0, 1] = 0;
        m[0, 2] = a;
        m[0, 3] = 0;
        m[1, 0] = 0;
        m[1, 1] = y;
        m[1, 2] = b;
        m[1, 3] = 0;
        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = c;
        m[2, 3] = d;
        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = e;
        m[3, 3] = 0;
        return m;
    }

}