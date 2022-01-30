using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerGrab : MonoBehaviour
{
    private float mouseAngle;
    private GameObject playerG;
    private PlayerController playerController;
    private Collider2D playerCollider;
    private Collider2D bodyCollider;
    private Rigidbody2D rb2d;
    public LayerMask bodyLayer;
    private RaycastHit2D hit;
    public GameObject bodyObject;
    private float distance;
    private GameObject grabPoint;
    private Vector2 mouseWorldPosition;
    private Vector2 lookDir;

    public float throwPower;
    
    // Start is called before the first frame update
    void Start()
    {
        playerG = this.gameObject;
        playerController = GetComponent<PlayerController>();
        rb2d = GetComponent<Rigidbody2D>();
        bodyLayer = LayerMask.GetMask("Body");
        grabPoint = playerG.transform.GetChild(1).gameObject;
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mouseWorldPosition - rb2d.position;
        if (Input.GetMouseButton(0) && playerController.isSoul)
        {
            //UnityEngine.Debug.Log("IDIOT");

            Debug.DrawRay(transform.position, lookDir, Color.red, 0f);

            hit = Physics2D.Linecast(transform.position, mouseWorldPosition, bodyLayer);
            
            //Is player clicking on body, is not player touching body
            if (hit.collider != null && !playerCollider.IsTouching(hit.collider))
            {
                bodyObject = hit.collider.gameObject;
                distance = Vector2.Distance(playerG.transform.position, bodyObject.transform.position);

                if (distance < 5f)
                {
                    GrabBody();
                }
            }
            else
            {
                Debug.Log("Nothing");
            }
        }

        if (Input.GetMouseButton(1))
        {
            ThrowBody();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bodyObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
    
    /* UTILITY FUNCTIONS */
    
    private void GrabBody()
    {
        bodyObject.GetComponent<BoxCollider2D>().isTrigger = true;
        bodyObject.GetComponent<Rigidbody2D>().isKinematic = true;
        bodyObject.transform.position = grabPoint.transform.position;
        bodyObject.transform.parent = grabPoint.transform;
    }

    private void ThrowBody()
    {
        bodyObject.transform.parent = null;
        bodyObject.GetComponent<Rigidbody2D>().isKinematic = false;
        bodyObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        bodyObject.GetComponent<BoxCollider2D>().isTrigger = false;
        bodyObject.GetComponent<Rigidbody2D>().AddForce(lookDir * throwPower, ForceMode2D.Impulse);
    }
}