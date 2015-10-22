/**
Script Author : Peter Stirling (Sourced from net) 
Description   : Dynamic Runner - Floating Origin
**/

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class FloatingOrigin : MonoBehaviour
{
    public float threshold = 100.0f;
    public float physicsThreshold = 1000.0f; // Set to zero to disable
    public float defaultSleepVelocity = 0.14f;
    public float defaultAngularVelocity = 0.14f;

    GameObject tempObj;
    Vector3 cameraPosition;
    ParticleEmitter pe;
    Rigidbody r;
    Transform t;
    Object[] objects;
    Particle[] emitterParticles;

    void LateUpdate()
    {
        cameraPosition = gameObject.transform.position;
        cameraPosition.z = 0f;
        //cameraPosition.x = 0f;
        if (cameraPosition.magnitude > threshold)
        {
            objects = FindObjectsOfType(typeof(Transform));
            for(int i=0;i<objects.Length;i++)
            {
                t = objects[i] as Transform;
                if(t.CompareTag("Stationary"))
                {
                    continue;
                }
                if(t.parent == null || t.parent.name=="Pooler")
                {
                    t.position -= cameraPosition;
                }
            }

            objects = FindObjectsOfType(typeof(ParticleEmitter));
            for (int i = 0; i < objects.Length; i++)
            {
                pe = objects[i] as ParticleEmitter;
                emitterParticles = pe.particles;
                for (int j = 0; j < emitterParticles.Length; j++)
                {
                    emitterParticles[j].position -= cameraPosition;
                }
                pe.particles = emitterParticles;
            }

            if (physicsThreshold >= 0f)
            {
                objects = FindObjectsOfType(typeof(Rigidbody));
                for (int i = 0; i < objects.Length; i++)
                {
                    r = objects[i] as Rigidbody;
                    if (r.gameObject.transform.position.magnitude > physicsThreshold)
                    {
                        r.sleepAngularVelocity = float.MaxValue;
                        r.sleepVelocity = float.MaxValue;
                    }
                    else
                    {
                        r.sleepAngularVelocity = defaultSleepVelocity;
                        r.sleepVelocity = defaultAngularVelocity;
                    }
                }
            }
        }
    }
}