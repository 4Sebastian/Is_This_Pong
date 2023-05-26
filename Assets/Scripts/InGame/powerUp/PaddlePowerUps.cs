using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlePowerUps : MonoBehaviour
{
    public PhysicsMaterial2D paddlePhysics;

    private static float paddleWidthOffset = 2f;

    private Vector3 defaultSize;

    private double paddleWidthPowerTimer = 0;
    private double paddleStrengthPowerTimer = 0;

    private bool powerReset = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultSize = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.countDone){
            paddleStrengthPowerTimer++;
            paddleWidthPowerTimer++;
            deactivateNecessaryPowers();
            powerReset = false;
        }else if(!powerReset){
            powerReset = true;
            resetPowerUps();
        }
    }

    public void ActivateStrengthPower(){
        paddleStrengthPowerTimer = 0;
        paddlePhysics.bounciness = 1.5f;
        print("Strength Activated");
    }

    public void ActivateWidthUpPower(){
        paddleWidthPowerTimer = 0;
        this.transform.localScale = new Vector3((defaultSize.x + paddleWidthOffset), defaultSize.y, defaultSize.z);
        print("Width Up Activated");

    }

    public void ActivateWidthDownPower(){
        paddleWidthPowerTimer = 0;
        this.transform.localScale = new Vector3((defaultSize.x - paddleWidthOffset), defaultSize.y, defaultSize.z);
        print("Width Down Activated");

    }

    public void deactivateNecessaryPowers(){
        if(paddleStrengthPowerTimer > powerUpPoint.maxPowerUpTime){
            paddleStrengthPowerTimer = 0;
            paddlePhysics.bounciness = 1.0f;
        }
        if(paddleWidthPowerTimer > powerUpPoint.maxPowerUpTime){
            paddleWidthPowerTimer = 0;
            this.transform.localScale = defaultSize;
        }
    }

    public void resetPowerUps(){
        paddleStrengthPowerTimer = 0;
        paddleWidthPowerTimer = 0;
        paddlePhysics.bounciness = 1.0f;
        this.transform.localScale = defaultSize;
    }
}
