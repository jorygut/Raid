using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xMovement;
    public float yMovement;
    float xRotation;
    float yRotation;


    public Transform camPos;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xMovement;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * yMovement;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        camPos.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
