using System;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 5f;
    [SerializeField] private LayerMask _pickupLayer;
    [SerializeField] private Transform _holdRoot;

    private Camera playerCamera;
    private Pickupable _pickedObject;
    private Rigidbody _pickedRigidbody;

    public event Action<Pickupable> onFindPickupable;
    public event Action onLostPickupable;
    public event Action onHoldPickupable;

    private bool _isHold;

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (!_isHold)
        {
            SerachPickupable();
        }

        if (_pickedObject != null)
        {
            if (Input.GetMouseButtonDown(0))
                Hold();
        }

        if (_isHold)
        {
            if (Input.GetMouseButtonUp(0))
                Drop();
        }
    }


    private void SerachPickupable()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayDistance, _pickupLayer))
        {
            if (hit.transform.TryGetComponent(out Pickupable pickupable))
            {
                _pickedObject = pickupable;
                _pickedRigidbody = pickupable.Rigidbody;
                onFindPickupable?.Invoke(pickupable);
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

    private void Lost()
    {
        _pickedObject = null;
        _pickedRigidbody = null;
        onLostPickupable?.Invoke();
    }


    private void Hold()
    {
        _isHold = true;
        _pickedRigidbody.isKinematic = true;
        _pickedObject.transform.parent = _holdRoot;
        onHoldPickupable?.Invoke();
    }

    private void Drop()
    {
        _pickedObject.transform.parent = null;
        _pickedRigidbody.isKinematic = false;
        _pickedObject = null;
        _pickedRigidbody = null;
        _isHold = false;
    }
}