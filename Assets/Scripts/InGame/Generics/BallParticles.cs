using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallParticles : MonoBehaviour
{
    public Rigidbody2D rb;
    public ParticleSystem onHit;
    public ParticleSystem trail;

    void Start()
    {
        trail.Play();
    }

    void FixedUpdate()
    {
        setTrailColor();
    }

    private void setTrailColor()
    {
        float influenceFactor = Mathf.Pow(2.71828f, rb.velocity.magnitude / (this.gameObject.GetComponent<Ball>().speed * 3f) - 0.5f) - 1.2f;
        //Debug.Log(influenceFactor);
        if (influenceFactor > 0.98f)
        {
            influenceFactor = 0.98f;
        }
        if (influenceFactor < 0.0f)
        {
            influenceFactor = 0.0f;
        }

        float orangeInfluence = 0.25f - influenceFactor;
        float redInfluence = 0.5f - influenceFactor;

        if (orangeInfluence < 0.05f)
        {
            orangeInfluence = 0.05f;
        }

        if (redInfluence < 0.1f)
        {
            redInfluence = 0.1f;
        }



        var col = trail.colorOverLifetime;
        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.gray, 0.0f), new GradientColorKey(new Vector4(1.0f, 1.0f, 0.0f), orangeInfluence), new GradientColorKey(Color.red, redInfluence) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.4f), new GradientAlphaKey(0.0f, 0.7f) });

        col.color = grad;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        onHit.Play();
    }
}