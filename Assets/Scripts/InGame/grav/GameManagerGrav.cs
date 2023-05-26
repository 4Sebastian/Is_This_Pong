using UnityEngine;

public class GameManagerGrav : MonoBehaviour
{

    [Header("Gravitational Point")]
    public GameObject gravPoint;

    public AnimationCurve gravWeightedSpawnCurve;

    public static int numPoints = 0;
    public static int totalPoints = 4;
    public static GameObject[] gravPoints = new GameObject[totalPoints];
    private int time = 0;
    private int maxTime = 400;
    private int minTime = 150;
    private bool gravReset = false;

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (GameManager.countDone)
        {
            if (time > getTimeDelay())
            {
                if (numPoints <= totalPoints - 1)
                {
                    gravPoints[numPoints] = SpawnGravitationalPoint();
                    numPoints++;
                }
                else
                {
                    changeGravitationalPoints();
                }
                time = 0;
            }
            gravReset = false;
            time++;
        }
        else if (!gravReset)
        {
            ResetGravPoints();
        }

    }

    int getTimeDelay()
    {
        float timeInfluence = 0.1f;
        int changeInTime = this.gameObject.GetComponent<GameManager>().getPlayerScore(1) - this.gameObject.GetComponent<GameManager>().getPlayerScore(2) - (int)(time * timeInfluence);
        if (maxTime - changeInTime < minTime)
        {
            return minTime;
        }
        else
        {
            return maxTime - changeInTime + changeInTime % 50;
        }
    }

    public void ResetGravPoints()
    {
        for (int i = numPoints - 1; i >= 0; i--)
        {
            Destroy(gravPoints[i]);
            gravPoints[i] = null;
        }
        numPoints = 0;
        time = 0;
    }

    private GameObject SpawnGravitationalPoint()
    {
        return Instantiate(gravPoint, getNewGravPosition(), Quaternion.identity);
    }

    private void changeGravitationalPoints()
    {
        int num = 3;
        while (num >= 3)
        {
            num = (int)(UnityEngine.Random.value * totalPoints);
        }


        Destroy(gravPoints[num]);
        gravPoints[num] = SpawnGravitationalPoint();

    }

    private Vector3 getNewGravPosition()
    {
        float preferredDistance = 20f;
        int loopsIterated = 0;
        Vector3 pos;
        do
        {
            pos = new Vector3(UnityEngine.Random.Range(-3, 3), curveWeightedRandom(-15f, 15f), 0);
            if (loopsIterated % (50 - numPoints * 10) == 0)
            {
                preferredDistance--;

            }

            loopsIterated++;
        } while (distanceToClosestPoint(pos) < preferredDistance);
        return pos;

    }

    private float distanceToClosestPoint(Vector3 pos)
    {
        float minDistance = 10000000000f;
        for (int i = 0; i < numPoints; i++)
        {
            float diagonal = calculateDiagonal(pos, gravPoints[i]);
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

    private void playParticles(GameObject[] particles)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].GetComponent<ParticleSystem>().Play();
        }
    }

    private float curveWeightedRandom(float min, float max)
    {
        return gravWeightedSpawnCurve.Evaluate(Random.value) * (max - min) + min;
    }
}
