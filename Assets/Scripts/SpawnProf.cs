using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProf : MonoBehaviour
{
    public GameObject professor;
    public bool isProfSpawned { get; set; }
    public static SpawnProf _instance;    

    private SpawnProf() { }

    public static SpawnProf Instance
    {
        get
        {
            if(_instance == null)
                _instance = new SpawnProf();

            return _instance;
        }
    }
    
   
    //public int profCount;

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
	isProfSpawned = false;
        StartCoroutine(ProfDrop(1));
    }

    void Update()
    {
       
    }

    public IEnumerator ProfDrop(int profCount)
    {
        //while (true)
        //{
            yield return new WaitForSeconds(Random.Range(5f, 15f));
            for (int i = 0; i < profCount; i++)
            {
                float xPos = Random.Range(-7, 16);
                float zPos = Random.Range(5, -24);
                Instantiate(professor, new Vector3(xPos, 0, zPos), Quaternion.identity);
		isProfSpawned = true;
	        _instance = this;
            }



        //}
    }

}
