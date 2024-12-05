using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    int chargeCount = 0;
    [SerializeField] int chargeMax = 5;
    [SerializeField] float pieceHoldDistance = 10f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] VisualEffect waterGlow;

    RuinPart pieceHeld;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlRuinPiece();
        if (chargeCount == 0)
        {
            waterGlow.Stop();
        }
    }

    void ControlRuinPiece()
    {
        if (pieceHeld != null)
        {
            Vector3 previousPos = pieceHeld.transform.position;
            pieceHeld.transform.position = transform.position + transform.forward * pieceHoldDistance;

            foreach (RaycastHit hit in Physics.RaycastAll(previousPos, Vector2.down, pieceHoldDistance + 10f))
            {
                if (hit.collider.gameObject.tag == "Sand")
                {
                        pieceHeld.transform.position = new Vector3(pieceHeld.transform.position.x, hit.point.y + pieceHeld.distanceFromGround, pieceHeld.transform.position.z);
                        break;
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pieceHeld == null) PickupRuin();
            else DropRuin();
        }

        if (pieceHeld != null)
        {
            pieceHeld.transform.Rotate(Vector3.up * (Input.mouseScrollDelta * (rotationSpeed * Time.deltaTime)));
        }
    }

    void PickupRuin()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(transform.position, Camera.main.transform.forward);
            if(hit.collider.gameObject.GetComponent<RuinPart>() != null)
            {
                pieceHeld = hit.collider.gameObject.GetComponent<RuinPart>();
                
            }
        }
    }

    void DropRuin()
    {
        pieceHeld = null;
    }

    public bool ExpendCharge()
    {
        if (chargeCount > 0)
        {
            chargeCount--;
            return true;
        }
        else return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            waterGlow.Play();
            chargeCount = chargeMax;
        }
    }

}
