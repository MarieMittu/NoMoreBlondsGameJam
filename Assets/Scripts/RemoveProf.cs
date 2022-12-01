using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class RemoveProf : MonoBehaviour
{
    //    public SpawnProf spawnProf;
    //    public ProfManager profManager;
    //    public
    float yPos;
    float xPos;
    float zPos;
    public Renderer[] componentProf;
    //public SpawnProf spawnProf;
    void Start()
    {
        StartCoroutine(SelfDestruct());

    }

    IEnumerator SelfDestruct()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(17f, 25f));
            //        Destroy(gameObject);
            //Calcolo nuova posizione per nemico
            yPos = gameObject.transform.position.y;
            xPos = UnityEngine.Random.Range(-7, 16);
            zPos = UnityEngine.Random.Range(5, -24);
            //Recupero componente dell'oggetto Professor
	    componentProf = gameObject.GetComponentsInChildren<Renderer>();
            //Nascondo il game Object Professor al player
	    HideGameObject(gameObject, componentProf);
	    yield return new WaitForSeconds(UnityEngine.Random.Range(7f, 12f));
            // Dopo 3 secondi sposto il nemico di posizione e lo faccio riapparire
	    MoveGameObject(gameObject, xPos, yPos, zPos);
	    SpawnGameObject(gameObject, componentProf);

        }
    }

    private void MoveGameObject(GameObject gameObject, float x, float y, float z)
    {
        gameObject.transform.position = new UnityEngine.Vector3(x, y, z);
    }

    private void HideGameObject(GameObject gameObject, Renderer[] componentProf)
    {
	//Rendo invisibile il renderer principale
        gameObject.GetComponent<Renderer>().enabled = false;
	//Mi cerco nei componenti figli altri renderer attivi da rendere invisibili.
	foreach (var child in componentProf) {
	child.enabled = false;
	}
        ProfManager.Instance.DisableState();
    }
    private void SpawnGameObject(GameObject gameObject, Renderer[] componentProf)
    {
	//Rendo invisibile il renderer principale
        gameObject.GetComponent<Renderer>().enabled = true;
	//Mi cerco nei componenti figli altri renderer attivi da rendere invisibili.
 	foreach (var child in componentProf) {
	child.enabled = true;
	}
        ProfManager.Instance.EnableState();
    }
}
