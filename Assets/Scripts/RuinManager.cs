using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuinManager : MonoBehaviour
{
    [SerializeField] List<GameObject> ruinPiecePrefabs;

    [SerializeField] TextMeshProUGUI guideText;

    int ruinCount = 0;


    bool processing = false;

    // Update is called once per frame
    void Update()
    {

    }

    GameObject DeterminePiece(GameObject object1, GameObject object2)
    {
        if (object1.tag == "Bush" && object2.tag == "Bush")
        {
            if ((object1.GetComponent<Wireframe>().isWireFrame && !object2.GetComponent<Wireframe>().isWireFrame) || (!object1.GetComponent<Wireframe>().isWireFrame && object2.GetComponent<Wireframe>().isWireFrame))
            {
                return ruinPiecePrefabs[10];
            }
            else
            {
                return ruinPiecePrefabs[4];
            }
        }
        else if (object1.tag == "Rock" && object2.tag == "Rock")
        {
            if ((object1.GetComponent<Wireframe>().isWireFrame && !object2.GetComponent<Wireframe>().isWireFrame) || (!object1.GetComponent<Wireframe>().isWireFrame && object2.GetComponent<Wireframe>().isWireFrame))
            {
                return ruinPiecePrefabs[5];
            }
            else
            {
                return ruinPiecePrefabs[0];
            }
        }
        else if (object1.tag == "Rock" && object2.tag == "Bush")
        {
            if (object1.GetComponent<Wireframe>().isWireFrame && !object2.GetComponent<Wireframe>().isWireFrame)
            {
                return ruinPiecePrefabs[6];
            }
            else
            {
                return ruinPiecePrefabs[1];
            }
        }

        else if (object1.tag == "Bush" && object2.tag == "Rock")
        {
            if (object1.GetComponent<Wireframe>().isWireFrame && !object2.GetComponent<Wireframe>().isWireFrame)
            {
                return ruinPiecePrefabs[9];
            }
            else
            {
                return ruinPiecePrefabs[1];
            }
        }

        else if (object1.tag == "Rock" && object2.tag == "Palm")
        {
            if (!object1.GetComponent<Wireframe>().isWireFrame && !object2.GetComponent<Wireframe>().isWireFrame)
            {
                return ruinPiecePrefabs[2];
            }
            else return null;
        }

        else if (object1.tag == "Palm" && object2.tag == "Rock")
        {
            if (!object1.GetComponent<Wireframe>().isWireFrame && !object2.GetComponent<Wireframe>().isWireFrame)
            {
                return ruinPiecePrefabs[2];
            }
            if (object1.GetComponent<Wireframe>().isWireFrame && !object2.GetComponent<Wireframe>().isWireFrame)
            {
                return ruinPiecePrefabs[7];
            }
            else return null;
        }
        else if (object1.tag == "Bush" && object2.tag == "Palm")
        {
            if (!object1.GetComponent<Wireframe>().isWireFrame && !object2.GetComponent<Wireframe>().isWireFrame)
            {
                return ruinPiecePrefabs[3];
            }
            if (!object1.GetComponent<Wireframe>().isWireFrame && object2.GetComponent<Wireframe>().isWireFrame)
            {
                return ruinPiecePrefabs[8];
            }
            else return null;
        }
        else if (object1.tag == "Palm" && object2.tag == "Bush")
        {
            if (!object1.GetComponent<Wireframe>().isWireFrame && !object2.GetComponent<Wireframe>().isWireFrame)
            {
                return ruinPiecePrefabs[3];
            }
            if (object1.GetComponent<Wireframe>().isWireFrame && !object2.GetComponent<Wireframe>().isWireFrame)
            {
                return ruinPiecePrefabs[8];
            }
            else return null;
        }
        else return null;

    }

    public void BuildRuin(GameObject object1, GameObject object2)
    {
        if (processing) return;
        
        GameObject ruinPart = DeterminePiece(object1, object2);
        Vector3 spawnPosition = object1.transform.position;

        if(ruinPart != null)
        {
            processing = true;
            Invoke("UnlockManager", 2f);

            if (ruinCount == 0)
            {
                StartCoroutine(Tutorial("Press E to hold/drop piece\nScroll to rotate"));
            }
            ruinCount++;

            GameObject ruinObject = Instantiate(ruinPart, spawnPosition, Quaternion.identity);

            if (ruinObject.GetComponent<RuinPart>().wireframeParent) ruinObject.transform.Translate(Vector3.up * 5f);

            ruinObject.transform.parent = transform;

            Destroy(object2);
            Destroy(object1);
        }

        
    }

    IEnumerator Tutorial(string text)
    {
        guideText.text = text;
        yield return new WaitForSeconds(10f);
        guideText.text = "";
    }

    void UnlockManager()
    {
        processing = false;
    }
}
