using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSmart : MonoBehaviour
{
    public GameObject smart;
    public int xPos;
    public int zPos;
    public int smartCount;
    public bool minusSmart = false;

    private static GenerateSmart _instance;

    private GenerateSmart() { }

    public static GenerateSmart Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GenerateSmart();

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
        StartCoroutine(SmartDrop());
    }

    IEnumerator SmartDrop()
    {
        while (smartCount < 10)
        {
            xPos = Random.Range(-534, 103);
            zPos = Random.Range(-118, 560);
            Instantiate(smart, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
            smartCount += 1;
        }
    }

    public void DecreaseSmart()
    {
        smartCount--;
        minusSmart = true;
    }
}
