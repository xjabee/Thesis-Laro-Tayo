using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogTaya : MonoBehaviour
{
    // Start is called before the first frame update
    int counter = 0;
    public bool isFlipped = false;
    Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            Debug.Log("i flipped");
            Destroy(this.gameObject, 1f);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("bonk");
            if (counter == 1)
                this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
            counter++;
        }
    }

}
