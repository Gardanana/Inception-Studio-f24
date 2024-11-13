using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)]
    float spawnRange = 10f;              // User-defined range for spawning
    CapsuleCollider trigger; 
    AudioSource leaves;

    [SerializeField]
    GameObject spawnPrefab;

    [SerializeField] int retryCount;
    [SerializeField] int maxPalmCount = 20;

    private void Start()
    {
        trigger = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider PlayerCapsule)
    {
        /*
        GameObject obj = Instantiate(spawnPrefab, Random.insideUnitSphere * 7 + transform.position, Quaternion.identity);
        obj.transform.parent = transform;
        pop = GetComponent<AudioSource>();
        pop.Play();
        Debug.Log("spawning palm");
        */

        SpawnTree(PlayerCapsule);
    }

    void SpawnTree(Collider PlayerCapsule)
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
            obj.transform.parent = transform;
            leaves = GetComponent<AudioSource>();
            leaves.Play();
            Debug.Log("spawning palm");
        }

    }
}
