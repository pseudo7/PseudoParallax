using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    static Camera mainCam;

    Vector3 origCamPos;

    #region Constants
    const float FOV = 60;
    const float NEAR_CLIP_PLANE = 0.05F;
    const float FAR_CLIP_PLANE = 15.0F;

    const float PARALLAX_STEP = 0.012F;
    const float HORIZONTAL_OBLIQUE = 0.06F;
    const float VERTICAL_OBLIQUE = 0.03F;
    #endregion

    void Start()
    {
        SetupCamera();
    }

    void LateUpdate()
    {
        mainCam.projectionMatrix = PerspectiveOffCenter(-HORIZONTAL_OBLIQUE + mainCam.transform.position.x * -PARALLAX_STEP,
            HORIZONTAL_OBLIQUE + mainCam.transform.position.x * -PARALLAX_STEP,
            -VERTICAL_OBLIQUE + mainCam.transform.position.y * -PARALLAX_STEP,
            VERTICAL_OBLIQUE + mainCam.transform.position.y * -PARALLAX_STEP,
            NEAR_CLIP_PLANE, FAR_CLIP_PLANE);
    }

    void SetupCamera()
    {
        mainCam = Camera.main;
        mainCam.orthographic = false;
        mainCam.fieldOfView = FOV;
        mainCam.nearClipPlane = NEAR_CLIP_PLANE;
        mainCam.farClipPlane = FAR_CLIP_PLANE;
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

        Matrix4x4 m = new Matrix4x4()
        {
            m00 = x,
            m01 = 0,
            m02 = a,
            m03 = 0,

            m10 = 0,
            m11 = y,
            m12 = b,
            m13 = 0,

            m20 = 0,
            m21 = 0,
            m22 = c,
            m23 = d,

            m30 = 0,
            m31 = 0,
            m32 = e,
            m33 = 0,
        };
        return m;
    }
}
