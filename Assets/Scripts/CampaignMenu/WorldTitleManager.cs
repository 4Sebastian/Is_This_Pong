using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldTitleManager : MonoBehaviour
{
    private static int CurrentWorld;
    public TextMeshProUGUI TextMesh;

    public MapNodeManager Manager;

    public void Start(){
        CurrentWorld = 1;
        TextMesh.text = "World " + CurrentWorld;
    }

    public void IncrementWorld(){
        if(CurrentWorld < 5){
            CurrentWorld++;
            TextMesh.text = "World " + CurrentWorld;        
        }
        Manager.updateWorldInformation(CurrentWorld);
    }

    public void DecrementWorld(){
        if(CurrentWorld > 1){
            CurrentWorld--;
            TextMesh.text = "World " + CurrentWorld;
        }
        Manager.updateWorldInformation(CurrentWorld);
    }

    public static int getCurrentWorld(){
        return CurrentWorld;
    }

}
