using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private float _throwForce = 300;
        [SerializeField] private Transform _grabPoint;
        [SerializeField] private Finder finder;

        private Vector3 _prevPos = Vector3.zero;
        private Vector3 _currentPos = Vector3.zero;
        private Vector3 _direction = Vector3.zero;
        private bool _isHold;
        private Grabable _grabable;

        public event Action<Grabable> onGrab;
        public event Action<Grabable> onThrow;

        private void Start()
        {
            _prevPos = _grabPoint.position;

            finder.onFind += FindObj;
            finder.onLost += LostGrabbableObj;
        }

        private void LostGrabbableObj(Grabable obj)
        {
            if (!_isHold)
                _grabable = null;
        }

        private void FindObj(Grabable obj)
        {
            if (!_isHold)
                _grabable = obj;
        }

        private void Update()
        {
            _currentPos = _grabPoint.position;
            _direction = _currentPos - _prevPos;
            _prevPos = _currentPos;


            if (_grabable != null)
            {
                if (Input.GetMouseButtonDown(0))
                    Grab();
            }

            if (_isHold)
            {
                if (Input.GetMouseButtonUp(0))
                    Throw();
            }
        }

        private Vector3 GetDirection()
        {
            Vector3 dir = Vector3.ClampMagnitude(_direction * _throwForce, 10);
            return dir;
        }

        private void Throw()
        {
            _grabable.transform.parent = null;
            LockingRigidbody(false);
            Vector3 throwDirection = GetDirection();
            _grabable.Rigidbody.AddForce(throwDirection, ForceMode.Impulse);
            _isHold = false;

            onThrow?.Invoke(_grabable);

            _grabable = null;
        }

        private void Grab()
        {
            LockingRigidbody(true);
            _grabPoint.transform.position = _grabable.transform.position;
            _grabable.transform.parent = _grabPoint.transform;
            _isHold = true;

            onGrab?.Invoke(_grabable);
        }

        private void LockingRigidbody(bool isLock)
        {
            _grabable.Rigidbody.isKinematic = isLock;
            _grabable.Rigidbody.interpolation =
                isLock ? RigidbodyInterpolation.None : RigidbodyInterpolation.Interpolate;
        }
    }
}