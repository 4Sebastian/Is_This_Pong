
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGrav : MonoBehaviour
{

    public Rigidbody2D rb;
    private float maxForce = 175f;
    private float maxDistanceForGravPoints = 1000f;
    private float angleForceInfluencer = 0.0005f;
    private float automaticSpeedInfluencer = 20f;
    private float distanceInfluencer = 0.7f;
    private float gravPointForce = 175f;

    void Start()
    {
    }

    void FixedUpdate()
    {
        for (int i = 0; i < GameManagerGrav.numPoints; i++)
        {
            rb.AddForce(getDirectionalForce(GameManagerGrav.gravPoints[i]));
        }
        if (rb.velocity.magnitude < this.gameObject.GetComponent<Ball>().speed * 2f)
        {
            rb.AddForce(rb.velocity.normalized * automaticSpeedInfluencer);
        }
        else if (rb.velocity.magnitude > this.gameObject.GetComponent<Ball>().speed * 3f)
        {
            rb.AddForce(-rb.velocity.normalized * automaticSpeedInfluencer * 2);
        }

        if (GameManagerGrav.numPoints > 0)
        {
            float difDistance = maxDistanceForGravPoints - Mathf.Pow(distanceToClosestPoint(transform.position, GameManagerGrav.gravPoints), 3);
            if (difDistance < 0)
            {
                difDistance = 0;
            }
        }
    }





    Vector3 getDirectionalForce(GameObject gravPoint)
    {
        float x = gravPoint.transform.position.x - transform.position.x;
        float y = gravPoint.transform.position.y - transform.position.y;
        float diagonal = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) * distanceInfluencer;
        Vector3 force = new Vector3(x / diagonal * gravPointForce / Mathf.Pow(diagonal, 2f), y / diagonal * gravPointForce / Mathf.Pow(diagonal, 2f), 0);
        float angle = Vector3.Angle(rb.velocity, force);

        force = new Vector3(force.x * (1 - Mathf.Pow(angle * angleForceInfluencer, 4)), force.y * (1 - Mathf.Pow(angle * angleForceInfluencer, 4)), 0);

        if (force.magnitude > maxForce)
        {
            Vector3 normalForce = force.normalized;
            force = new Vector3(maxForce * normalForce.x * (1 - Mathf.Pow(angle * angleForceInfluencer, 4)), maxForce * normalForce.y * (1 - Mathf.Pow(angle * angleForceInfluencer, 4)), 0);
        }

        return force;
    }

    private float distanceToClosestPoint(Vector3 pos, GameObject[] positions)
    {
        float minDistance = 10000000000f;
        for (int i = 0; i < GameManagerGrav.numPoints; i++)
        {
            float diagonal = calculateDiagonal(pos, positions[i]);
            if (diagonal < minDistance)
            {
                minDistance = diagonal;
            }
        }
        return minDistance;
    }

    private float calculateDiagonal(Vector3 gravPoint1, GameObject gravPoint2)
    {
        float x = gravPoint1.x - gravPoint2.transform.position.x;
        float y = gravPoint1.y - gravPoint2.transform.position.y;
        return Mathf.Abs(Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)));
    }

}
