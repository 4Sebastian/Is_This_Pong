using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private bool launchDone = false;
    private Vector3 startPosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (!launchDone && GameManager.countDone)
        {
            Launch();
            launchDone = true;
        }
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        launchDone = false;
    }
}
