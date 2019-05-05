using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelGenerator : MonoBehaviour
{
    void Awake()
    {
        Steps();
    }

    void Steps()
    {
        for (int i = 0; i <= 50; i++)
        {
            GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
            tile.transform.position = Vector3.forward * i * 1.5f + transform.position;
            tile.transform.localScale = Vector3.one + transform.right * 10;
        }
    }

    void Generate()
    {
        GameObject parent = new GameObject("Tunnel");
        //parent.transform.position = new Vector3(0, 0, -5);

        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Quad);
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Quad);

        ceiling.transform.parent = parent.transform;
        floor.transform.parent = parent.transform;

        ceiling.transform.localScale = new Vector3(10, 20, 0);
        floor.transform.localScale = new Vector3(10, 20, 0);

        ceiling.transform.localEulerAngles = new Vector3(-90, 0, 0);
        floor.transform.localEulerAngles = new Vector3(90, 0, 0);

        ceiling.transform.localPosition = new Vector3(0, 2.5f, 0);
        floor.transform.localPosition = new Vector3(0, -2.5f, 0);
    }
}