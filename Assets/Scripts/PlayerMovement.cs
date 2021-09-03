using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float XRotation;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivty;
    [SerializeField] private float Jumpforce;

    void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f ,Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MovePlayerCamera();
    }
    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y,MoveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
        }
    }
    private void MovePlayerCamera()
    {
        XRotation -= PlayerMouseInput.y * Sensitivty;

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivty, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(XRotation,0,0);
    }
 }
