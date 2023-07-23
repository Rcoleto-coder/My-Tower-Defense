using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30.0f;
    public float panBorderThickness = 10.0f;

    public float scrollSpeed = 5.0f;
    public float minY = 10.0f;
    public float maxY = 80.0f;

    // Update is called once per frame
    void Update()
    {
        if (GManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        // Get reference to the mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 position = transform.position;

        position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        position.y = Mathf.Clamp(position.y, minY, maxY);

        transform.position = position;

    }
}
