using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkParticles : MonoBehaviour
{
    public ParticleSystem particleSystem;
    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int events = particleSystem.GetCollisionEvents(other, colEvents);

        for(int i = 0; i < events; i++)
        {

        }
    }
}
