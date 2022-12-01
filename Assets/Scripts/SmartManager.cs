using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartManager : MonoBehaviour
{
    public GameObject player;
    public GameObject bionda;
    public GenerateSmart genSmart;
    public bool isHit { get; set; }

    public float rayLength;
    public bool isSteering;
    //public GameObject[] walls;
    GameObject closestObject;
    //float wallDistance = Mathf.Infinity;

    private NavMeshAgent navAgent;
    private GameObject[] allSmart;
    public float PlayerDistanceRun = 100.0f;

    public float WallsDistanceRun = 50.0f;
   
    public Animator smartAnimator;
    public GameObject pinkShot;
    public ParticleSystem particleSystem;

    public AudioClip hitSound;
    private AudioSource audioSource;

    [Header("Sensors")]
    public float sensorLength = 10f;
    public Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 0.5f);
    public float frontSideSensorPosition = 0.2f;
    public float frontSensorAngle = 30;



    private bool avoiding = false;
    private static SmartManager _instance;

    private SmartManager() { }

    public static SmartManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SmartManager();

            return _instance;
        }
    }

    //public void GetNearestWall()
    //{
    //    if (wallDistance <= 50.0f)
    //    {
    //        //nearWall = true;
    //        //print("Near wall!");
    //        Vector3 dirToWall = transform.position - closestObject.transform.position;
    //        Vector3 newPos = transform.position + dirToWall;
    //        navAgent.SetDestination(newPos);
    //    }
    //}

    void FindNearestWall(GameObject currentSmart)
    {
        smartAnimator = currentSmart.GetComponent<Animator>();
        Vector3 smartPosition = currentSmart.transform.position;

        GameObject[] objectArray;
        objectArray = GameObject.FindGameObjectsWithTag("Wall");
        float wallDistance = 50.0f;

        foreach (GameObject currentObject in objectArray)
        {
            Vector3 distanceCheck = currentObject.transform.position - currentSmart.transform.position;
            float currentDistance = distanceCheck.sqrMagnitude;
            float distance = Vector3.Distance(currentSmart.transform.position, currentObject.transform.position);

            if (distance < wallDistance)
            {
                //closestObject = currentObject;
                //wallDistance = currentDistance;
                Vector3 dirToWall = currentSmart.transform.position - currentObject.transform.position;
                Vector3 newPos = currentSmart.transform.position + dirToWall;
                navAgent.SetDestination(newPos);

            }
        }

        //return closestObject;
    }


    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //walls = GameObject.FindGameObjectsWithTag("Wall");
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        allSmart = GameObject.FindGameObjectsWithTag("Smart");
        _instance = this;
    }

    //void ObstacleAvoidance(Vector3 dir, Vector3 steering, bool checkObstacles)
    //{
    //    List<Vector3> steeringRays = new List<Vector3>();
    //    var _holdTheJump = dir.y;

    //    bool left = false;
    //    bool right = false;
    //    bool front = false;
    //    Vector3 adjDirection = dir;

    //    steeringRays.Add(transform.TransformDirection(-steering.x, steering.y, steering.z)); //ray pointed slightly left 
    //    steeringRays.Add(transform.TransformDirection(steering.x, steering.y, steering.z)); //ray pointed slightly right 
    //    steeringRays.Add(transform.forward); //ray 1 is pointed straight ahead

    //    RaycastHit hit;

    //    if (checkObstacles)
    //    {
    //        //Debug.DrawRay(transform.localPosition, steeringRays[0].normalized * rayLength, Color.cyan);
    //        //Debug.DrawRay(transform.localPosition, steeringRays[1].normalized * rayLength, Color.cyan);
    //        //Debug.DrawRay(transform.localPosition, steeringRays[2].normalized * rayLength, Color.cyan);


    //        if (Physics.Raycast(transform.position, steeringRays[0], out hit, rayLength))
    //        {
    //            if (hit.collider.gameObject.layer != 13 && (!front && !left))
    //            {
    //                isSteering = true;
    //                front = false; right = false; left = true;
    //                Debug.DrawLine(transform.position, hit.point, Color.red);
    //                transform.forward = new Vector3(dir.x, 0, dir.z) + (hit.normal).normalized * Time.smoothDeltaTime;
    //                Debug.Log("Steer Left");
    //            }
    //        }
    //        else
    //        if (Physics.Raycast(transform.position, steeringRays[1], out hit, rayLength))
    //        {
    //            if (hit.collider.gameObject.layer != 13 && (!front && !left)) //Character layer
    //            {
    //                Debug.DrawLine(transform.position, hit.point, Color.red);
    //                front = false; right = true; left = false;
    //                isSteering = true;
    //                transform.forward = new Vector3(dir.x, 0, dir.z) + (hit.normal).normalized * Time.smoothDeltaTime;
    //                Debug.Log("Steer Right");
    //            }
    //        }
    //        else
    //        {
    //            isSteering = false;
    //            left = false; right = false; front = false;
    //        }
    //    }
    //    //if (isSteering)
    //    //trans.forward = new Vector3(adjDirection.x, 0, adjDirection.z)  * Time.smoothDeltaTime;
    //    //Quaternion rot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //    //trans.rotation = Quaternion.Lerp(trans.rotation, rot, 15f * Time.smoothDeltaTime);
    //}

    // Update is called once per frame
    void Update()
    {
      
        foreach (GameObject currentSmart in allSmart)
        {

            if (currentSmart.tag == "Smart")
            {
                FleeFromPlayer(currentSmart);
                //AvoidWalls(currentSmart);
                //FindNearestWall(currentSmart);
                
            }
            Sensors(currentSmart);

        }

        if(GenerateSmart.Instance.minusSmart == true && GenerateSmart.Instance.smartCount == 0)
        {
            Invoke("LoadWonScene", 5f);
        }

    }

    private void Sensors(GameObject currentSmart)
    {
        
            RaycastHit hit;
            Vector3 sensorStartPos = transform.position;
            sensorStartPos += transform.forward * frontSensorPosition.z;
            sensorStartPos += transform.up * frontSensorPosition.y;
            float avoidMultiplier = 0;
            avoiding = false;

            // front center sensor
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                    if(hit.collider.CompareTag("Wall"))
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        avoiding = true;
                    }
                   
            }



        // front right sensor
        sensorStartPos += transform.right * frontSideSensorPosition;
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoiding = true;
                    avoidMultiplier -= 1.0f;
                }
            }


        // front right angle sensor

        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }

        // front left sensor
        sensorStartPos -= transform.right * frontSideSensorPosition * 2;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
                if (hit.collider.CompareTag("Wall"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoiding = true;
                avoidMultiplier += 1.0f;
            }
        }
            

            // front left angle sensor

            if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoiding = true;
                avoidMultiplier += 0.5f;
            }
            }

            if(avoiding)
            {
                Vector3 newPos = -transform.position;
                navAgent.SetDestination(newPos);
            }
    }

    void LoadWonScene()
    {
        FindObjectOfType<GameManager>().WonGame();
    }

    void FleeFromPlayer(GameObject currentSmart)
    {
        smartAnimator = currentSmart.GetComponent<Animator>();
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("Distance: " + distance);

        if (distance < PlayerDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            navAgent.SetDestination(newPos);

           
        }
    }

    //void AvoidWalls(GameObject currentSmart)
    //{
    //    smartAnimator = currentSmart.GetComponent<Animator>();
    //    float distance = Vector3.Distance(transform.position, wall.transform.position);
    //    //Debug.Log("Distance: " + distance);

    //    if (distance < WallsDistanceRun)
    //    {
    //        Vector3 dirToWall = transform.position - wall.transform.position;
    //        Vector3 newPos = transform.position + dirToWall;
    //        navAgent.SetDestination(newPos);


    //    }
    //}

    //void CheckWallDistance(GameObject currentSmart)
    //{

    //    smartAnimator = currentSmart.GetComponent<Animator>();
    //    foreach (GameObject wall in walls)
    //    {
    //        if (Vector3.Distance(transform.position, wall.transform.position) < WallsDistanceRun)
    //        {
    //            Vector3 dirToWall = transform.position - wall.transform.position;
    //            Vector3 newPos = transform.position + dirToWall;
    //            navAgent.SetDestination(newPos);
    //        }
    //    }
    //}


    void OnCollisionEnter(Collision collision)
    {
        GameObject currentSmart = collision.gameObject;
        Debug.Log("Current Game Object is : " + currentSmart);
        Debug.Log("Current Collider is : " + collision.collider);
        if (collision.gameObject.tag == "Bullet" && this.gameObject.tag == "Smart")
        //        if (collision.gameObject.tag == "Bullet")
        {
            //            staminaManager = Cam.GetComponent<StaminaManager();
            //	    staminaManager = GameObject.GetComponent<StaminaManager>();
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
            //            isHit = true;
            StaminaManager.Instance.Increase();
            ScoreManager.Instance.AddScore();
            GenerateSmart.Instance.DecreaseSmart();
            Debug.Log("Increase");
            GetComponent<AudioSource>().Play();

            Destroy(collision.gameObject);
            Instantiate(pinkShot, transform.position, Quaternion.identity);
            particleSystem.Play();
            Destroy(this.gameObject, 0.5f);
            GameObject newBionda = Instantiate(bionda) as GameObject;
            newBionda.transform.position = this.gameObject.transform.position;
            BiondeManager biondeManager = newBionda.GetComponent<BiondeManager>();
        } 
    }




    public bool checkObjectSmart()
    {
        foreach (GameObject currentSmart in allSmart)
        {
            if (currentSmart.tag == "Smart")
            {
                return true;
            }
        }
        return false;
    }

    public GameObject[] getListOfSmartGameObjects()
    {
        if (allSmart != null)
        {
            return allSmart;
        }
        else
        {
            GameObject[] smartGameObjects;
            return smartGameObjects = GameObject.FindGameObjectsWithTag("Smart");
        }
    }

    //public GameObject[] getListOfWalls()
    //{
    //    if (walls != null)
    //    {
    //        return walls;
    //    }
    //    else
    //    {
    //        GameObject[] allTheWalls;
    //        return allTheWalls = GameObject.FindGameObjectsWithTag("Wall");
    //    }
    //}
}
