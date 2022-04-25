using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Vector3 cameraOffset; 

    [SerializeField]
    Vector3Variable playerPosition;

    [SerializeField]
    QuaternionVariable playerRotation;

    public float sensitivity;

    Vector2 rotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rotation = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = new Vector2(
            Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity,
            Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity
            );

        rotation.y += mousePosition.x;

        rotation.x -= mousePosition.y;
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);

        transform.position = playerPosition.Get() + cameraOffset;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        playerRotation.Set(Quaternion.Euler(0, rotation.y, 0));
    }
}
