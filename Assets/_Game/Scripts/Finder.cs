using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class Finder : MonoBehaviour
    {
        [SerializeField] private float _rayDistance = 5f;
        [SerializeField] private LayerMask _pickupLayer;

        private Camera playerCamera;
        private Grabable _grabable;

        public event Action<Grabable> onFind;
        public event Action<Grabable> onLost;

        private void Awake()
        {
            playerCamera = Camera.main;
        }

        private void Update()
        {
            Search();
        }

        private void Search()
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _rayDistance, _pickupLayer))
            {
                bool isDetect = hit.transform.TryGetComponent(out Grabable grabable);
                if (isDetect)
                {
                    Find(grabable);
                }
                else
                {
                    Lost();
                }
            }
            else
            {
                Lost();
            }
        }

        private void Find(Grabable grabable)
        {
            _grabable = grabable;
            onFind?.Invoke(grabable);
        }

        private void Lost()
        {
            onLost?.Invoke(_grabable);
            _grabable = null;
        }
    }
}