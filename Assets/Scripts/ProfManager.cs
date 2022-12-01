using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProfManager : MonoBehaviour
{
    public GameObject bionda;
    //public Rigidbody rigidbody;
    public bool isFollowing;
    private bool isActive { get; set; }

    private static ProfManager _instance;

    public GameObject pinkShot;
    public ParticleSystem particleSystem;

    private ProfManager() { }

    public static ProfManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = new ProfManager();

            return _instance;
        }
    }
   
    void Awake()
    {
        _instance = this;
        ProfManager.Instance.EnableState();
    }

    // Start is called before the first frame update
    void Start()
    {
	Debug.Log("Started ProfManager");
        bionda = GameObject.FindGameObjectWithTag("Blond");
        //rigidbody = GetComponent<Rigidbody>();
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {

	    Debug.Log("isFollowing value: " + isFollowing);
	
 	      
            if (BiondeManager.Instance.checkObjectBlond() && isFollowing && ProfManager.Instance.isActive)
            {
                Debug.Log("Finding closest Bionda");
                //GetComponent<NavMeshAgent>().destination = bionda.transform.position;
                FindClosestBionda();
            }
        
      
    }

    void FindClosestBionda()
    {
        
            float distToClosestBionda = Mathf.Infinity;
            BiondeManager closestBionda = null;
            BiondeManager[] allBionde = GameObject.FindObjectsOfType<BiondeManager>();

            foreach (BiondeManager currentBionda in allBionde)
            {
		if (currentBionda.tag == "Blond")
		{
                	float distToBionda = (currentBionda.transform.position - this.transform.position).sqrMagnitude;
                if (distToBionda < distToClosestBionda)
                {
                    distToClosestBionda = distToBionda;
                    closestBionda = currentBionda;
                    GetComponent<NavMeshAgent>().destination = closestBionda.transform.position;

                    if(distToBionda <= 2)
                    {
                        //hit anim, bool?
                    }
                }
		}
            }
              
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(pinkShot, transform.position, Quaternion.identity);
            particleSystem.Play();

            //idle or drunk anim
            isFollowing = false;
            Invoke("FollowsAgain", 5f);

        }
    
    }

    void FollowsAgain()
    {
        isFollowing = true;
        //run anim
    }

    public void DisableState()
    {
        ProfManager.Instance.isActive = false;
    }
    
    public void EnableState()
    {
        ProfManager.Instance.isActive = true;
    }
}
