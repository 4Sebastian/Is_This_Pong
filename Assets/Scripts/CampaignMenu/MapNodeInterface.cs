using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNodeInterface : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sprite;
    public ParticleSystem pathToNextNode;
    public ParticleSystem CurrentNode;

    public bool hasPathOn = false, isCurrentNode = false, hasBeenReached = false;

    void Start()
    {
        
    }

    void OnEnable()
    {
        initializeAll();
    }

    void initializeAll(){
        initializeCurrentNode();
        initializePath();
        initializeReach();
    }

    public void initializePath(){
        if(hasPathOn){
            //if(!pathToNextNode.isPlaying){
                pathToNextNode.Play();
            //}
        }else{
            pathToNextNode.Stop();
        }
    }

    public void initializeCurrentNode(){
        if(isCurrentNode){
            //if(!CurrentNode.isPlaying){
                CurrentNode.Play();
            //}
        }else{
            CurrentNode.Stop();
        }
    }

    public void initializeReach(){
        if(hasBeenReached){
            sprite.color = Color.white;
        }else{
            sprite.color = Color.gray;
        }
    }

    public void getSavedNodeInformation(int number, int world){
        int Current =  PlayerPrefs.GetInt("Node_" + number + "World_" + world, 0);
        int Next = 0;
        int PrevWorldCompleted = PlayerPrefs.GetInt("Node_" + 1 + "World_"+ world, 0);
        if(number != 5){
            Next = PlayerPrefs.GetInt("Node_" + (number + 1) + "World_" + world, 0);
        }
        if(world == 1 && number == 1){
            Current = 1;
        }

        if(Current == 1){
            hasBeenReached = true;

            if(Next == 1){
                hasPathOn = true;
                isCurrentNode = false;
            }else{
                hasPathOn = false;
                isCurrentNode = true;
            }
        }else{
            hasBeenReached = false;
            hasPathOn = false;
            isCurrentNode = false;
        }

        initializeAll();
    }

    public void setSavedNodeInformation(int number, int world, int value){
        if(number > 5){
            number = 1;
            world++;
        }

        if(world > 5){
            print("something went wrong... why is world greater than 5... bro you messed up hard...");
        }else{
            PlayerPrefs.SetInt("Node_" + number + "World_" + world, value);
            print("Saved World: " + world + " Node: " + number + " as Value: " + value);
        }
    }
}
