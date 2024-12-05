using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinPart : MonoBehaviour
{
    public bool wireframeParent = false;

    [SerializeField] float groupRadius = 15f;
    [SerializeField] int groupSize = 5;

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material groupedMaterial;

    public float distanceFromGround;

    public List<RuinPart> neighbors;


    // Start is called before the first frame update
    void Start()
    {
        neighbors = new List<RuinPart> ();
        if (wireframeParent) distanceFromGround += 5f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckClusterStatus();
    }

    public void SetStatus(bool state)
    {
        if (state)
        {
            meshRenderer.material = groupedMaterial;
        }
        else
        {
            meshRenderer.material = normalMaterial;
        }
    }

    void RecurseEveryNeighbor(RuinPart parentPart, List<RuinPart> neighborNetwork)
    {
        foreach(RuinPart neighbor in parentPart.neighbors)
        {
            if (!neighborNetwork.Contains(neighbor))
            {
                neighborNetwork.Add(neighbor);
                RecurseEveryNeighbor(neighbor, neighborNetwork);
            }
        }
    }


    void CheckClusterStatus()
    {
        foreach (RuinPart ruin in FindObjectsOfType<RuinPart>())
        {
            if(neighbors.Contains(ruin))
            {
                if (Vector3.Distance(transform.position, ruin.transform.position) > groupRadius)
                {
                    neighbors.Remove(ruin);
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, ruin.transform.position) <= groupRadius)
                {
                    neighbors.Add(ruin);
                }
            }
        }

        List<RuinPart> neighborWeb = new List<RuinPart> ();

        RecurseEveryNeighbor(this, neighborWeb);


        if (neighborWeb.Count > groupSize - 1)
        {
            meshRenderer.material = groupedMaterial;
        }
        else {
            meshRenderer.material = normalMaterial;
        }
    }
}
