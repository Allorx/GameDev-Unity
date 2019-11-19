using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueChange : MonoBehaviour
{
    public Material affectedMat;
    public float hueIncrease;
    public float scoreMulToIncrease;
    Vector4 hueInc;
    bool changedHue = false;

    void Awake()
    {
        hueInc = new Vector4(hueIncrease, 0, 0, 0);
        affectedMat.SetVector("HueAmount", new Vector4(0, 0, 0, 0));
    }

    void Update()
    {
        if (!changedHue && ScoreCounter.score > 0 && ScoreCounter.score % scoreMulToIncrease == 0)
        {
            changedHue = true;
            affectedMat.SetVector("HueAmount", new Vector4((affectedMat.GetVector("HueAmount") + hueInc).x, 0, 0, 0));
        }
        else if (ScoreCounter.score % scoreMulToIncrease != 0)
        {
            changedHue = false;
        }
    }
}
