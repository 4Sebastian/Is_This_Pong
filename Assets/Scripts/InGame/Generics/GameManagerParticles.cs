using UnityEngine;

public class GameManagerParticles : MonoBehaviour
{
    [Header("goalParticles")]
    public GameObject[] player1Particles;
    public GameObject[] player2Particles;

    void Start()
    {

    }

    void FixedUpdate()
    {

    }

    public void playParticles(int player)
    {
        GameObject[] particles;
        if (player == 1)
        {
            particles = player1Particles;
        }
        else
        {
            particles = player2Particles;
        }
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].GetComponent<ParticleSystem>().Play();
        }
    }
}
