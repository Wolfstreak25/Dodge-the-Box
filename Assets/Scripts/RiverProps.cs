using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverProps : MonoBehaviour
{
    private Vector3 origionalPosition;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start ()
    {
        origionalPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rigidbody.AddForce(Vector3.right * 10.0f * Time.fixedDeltaTime);
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RiverEnd")
        {
            transform.position = origionalPosition;
        }
    }
}
