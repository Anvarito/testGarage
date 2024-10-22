using UnityEngine;

public class Grabable : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
