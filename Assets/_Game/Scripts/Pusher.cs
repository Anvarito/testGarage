using UnityEngine;

namespace _Game.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class Pusher : MonoBehaviour
    {
        [SerializeField] private float _pushForce;
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            if (body != null && !body.isKinematic)
            {
                Vector3 pushDirection = hit.moveDirection;
                body.AddForce(pushDirection * _pushForce, ForceMode.Impulse);
            }
        }
    }
}