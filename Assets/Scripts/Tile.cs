using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    GameObject placedObject;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void PlaceObject(GameObject newObject)
    {
        if(placedObject == null)
        {
            placedObject = newObject;
        }
    }

    public GameObject GetObject()
    {
        return placedObject;
    }

    public void SetVisibility(bool visible)
    {
        meshRenderer.enabled = visible;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
