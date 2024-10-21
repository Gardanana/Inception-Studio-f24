using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RuinBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject ruinPart;
    // Update is called once per frame
    void Update()
    {
            
    }
    public void BuildRuin()
    {
        Instantiate(ruinPart, transform.position, Quaternion.identity);
    }

}
