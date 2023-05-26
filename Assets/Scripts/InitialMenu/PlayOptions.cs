using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayOptions : MonoBehaviour
{

    private int selectedVal;

    public void setSelectedGamemode(int val)
    {
        selectedVal = val;
    }

    public void playSelectedGamemode()
    {
        SceneManager.LoadScene(1);
    }

}
