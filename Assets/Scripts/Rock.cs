using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public RuinManager ruinManager;
    // Start is called before the first frame update
    void Start()
    {
        ruinManager = FindFirstObjectByType<RuinManager>();
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
}
