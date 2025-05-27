using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkForward : MonoBehaviour
{
    public bool activateMovement = true; // Flag to control movement activation
    public float Speed = 5f; // Speed of the object
    Rigidbody rigidBody;

    void FixedUpdate()
    {
        if (!activateMovement)
        {
            // If movement is not activated, exit the method
            return;
        }
        // Move the object forward at a constant speed
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * Speed);
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

}
