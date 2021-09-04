using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float XRotation;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float DistanceToGround;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float Sensitivty;
    [SerializeField] private float Jumpforce;

    void Start() {
        var mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;
        Cursor.visible = false;

    }

    private void Update() {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MovePlayerCamera();


    }
    private void MovePlayer() {
        float Velocity;
        if (Input.GetKey(KeyCode.LeftShift)) {
            Velocity = RunSpeed;
        } else {
            Velocity = Speed;
        }
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Velocity;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);

        Debug.DrawRay(transform.position, transform.forward, Color.green);

      

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Physics.Raycast(transform.position, Vector3.down, DistanceToGround + 0.1f)) {
                PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
            }
        }
    }
    private void MovePlayerCamera() {
        XRotation -= PlayerMouseInput.y * Sensitivty;

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivty, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(XRotation, 0, 0);
    }
}
