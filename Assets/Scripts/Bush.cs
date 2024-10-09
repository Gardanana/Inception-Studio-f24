using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bush : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private Vector2 bounceForce;
    [SerializeField] private float maxKick;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                Vector3 kickForce = (Vector3.up * bounceForce.y) + (collision.relativeVelocity * bounceForce.x);

                rb.AddForce(kickForce.normalized * (Mathf.Clamp(kickForce.magnitude, -maxKick, maxKick)), ForceMode.Impulse);
            }
        }
    }
}
