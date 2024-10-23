using UnityEngine;

namespace _Game.Scripts
{
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform playerCamera;

        private float cameraPitch = 0f;
        public float jumpHeight = 2f;
        public float gravity = -9.81f;
        private Vector3 _jumpVelocity;

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            RotateAndPitch();
            CalculateJumpVelocity();
            var move = GetMoveDirection();

            Vector3 finalMove = move * moveSpeed + _jumpVelocity;
            _characterController.Move(finalMove * Time.deltaTime);
        }

        private void CalculateJumpVelocity()
        {
            if (_characterController.isGrounded && _jumpVelocity.y < 0)
            {
                _jumpVelocity.y = -2f;
            }

            if (_characterController.isGrounded && Input.GetButtonDown("Jump"))
            {
                _jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            _jumpVelocity.y += gravity * Time.deltaTime;
        }

        private Vector3 GetMoveDirection()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = _characterController.transform.right * moveX +
                           _characterController.transform.forward * moveZ;
            return move;
        }

        private void RotateAndPitch()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            _characterController.transform.Rotate(Vector3.up * mouseX);

            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
            playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        }
    }
}