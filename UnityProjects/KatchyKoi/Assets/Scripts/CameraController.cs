using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform camTransform;
    public Transform playerTransform;
    public float followRange = 2;

    // How long the object should shake for.
    public static float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 10f;
    public float decreaseFactor = 1f;
    public float decreaseShakeAmount = 0.1f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void Update()
    {
        //Follow
        if(PlayerController.movementActivated && playerTransform.position.x < followRange && playerTransform.position.x > -followRange){
            camTransform.localPosition = new Vector3(playerTransform.position.x, originalPos.y, originalPos.z);
        }
        else{
            originalPos = camTransform.localPosition;
        }
        //Shake
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * (shakeAmount) / 100;

            shakeDuration -= Time.fixedUnscaledDeltaTime * decreaseFactor;
            shakeAmount -= decreaseShakeAmount;
        }
        else if(!PlayerController.movementActivated)
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}