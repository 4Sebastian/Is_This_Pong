using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{

    public void Start(){

    }

    public void Update(){

    }
    
    public static void goToInitialMenu(){
        SceneManager.LoadScene(0);
    }

    public static void goToCampaign(){
        SceneManager.LoadScene(2);
    }

    public static void goToEndless(){
        //doesn't exist yet
    }

    public static void goToGame(){
        SceneManager.LoadScene(1);
    }

    public static void goToShop(){
        //doesn't exist yet
    }
}
