using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class FirstPersonController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float mouseSensitivity = 2f;
        public CharacterController _characterController;
        public Transform playerCamera;

        private float cameraPitch = 0f;

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            transform.Rotate(Vector3.up * mouseX);

            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
            playerCamera.localEulerAngles = Vector3.right * cameraPitch;

            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;

            _characterController.Move((move + Physics.gravity) * moveSpeed * Time.deltaTime);
        }

        // Метод для возврата текущей скорости игрока
        public Vector3 GetPlayerVelocity()
        {
            return _characterController.velocity;
        }
    }
}