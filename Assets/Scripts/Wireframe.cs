using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wireframe : MonoBehaviour
{
    public bool isWireFrame = true;

    public GameObject wireFrameBody;
    public GameObject normalBody;
    public AudioSource wireAudio;
    [SerializeField] public AudioClip chime;
    [SerializeField] public AudioClip crunch;

    Rigidbody rb;

    public float timer = 90f;

    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        SetWireframe();
        wireAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNormal()
    {
        wireFrameBody.SetActive(false);
        normalBody.SetActive(true);

        if(rb != null && canMove)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        wireAudio.clip = chime;
        wireAudio.Play();

        isWireFrame = false;

        Invoke("SetWireframe", timer);
    }

    public void SetWireframe()
    {
        wireFrameBody.SetActive(true);
        normalBody.SetActive(false);

        if (rb != null && canMove)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        isWireFrame = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && isWireFrame)
        {
            if(collision.gameObject.GetComponent<PlayerController>().ExpendCharge())
            {
                SetNormal();
            }
        }
    }
}
