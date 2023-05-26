using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGameInfo : MonoBehaviour
{
    private static bool isPlayerBot = true;
    private static int selectedOuterMap = 5;
    private static int selectedInnerObstacles = 2;
    private static bool spawnPortals = true;
    private static bool spawnGravPoints = true;
    private static bool spawnPowerPoints = true;

    public void setPlayerBot(bool val)
    {
        isPlayerBot = val;
    }

    public static bool getPlayerBot()
    {
        return isPlayerBot;
    }

    public void setSelectedOuterMap(int i){
        selectedOuterMap = i;
    }

    public static int getSelectedOuterMap(){
        return selectedOuterMap;
    } 

    public void setSelectedInnerObstacles(int i){
        selectedInnerObstacles = i;
    }

    public static int getSelectedInnerObstacles(){
        return selectedInnerObstacles;
    } 

    public void setSpawnGravPoints(bool val){
        spawnGravPoints = val;
    }

    public static bool getSpawnGravPoints(){
        return spawnGravPoints;
    }

    public void setSpawnPortals(bool val){
        spawnPortals = val;
    }

    public static bool getSpawnPortals(){
        return spawnPortals;
    }

    public void setSpawnPowerPoints(bool val){
        spawnPowerPoints = val;
    }

    public static bool getSpawnPowerPoints(){
        return spawnPowerPoints;
    }

    public static void setGameInfo(int outer, int inner, bool grav, bool powers, bool portals){
        selectedOuterMap = outer;
        selectedInnerObstacles = inner;
        spawnGravPoints = grav;
        spawnPowerPoints = powers;
        spawnPortals = portals;
    }

}
