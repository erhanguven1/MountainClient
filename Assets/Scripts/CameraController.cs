using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float sensivity;
    [SerializeField]
    private Transform playerGraphic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += (Input.GetAxis("Mouse Y") * Vector3.left + Input.GetAxis("Mouse X") * Vector3.up) * Time.deltaTime * sensivity;
        if (Player.instance.GetVelocityXZ() > 0) //If we're moving, player should turn with our camera
        {
            //Player.instance.movementDirection.localEulerAngles = Vector3.Lerp(Player.instance.movementDirection.localEulerAngles, Vector3.up * transform.localEulerAngles.y, Time.deltaTime * 15);
            Player.instance.movementDirection.rotation = Quaternion.RotateTowards(Player.instance.movementDirection.rotation, Quaternion.Euler(Vector3.up * transform.localEulerAngles.y), 10);
        }
    }
}
