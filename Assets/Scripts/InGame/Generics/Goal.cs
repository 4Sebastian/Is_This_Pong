using UnityEngine;

public class Goal : MonoBehaviour
{

    public bool isPlayer1Goal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (!isPlayer1Goal)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerScored(1);
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerScored(2);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
