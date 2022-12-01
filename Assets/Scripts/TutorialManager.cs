using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    public float waitTime = 3f;
    public float waitTime2 = 7f;



    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if(popUpIndex == 0)
        {
            if(waitTime <= 0)
            {
                popUpIndex++;
            } else
            {
                waitTime -= Time.deltaTime;
            }
        } else if (popUpIndex == 1)
        {
            if(waitTime2 <= 0)
            {
                Destroy(gameObject);
            } else
            {
                waitTime2 -= Time.deltaTime;
            }
        }
    }
}
