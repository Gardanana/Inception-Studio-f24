using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)]
    float spawnRange = 10f;              // User-defined range for spawning

    [SerializeField]
    GameObject spawnPrefab;

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider PlayerCapsule)
    {
          GameObject obj = Instantiate(spawnPrefab);

          obj.transform.position = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                Random.Range(0f, 0f),
                Random.Range(-spawnRange, spawnRange)
                );
          obj.transform.parent = transform;
        Debug.Log("spawning palm");
    }
}
