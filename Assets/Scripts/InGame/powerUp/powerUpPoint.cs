using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpPoint : MonoBehaviour
{
    public static Color[] types = { Color.red, Color.cyan, Color.yellow, Color.blue, Color.magenta, Color.green, new Vector4(1, 1, 0, 1) };

    public static bool[] inPlaytypes = { false, false, false, false, false, false, false };
    // Strength = Color.red
    // SpeedUp = Color.cyan
    // PaddleUp = Color.yellow
    // SpeedDown = Color.blue 
    // SizeUp = Color.magenta
    // SizeDown = Color.green
    // PaddleDown = Color.orange

    public static int maxPowerUpTime = 500;

    private Color type = Color.green;
    public ParticleSystem aura;
    void Start()
    {

    }

    void FixedUpdate()
    {
        var col = aura.colorOverLifetime;

        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] { new GradientColorKey(type, 0.0f) }
        , new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.4f) });

        col.color = grad;
    }

    public void setAuraType(Color selectedAura)
    {
        type = selectedAura;
    }

    public Color getAuraType(){
        return type;
    }

    public static void resetInPlayTypes()
    {
        for (int i = 0; i < inPlaytypes.Length; i++)
        {
            inPlaytypes[i] = false;
        }
    }

    public static bool hasEveryPowerPlayed()
    {
        foreach (bool type in inPlaytypes)
        {
            if (type == false)
            {
                return false;
            }
        }
        return true;
    }

    public static int getSelectedTypeIndex(Color type){
        for(int i = 0; i < types.Length; i++){
            if(types[i] == type){
                return i;
            }
        }
        return -999;
    }
}
