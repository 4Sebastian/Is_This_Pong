using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemodeSelection : MonoBehaviour
{
    private bool spawnGravPoints = false;
    private bool spawnPowerPoints = false;



    // Start is called before the first frame update
    void Start()
    {
        spawnGravPoints = StaticGameInfo.getSpawnGravPoints();
        spawnPowerPoints = StaticGameInfo.getSpawnPowerPoints();

        if(!spawnGravPoints){
            Destroy(this.GetComponent<GameManagerGrav>());
        }
        if(!spawnPowerPoints){
            Destroy(this.GetComponent<GameManagerPowerUps>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
