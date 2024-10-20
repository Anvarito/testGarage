using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
