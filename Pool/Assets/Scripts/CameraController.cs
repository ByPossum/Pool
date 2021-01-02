using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform t_objectToFollow;
    [SerializeField] private float f_sensitivity;
    [SerializeField] private Text debugText;
    private Vector2 v_input;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowObject();
        GrabInputs();
        RotateCamera();
    }

    private void FollowObject()
    {
        transform.position = t_objectToFollow.position;
    }

    private void GrabInputs()
    {
        v_input.x = Input.GetAxis("Mouse X");
        v_input.y = Input.GetAxis("Mouse Y");
        debugText.text = v_input.ToString();
    }

    private void RotateCamera()
    {
        transform.Rotate(transform.right.normalized * v_input.y * f_sensitivity, Space.World);
        transform.Rotate(transform.up.normalized * v_input.x * f_sensitivity, Space.World);
        transform.eulerAngles = Vector3.Scale(transform.eulerAngles, Vector3.one - Vector3.forward);
    }
}
