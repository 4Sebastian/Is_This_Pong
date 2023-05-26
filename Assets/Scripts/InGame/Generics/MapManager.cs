using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private int selectedOuterMap = 0;
    private int selectedInnerObstacles = 0;
    public GameObject[] outerMaps;
    public GameObject[] innerObstacles;
    public GameObject[] portals;
    // Start is called before the first frame update
    void Start()
    {
        selectedOuterMap = StaticGameInfo.getSelectedOuterMap();
        selectedInnerObstacles = StaticGameInfo.getSelectedInnerObstacles();
        if(StaticGameInfo.getSpawnPortals()){
            toggleAllPortals(true);
        }else{
            toggleAllPortals(false);
        }
        disableAllMapsExceptSelected();
    }

    void disableAllMapsExceptSelected(){
        //outer map (the walls of the game field)
        for (int i = 0; i < outerMaps.Length; i++)
        {
            if(i != selectedOuterMap){
                outerMaps[i].SetActive(false);
            }
        }
        outerMaps[selectedOuterMap].SetActive(true);

        //inner obstacles (like boxes or walls or stuff like that)
        for (int i = 0; i < innerObstacles.Length; i++)
        {
            if(i != selectedInnerObstacles){
                innerObstacles[i].SetActive(false);
            }
        }
        innerObstacles[selectedInnerObstacles].SetActive(true);
    }

    void toggleAllPortals(bool val){
        for(int i = 0; i < portals.Length; i++){
            portals[i].SetActive(val);
        }
    }
}
