using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartSpawner : MonoBehaviour
{
    public GameObject smartPrefab;
    private float speed = 5.0f;
    public float width = 10f;
    public float height = 5f;
    private bool movingRight = true;
    private bool movingDown = true;
    private float xmax;
    private float xmin;
    private float zmax;
    private float zmin;



    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            GameObject smart = Instantiate(smartPrefab, child.transform.position, Quaternion.identity) as GameObject;
            smart.transform.parent = child;
        }
        //calc distance object is to camera, only needed for 3d.
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;

        //find x and y coordinates of object
        //var leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera)).x;
        //var rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera)).x;
        //var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera)).y;
        //var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distanceToCamera)).y;
        xmax = 103;
        xmin = -534;
        zmax = 560;
        zmin = -118;



    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));

    }


    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.position += new Vector3(Random.Range(1, 5), 0, 0) * speed * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(Random.Range(-1, -5), 0, 0) * speed * Time.deltaTime;
        }
        if (movingDown)
        {
            transform.position += new Vector3(0, 0, Random.Range(-1, -5)) * speed * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, 0, Random.Range(1, 5)) * speed * Time.deltaTime;
        }

        // Check if the formation is going outside the playspace...
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        float topEdgeOfFormation = transform.position.z + (0.5f * height);
        float bottomEdgeOfFormation = transform.position.z - (0.5f * height);
        if (leftEdgeOfFormation < xmin || rightEdgeOfFormation > xmax)
        {
            movingRight = !movingRight;
        }
        if (bottomEdgeOfFormation < zmin || topEdgeOfFormation > zmax)
        {
            movingDown = !movingDown;
        }
    }

}