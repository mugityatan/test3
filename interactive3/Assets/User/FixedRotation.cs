using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour {

    public float maxAngle = 15.0f;
    public float minAngle = -15.0f;
    public float speed = 1.5f;
    public bool InpotKeyTarget = true;

    void Update()
    {
        float deltaZ = Input.GetAxis("Horizontal");
        float rotateZ = (transform.eulerAngles.z > 180) ? transform.eulerAngles.z - 360 : transform.eulerAngles.z;
        float angleZ = Mathf.Clamp(rotateZ + ((InpotKeyTarget) ? -deltaZ : deltaZ) * speed, minAngle, maxAngle);
        angleZ = (angleZ < 0) ? angleZ + 360 : angleZ;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }

}
