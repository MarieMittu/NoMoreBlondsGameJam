using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    Image timerBar;
    private float maxTime = 100f;
    private float timeLeft;
//    public BiondeManager biondeManager;
    private static StaminaManager _instance;    

    private StaminaManager() { }

    public static StaminaManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = new StaminaManager();

            return _instance;
        }
    }
    

    void Awake()
     {
         _instance = this;
     }

    // Start is called before the first frame update
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
            //Inscrease();
            //Decrease();
            //Debug.Log("Time left " + timeLeft);
        } else
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void Increase()
    {
//       if (biondeManager.isHit)
//        {
            timeLeft += 20f;
            Debug.Log("Time Increase");
            if(timeLeft > maxTime)
            {
                timeLeft = maxTime;
            }
	    timerBar.fillAmount = timeLeft / maxTime;
//        }
    }
    public void Decrease()
    {
//        if (biondeManager.isHealed)
//        {
	    Debug.Log("Time Decrease");
            timeLeft -= 15f;
//        }
	  timerBar.fillAmount = timeLeft / maxTime;
    }
}
