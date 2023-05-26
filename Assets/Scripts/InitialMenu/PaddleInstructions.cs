using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleInstructions : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector3(2, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.velocity = -rb.velocity;
    }


}
