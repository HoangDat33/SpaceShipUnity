using System.Collections.Generic;
using UnityEngine;

public class PlanetControllrt : MonoBehaviour
{
    public GameObject[] Planets;

    Queue<GameObject> availablePlanets = new Queue<GameObject>();

    void Start()
    {
        availablePlanets.Enqueue(Planets[0]);
        availablePlanets.Enqueue(Planets[1]);
        availablePlanets.Enqueue(Planets[2]);

        InvokeRepeating("MovePlanetDown", 0.1f, 20f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MovePlanetDown()
    {
        EnqueuePlanets();
        if (availablePlanets.Count == 0)
        {
            return;
        }
        GameObject aPlanet = availablePlanets.Dequeue();
        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    void EnqueuePlanets()
    {
        foreach (GameObject aPlanet in Planets)
        {
            if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                aPlanet.GetComponent<Planet>().ResetPosition();
                availablePlanets.Enqueue(aPlanet);
            }
        }
    }
}