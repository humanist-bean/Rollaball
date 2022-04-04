using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public float jumpHeight;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private float movementZ;
    private bool doubleJumped;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        doubleJumped = true;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
        movementZ = 0.0f;
    }

    void OnJump(InputValue movementValue)
    {
        if( rb.transform.position.y <= 0.6f){
            // do jump here
            doubleJumped = false;
            Vector3 jump = new Vector3 (0.0f, jumpHeight, 0.0f);
            rb.AddForce(jump);
        } else {
            if(!doubleJumped){
               Vector3 jump = new Vector3 (0.0f, jumpHeight, 0.0f);
                rb.AddForce(jump);
                doubleJumped = true;
            }
            
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 7)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementZ, movementY);
        rb.AddForce(movement * speed);

        // ALDERS CUSTOM CODE TO TRY AND MAKE A DOUBLE JUMP ACTION
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            ++count;

            SetCountText();
        }
        
    }
    
}
