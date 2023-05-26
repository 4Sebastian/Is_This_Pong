using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInstructions : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem trail;
    public Rigidbody2D rb;
    void Start()
    {
        Launch();
        setTrailColor();
    }

    void OnEnable()
    {
        Launch();
        setTrailColor();
    }

    // Update is called once per frame
    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(12 * x, 12 * y);
    }

    private void setTrailColor()
    {
        float influenceFactor = Mathf.Pow(2.71828f, rb.velocity.magnitude / (12 * 3f) - 0.5f) - 1.2f;
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
}
