using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessController : MonoBehaviour
{
    public GameObject postProcessObject;
    public Transform cameraPosition;
    static GameObject postProcessObj;
    static Transform cameraPos;
    static Vector3 defaultPos = new Vector3(0, 0, 0);

    void Start()
    {
        postProcessObj = postProcessObject;
        cameraPos = cameraPosition;
    }

    public static void StartPostProcess()
    {
        postProcessObj.transform.position = cameraPos.position;
    }

    public static void EndPostProcess()
    {
        postProcessObj.transform.position = defaultPos;
    }
}

