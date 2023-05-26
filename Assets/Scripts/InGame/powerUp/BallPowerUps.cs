using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPowerUps : MonoBehaviour
{   
    public GameObject paddle1;
    public GameObject paddle2;

    private bool lastHitPlayer1 = false;

    private int ballSpeedPowerTimer = 0;
    private int ballSizePowerTime = 0;

    private float defaultBallSpeed;

    private float offsetBallSpeed = 3f;

    public static float offsetBallSize = 0.3f;

    private bool powerReset = false; 

    public void Start(){
        defaultBallSpeed = this.gameObject.GetComponent<Ball>().speed;
    }

    public void FixedUpdate(){
        if(GameManager.countDone){
            ballSizePowerTime++;
            ballSpeedPowerTimer++;
            deactivateNecessaryBallPowers();
            powerReset = false;
        }else if(!powerReset){
            powerReset = true;
            resetPowerUps();
        }
        
    }

    private void deactivateNecessaryBallPowers(){
        if(ballSizePowerTime > powerUpPoint.maxPowerUpTime){
            ballSizePowerTime = 0;
            deactivateBallSizePower();
        }
        if(ballSpeedPowerTimer > powerUpPoint.maxPowerUpTime){
            ballSpeedPowerTimer = 0;
            deactivateBallSpeedPower();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Power Up"))
        {

            for (int i = 0; i < GameManagerPowerUps.powerPoints.Length; i++)
            {
                if (GameManagerPowerUps.powerPoints[i] == collision.gameObject)
                {
                    GameManagerPowerUps.powerPoints[i] = null;
                    GameManagerPowerUps.removeNullPowerPoints();
                    GameManagerPowerUps.numPoints--;
                    activatePowerUp(collision.gameObject.GetComponent<powerUpPoint>().getAuraType());
                }
            }
            Destroy(collision.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("paddle1")){
            lastHitPlayer1 = true;
        }else if(collision.gameObject.CompareTag("paddle2")){
            lastHitPlayer1 = false;
        }
    }

    private void activatePowerUp(Color type){
        int i = powerUpPoint.getSelectedTypeIndex(type);
        
        switch (i)
        {
            case 0:// red (Paddle Strength)
            if(lastHitPlayer1){
                paddle1.GetComponent<PaddlePowerUps>().ActivateStrengthPower();
            }else{
                paddle2.GetComponent<PaddlePowerUps>().ActivateStrengthPower();
            }
            break; 

            case 1:// cyan (Ball Speed Up)
            activateBallSpeedUpPower();
            break;

            case 2:// yellow (Paddle Width Up)
            if(lastHitPlayer1){
                paddle1.GetComponent<PaddlePowerUps>().ActivateWidthUpPower();
            }else{
                paddle2.GetComponent<PaddlePowerUps>().ActivateWidthUpPower();
            }
            break;

            case 3:// blue (Ball Speed Down)
            activateBallSpeedDownPower();
            break;

            case 4:// magenta (Ball Size Up)
            activateBallSizeUpPower();
            break;

            case 5:// green (Ball Size Down)
            activateBallSizeDownPower();
            break;

            case 6:// orange (Paddle Width Down)
            if(lastHitPlayer1){
                paddle1.GetComponent<PaddlePowerUps>().ActivateWidthDownPower();
            }else{
                paddle2.GetComponent<PaddlePowerUps>().ActivateWidthDownPower();
            }
            break;

            default:
            //Do nothing
            print("Yo... You were never supposed to get this message");
            break;
        }
    }

    private void activateBallSizeUpPower(){
        ballSizePowerTime = 0;
        this.transform.localScale = new Vector3(1 + offsetBallSize, 1 + offsetBallSize, 1);
        print("Ball Size Up Activated");


    }

    private void activateBallSizeDownPower(){
        ballSizePowerTime = 0;
        this.transform.localScale = new Vector3(1 - offsetBallSize, 1 - offsetBallSize, 1);
        print("Ball Size Down Activated");
    }

    private void activateBallSpeedUpPower(){
        ballSpeedPowerTimer = 0;
        this.GetComponent<Ball>().speed = defaultBallSpeed + offsetBallSpeed;
        print("Ball Speed Up Activated");
    }

    private void activateBallSpeedDownPower(){
        ballSpeedPowerTimer = 0;
        this.GetComponent<Ball>().speed = defaultBallSpeed - offsetBallSpeed;
        print("Ball Speed Down Activated");

    }

    private void deactivateBallSizePower(){
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    private void deactivateBallSpeedPower(){
        this.GetComponent<Ball>().speed = defaultBallSpeed;
    }

    public void resetPowerUps(){
        ballSizePowerTime = 0;
        ballSpeedPowerTimer = 0;
        deactivateBallSizePower();
        deactivateBallSpeedPower();
    }

}
