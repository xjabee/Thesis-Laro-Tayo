using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPalosebo : MonoBehaviour
{
    public MeshRenderer kawayan;
    public Animator anim;
    public Rigidbody rb;

    public float jumpForce = 7;
    public float harderJumpForce = 5 ;
    public float hardestJumpForce = 3;
    private float defaultJumpForce;

    public bool pressedJump = false;
    public bool justJumped = false;
    public bool holdingOn = false;

    private float jumpCount = 0;

    public float maxHoldTime = 1.5f;
    private float holdCount;

    public float heightPerc1 = 50;
    public float heightPerc2 = 80;
    [SerializeField]private float heightCheck1;
    [SerializeField]private float heightCheck2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        defaultJumpForce = jumpForce;

        //getting a percentage of the kawayan's height to be used as checkpoints for changing difficulty
        heightCheck1 = Mathf.Lerp(0, kawayan.bounds.size.y, heightPerc1/100); 
        heightCheck2 = Mathf.Lerp(0, kawayan.bounds.size.y, heightPerc2/100);
    }



    /*
     * game sequence is 
     * climb -> hold -> climb 
     * repeat until the player reaches the top, after x seconds of holding on the player stars falling and must hold again in order to stop falling.
     * 
     * 
     */

    // Update is called once per frame
    void Update()
    {
        if ( jumpCount == 0 && Input.GetKeyDown(KeyCode.Space) ) //climbing
        {
            holdingOn = false;
            pressedJump = true;
            rb.isKinematic = false;
            holdingOn = false;
            jumpCount = 1;
            anim.enabled = true;
            anim.SetBool("isClimbing", true);
        }

        if ( Input.GetKeyDown(KeyCode.M) ) //holding on 
        {
            holdingOn = true;
            rb.isKinematic = true;
            anim.enabled = false;
            jumpCount = 0;
            holdCount = 0;
        }

        if ( holdingOn )
        {
            holdCount += Time.deltaTime;
            if (holdCount > maxHoldTime)
            {
                rb.isKinematic = false;
                Debug.Log("hold count is longer than max hold");
            }
        }

        HeightChecking();
    }

    private void HeightChecking() //check height of player, higher player weaker jump
    {
        if ( transform.position.y >= heightCheck1 )
        {
            jumpForce = harderJumpForce;

            if ( transform.position.y >= heightCheck2 )
            {
                jumpForce = hardestJumpForce;
            }
        }

        if ( transform.position.y <= heightCheck2 )
        {
            jumpForce = harderJumpForce;

            if ( transform.position.y <= heightCheck1 )
            {
                jumpForce = defaultJumpForce;
            }

        }

    }

    private void FixedUpdate()
    {
        if ( pressedJump )
        {
            rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            pressedJump = false;
            justJumped = true;
            anim.SetBool("isClimbing", false);
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.transform.tag == "ground")
        {
            jumpCount = 0;
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == "goal")
        {
            rb.MovePosition(c.transform.position);
            Debug.Log("Goal");
            rb.isKinematic = true;
            anim.enabled = false;
        }
    }

}
