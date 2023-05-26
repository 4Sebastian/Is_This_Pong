using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linkedPortalManager : MonoBehaviour
{
    private Vector3 relativeOffsetVector = new Vector3(0f, BallPowerUps.offsetBallSize + 0.5f + 0.3f, 0f);

    public GameObject thisPortal;
    public GameObject coupledPortal;

    private int cnt = 0;

    private static int portal_limit = 5;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Ball") && cnt == 0){
            changePosition(collision.gameObject);
            changeVelocity(collision.gameObject);
            setCounter();
            coupledPortal.transform.GetChild(2).GetComponent<linkedPortalManager>().setCounter();
        }
        
    }

    void FixedUpdate()
    {
        if (cnt > 0){
            cnt--;
        }
    }

    private void changePosition(GameObject ball){
        ball.transform.position = coupledPortal.transform.TransformPoint(relativeOffsetVector);
    }

    private void changeVelocity(GameObject ball){
        Vector2 relVelocity = thisPortal.transform.InverseTransformDirection(ball.GetComponent<Rigidbody2D>().velocity);
        ball.GetComponent<Rigidbody2D>().velocity = coupledPortal.transform.TransformDirection(relVelocity);
    }

    public void setCounter(){
        cnt = portal_limit;
    }

}
