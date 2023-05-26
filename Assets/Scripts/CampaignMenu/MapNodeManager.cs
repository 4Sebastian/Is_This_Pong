using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MapNodeManager : MonoBehaviour
{
    public TextAsset CampaignData;
    public GameObject[] Levels = new GameObject[5];//organized from 1 to 5

    public TextMeshPro textMesh;
    public UnityEvent OnClick = new UnityEvent();
    public UnityEvent OnClick2 = new UnityEvent();

    private int currentNode = 1;
    

    // Use this for initialization
    void Start()
    {
        updateWorldInformation(WorldTitleManager.getCurrentWorld());
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var ray2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D Hit;
        
        if (Input.GetMouseButtonDown(0))
        {
            Hit = Physics2D.Raycast(ray2D, -Vector2.up);
            if(Hit.collider != null){
                int levelSelected = LevelsContain(Hit.collider.gameObject);
                if(levelSelected <= 5){
                    if(Levels[levelSelected-1].GetComponent<MapNodeInterface>().hasBeenReached){
                        textMesh.text = "" + levelSelected;
                        currentNode = levelSelected;
                        OnClick.Invoke();
                    }
                }
            }else{
                OnClick2.Invoke();
            }
        }
    }

    int LevelsContain(GameObject objectSelected){
        for(int i = 0; i < Levels.Length; i++){
            if(Levels[i] == objectSelected){
                return i+1;
            }
        }
        return 999;
    }

    public void updateWorldInformation(int world){
        if(world <= 5){
            for(int i = 0; i < Levels.Length; i++){
                Levels[i].GetComponent<MapNodeInterface>().getSavedNodeInformation(i+1, world);
            }
        }else{
            //this is endless stuff
        }
        
    }

    public void startLevelGame(){
        //PlayerPrefs.SetInt();//This is the next level for the purpose of tracking the "reached" level not the current level in play
        int NodeNumber = PlayerPrefs.GetInt("Current_Node", 999);
        int outerMap = 1;
        int innerMap = 1;
        bool grav = false;
        bool powers = false;
        bool portals = false;

        string campaign = CampaignData.text;
        string world = "";
        string nodeInfo = "";

        int beginning = campaign.IndexOf("World " + WorldTitleManager.getCurrentWorld());
        int end = campaign.IndexOf("--", beginning);
        
        world = CampaignData.text.Substring(beginning + 8, (end - beginning) - 8);
        
        beginning = world.IndexOf("Node " + NodeNumber);
        end = world.IndexOf(";", beginning);
        
        nodeInfo = world.Substring(beginning + 8, (end - beginning) - 8);

        beginning = nodeInfo.IndexOf("OuterMap");
        end = nodeInfo.IndexOf(",", beginning);

        outerMap = int.Parse(nodeInfo.Substring(beginning + 9, 1));
        
        beginning = nodeInfo.IndexOf("InnerMap");
        end = nodeInfo.IndexOf(",", beginning);

        innerMap = int.Parse(nodeInfo.Substring(beginning + 9, 1));
        
        if(nodeInfo.Contains("Grav")){
            grav = true;
        }
        if(nodeInfo.Contains("Powers")){
            powers = true;
        }
        if(nodeInfo.Contains("Portals")){
            portals = true;
        }

        StaticGameInfo.setGameInfo(outerMap, innerMap, grav, powers, portals);
        PlayerPrefs.SetInt("isCampaignGame", 1);
        PlayerPrefs.SetInt("isEndlessCampaignGame", 0);

        print("Outer: " + outerMap + " Inner: " + innerMap + " Node: " + NodeNumber + " World: " + WorldTitleManager.getCurrentWorld());


    }

    public void saveCurrentNode(){
        PlayerPrefs.SetInt("Current_Node", currentNode);
    }

    public static void saveLevelGame(bool wonLevel){
        if(wonLevel){
            int number = PlayerPrefs.GetInt("Current_Node", 999) + 1;//plus one for next one to say you have reached the next level 
            int world = WorldTitleManager.getCurrentWorld();
            if(number == 6){
                number = 1;
                world++;
            }
            if(world > 5){
                //completed campaign... trigger some completion thing
            }
            if(number > 6){
                print("problem bro... what in the world did you do?");
            }else{
                PlayerPrefs.SetInt("Node_" + number + "World_" + world, 0); 
            }
        }
    }
}
