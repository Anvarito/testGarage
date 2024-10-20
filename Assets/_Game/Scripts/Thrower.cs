using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Thrower : MonoBehaviour
    {
        [SerializeField] private float _throwForce = 300;
        private Vector3 _prevPos = Vector3.zero;
        private Vector3 _currentPos = Vector3.zero;
        private Vector3 _direction = Vector3.zero;

        private void Start()
        {
            _prevPos = transform.position;
        }

        private void Update()
        {
            _currentPos = transform.position;
            _direction = _currentPos - _prevPos;
            _prevPos = _currentPos;
        }

        private Vector3 GetDirection()
        {
            Vector3 dir = Vector3.ClampMagnitude(_direction * _throwForce, 10);
            return dir;
        }

        public void Throw(Rigidbody pickedRigidbody)
        {
            Vector3 throwDirection = GetDirection();
            pickedRigidbody.AddForce(throwDirection, ForceMode.Impulse);
        }
    }
}