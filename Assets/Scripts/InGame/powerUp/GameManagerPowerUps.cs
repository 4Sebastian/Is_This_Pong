using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPowerUps : MonoBehaviour
{
    [Header("Power Up")]
    public GameObject powerUp;
    public static int numPoints = 0;
    public static int totalPoints = 2;
    public static GameObject[] powerPoints = new GameObject[totalPoints];
    private int time = 0;
    private int maxTime = 400;
    private int minTime = 150;
    private bool powerReset = false;
    void Start()
    {
        Random.InitState(10);
    }

    void FixedUpdate()
    {
        if (GameManager.countDone)
        {
            if (time > getTimeDelay())
            {
                if (numPoints <= totalPoints - 1)
                {
                    powerPoints[numPoints] = SpawnPowerPoint();
                    updatePowerPoint(numPoints);
                    numPoints++;
                }
                else
                {
                    changePowerPoints();
                }
                time = 0;
            }
            powerReset = false;
            time++;
        }
        else if (!powerReset)
        {
            ResetPowerPoints();
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

    public void ResetPowerPoints()
    {
        for (int i = numPoints - 1; i >= 0; i--)
        {
            Destroy(powerPoints[i]);
            powerPoints[i] = null;
        }
        numPoints = 0;
        time = 0;
    }

    private GameObject SpawnPowerPoint()
    {
        return Instantiate(powerUp, getNewPowerPosition(), Quaternion.identity);
    }

    private void updatePowerPoint(int i)
    {
        int selectedType = Mathf.RoundToInt(Random.Range(0f, powerUpPoint.inPlaytypes.Length - 1));
        bool hasPlayed = true;
        if (powerUpPoint.hasEveryPowerPlayed())
        {
            powerUpPoint.resetInPlayTypes();
        }
        do
        {
            if (powerUpPoint.inPlaytypes[selectedType] == true)
            {
                selectedType++;
                if (selectedType >= powerUpPoint.inPlaytypes.Length)
                {
                    selectedType = 0;
                }
            }
            else
            {
                powerUpPoint.inPlaytypes[selectedType] = true;
                hasPlayed = false;
            }


        } while (hasPlayed);
        powerPoints[i].GetComponent<powerUpPoint>().setAuraType(powerUpPoint.types[selectedType]);
    }

    private void changePowerPoints()
    {
        int num = 3;
        while (num >= 3)
        {
            num = (int)(UnityEngine.Random.value * totalPoints);
        }


        Destroy(powerPoints[num]);
        powerPoints[num] = SpawnPowerPoint();

    }

    private Vector3 getNewPowerPosition()
    {
        float preferredDistance = 20f;
        int loopsIterated = 0;
        Vector3 pos;
        do
        {
            pos = new Vector3(UnityEngine.Random.Range(-6, 6), UnityEngine.Random.Range(-4, 4), 0);
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
            float diagonal = calculateDiagonal(pos, powerPoints[i]);
            if (diagonal < minDistance)
            {
                minDistance = diagonal;
            }
        }
        return minDistance;
    }

    private float calculateDiagonal(Vector3 pos1, GameObject pos2)
    {
        float x = pos1.x - pos2.transform.position.x;
        float y = pos1.y - pos2.transform.position.y;
        return Mathf.Abs(Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)));
    }

    public static void removeNullPowerPoints()
    {
        GameObject[] temp = new GameObject[powerPoints.Length];
        int cnt = 0;
        foreach (GameObject powerPoint in powerPoints)
        {
            if (powerPoint != null)
            {
                temp[cnt++] = powerPoint;
            }
        }
        powerPoints = temp;
    }
}
