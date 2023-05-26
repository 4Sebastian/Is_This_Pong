using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAndBackFunction : MonoBehaviour
{
    public Rigidbody2D pong;
    public ParticleSystem pongTrail;


    public MonoBehaviour[] scripts;

    private Vector3 savedVelocity;
    private bool alreadySavedVelocity = false;

    public void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void continueGame()
    {
        Time.timeScale = 1;
        this.gameObject.GetComponent<GameManager>().ResetCounter();
        disableNecessaryObjects();
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        this.gameObject.GetComponent<GameManager>().RestartGame();
    }

    public void disableNecessaryObjects()
    {
        foreach (MonoBehaviour script in scripts)
        {
            try
            {
            script.enabled = false;
            }
            catch (System.Exception)
            {
                print("Yeah yeah, we got rid of it... deal with it!");
            }
        }
        if(!alreadySavedVelocity){
            savedVelocity = pong.velocity;
            alreadySavedVelocity = true;
        }
        pong.Sleep();
        pongTrail.Pause();
    }

    public void enableNecessaryObjects()
    {
        foreach (MonoBehaviour script in scripts)
        {
            try
            {
                script.enabled = true;
            }
            catch (System.Exception)
            {
                print("Yeah yeah, we got rid of it... deal with it!");
            }
            
        }
        pong.WakeUp();
        if(alreadySavedVelocity){
            pong.velocity = savedVelocity;
            alreadySavedVelocity = false;
        }
        
        pongTrail.Play();
    }
}
