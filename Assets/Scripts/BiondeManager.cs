using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BiondeManager : MonoBehaviour
{
    
    public GameObject professor;
    public GenerateBionde genBionde;
    public GameObject smart;
    public bool isHealed { get; set; }
    private bool isProfInstantiated;
    private NavMeshAgent navAgent;
    private GameObject[] allBionde;
  
    public float ProfDistanceRun = 4.0f;
    public Animator biondaAnimator;


    private static BiondeManager _instance;    

    private BiondeManager() { }

    public static BiondeManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = new BiondeManager();

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
        isProfInstantiated = false;
        navAgent = GetComponent<NavMeshAgent>();
	allBionde = GameObject.FindGameObjectsWithTag("Blond");
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isProfInstantiated && SpawnProf.Instance.isProfSpawned) 
        {
            professor = GameObject.FindGameObjectWithTag("Prof");
	    isProfInstantiated = true;
	    Debug.Log("Prof has been instantiated.");
        }
	foreach (GameObject currentBionda in allBionde) 
	{
        
            
            if(currentBionda.tag == "Blond" && SpawnProf.Instance.isProfSpawned && isProfInstantiated)
            {
                FleeFromProf(currentBionda);
            }
	}
       
    }


    void FleeFromProf(GameObject currentBionda)
    {
        biondaAnimator = currentBionda.GetComponent<Animator>();
        float distance = Vector3.Distance(transform.position, professor.transform.position);
        //Debug.Log("Distance: " + distance);

        if (distance < ProfDistanceRun)
        {
            Vector3 dirToProf = transform.position - professor.transform.position;
            Vector3 newPos = transform.position + dirToProf;
            navAgent.SetDestination(newPos);
       
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject currentBlonde = collision.gameObject;
        Debug.Log("Current Game Object is : " + currentBlonde);
	Debug.Log("Current Collider is : " + collision.collider);
       if ((collision.gameObject.tag == "Prof" || collision.gameObject.tag == "Book") && this.gameObject.tag == "Blond") //change for "Book"
        {
	    Debug.Log("prof collided with : " + this.gameObject);
            isHealed = true;
            StaminaManager.Instance.Decrease();
            Debug.Log("Increase");
            Destroy(this.gameObject);
            GameObject newSmart = Instantiate(smart) as GameObject;
            newSmart.transform.position = this.gameObject.transform.position;
            SmartManager smartManager = newSmart.GetComponent<SmartManager>();
        }
    }


    public bool checkObjectBlond()
    {
        foreach (GameObject currentBionda in allBionde) 
	{
           if (currentBionda.tag == "Blond")
           {
              return true;
           }
        }
	return false;
    }

    public GameObject[] getListOfBlondGameObjects()
    {
	if (allBionde != null)
        {
	   return allBionde;
        }
        else 
	{
           GameObject[] blondeGameObjects;
	   return blondeGameObjects = GameObject.FindGameObjectsWithTag("Blond");
        }
    }
}
