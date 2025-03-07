using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El personaje a seguir
    public float smoothSpeed = 5f;
    private Vector3 initialOffset;
    public float minZoom = 30f;
    public float maxZoom = 90f;
    public float zoomSpeed = 10f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom); // Asegurar que el FOV inicial esté dentro de los límites
        if (target != null)
        {
            initialOffset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;
        
        // Mantener el mismo enfoque inicial
        Vector3 desiredPosition = target.position + initialOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Control de zoom con la rueda del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            cam.fieldOfView -= scroll * zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
        }
    }
}
