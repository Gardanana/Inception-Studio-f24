using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Bush : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private Vector2 bounceForce;
    [SerializeField] private float maxKick;
    AudioSource pop;
    public RuinBuilder ruinBuilder;
    public RuinManager ruinManager;
    [SerializeField] AudioClip bushSound;

    Wireframe wireFrame;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ruinBuilder = FindFirstObjectByType<RuinBuilder>();
        ruinManager = FindFirstObjectByType<RuinManager>();
        wireFrame = GetComponent<Wireframe>();
        pop = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.tag == "Player" && !wireFrame.isWireFrame)
            {
                Vector3 kickForce = (Vector3.up * bounceForce.y) + (collision.relativeVelocity * bounceForce.x);

                rb.AddForce(kickForce.normalized * (Mathf.Clamp(kickForce.magnitude, -maxKick, maxKick)), ForceMode.Impulse);

                pop.clip = bushSound;
                pop.Play();
            }
            else
            {
                ruinManager.BuildRuin(gameObject, collision.gameObject);
            }
        }
    }
}
