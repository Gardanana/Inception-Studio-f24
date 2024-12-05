using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Palm : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)]
    float spawnRange = 10f;              // User-defined range for spawning
    CapsuleCollider trigger;
    AudioSource leaves;
    [SerializeField] AudioClip leafSound;

    [SerializeField]
    GameObject spawnPrefab;

    [SerializeField] int retryCount;
    [SerializeField] int maxPalmCount = 20;

    public RuinManager ruinManager;

    private void Start()
    {
        trigger = GetComponent<CapsuleCollider>();
        ruinManager = FindFirstObjectByType<RuinManager>();
        leaves = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            ruinManager.BuildRuin(gameObject, collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        /*
        GameObject obj = Instantiate(spawnPrefab, Random.insideUnitSphere * 7 + transform.position, Quaternion.identity);
        obj.transform.parent = transform;
        pop = GetComponent<AudioSource>();
        pop.Play();
        Debug.Log("spawning palm");
        */
        if(!GetComponent<Wireframe>().isWireFrame && collision.gameObject.tag == "Player") SpawnTree();
    }

    void SpawnTree()
    {
        if (GameObject.FindGameObjectsWithTag("Palm").Length > maxPalmCount) return;

        bool isOnGround = false;
        Vector3 spawnPosition = Vector3.zero;

        int retries = retryCount;

        while(!isOnGround && retryCount >= 0)
        {
            spawnPosition = new Vector3(transform.position.x + (Random.Range(0f, 1f) > 0.5f ? 1 : -1) * Random.Range(trigger.radius + 1, spawnRange), 1000f, transform.position.z + (Random.Range(0f, 1f) > 0.5f ? 1 : -1) * Random.Range(trigger.radius + 1, spawnRange));

            RaycastHit hit;
            if (Physics.Raycast(spawnPosition, Vector3.down, out hit))
            {
                spawnPosition.y = hit.point.y - trigger.center.y;

                isOnGround = hit.collider.gameObject.tag == "Sand";
            }

            retryCount--;
        }

        if(isOnGround)
        {
            GameObject obj = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
            obj.transform.parent = transform.parent;
            leaves.clip = leafSound;
            leaves.Play();
            Debug.Log("spawning palm");
        }

    }
}
