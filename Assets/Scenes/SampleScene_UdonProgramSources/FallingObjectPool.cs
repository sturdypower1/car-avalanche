
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FallingObjectPool : UdonSharpBehaviour
{
    public GameObject[] cars;
    [UdonSynced] bool[] areActive = new bool[0];



    public override void OnDeserialization()
    {
        for(int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(areActive[i]);
        }
    }

    private void Update()
    {
        // should only update if it is the owner
        if(Networking.IsOwner(Networking.LocalPlayer, gameObject))
        {
            int i = 0;
            foreach (GameObject car in cars)
            {
                if (car.transform.position.y < -10)
                {
                    car.transform.position = Vector3.zero;
                    areActive[i] = false;
                    car.SetActive(false);
                }
                i++;
            }
        }
        
    }

    private void Start()
    {
        areActive = new bool[cars.Length];

    }

    public GameObject GetFallingObject()
    {
        int i = 0;
        foreach(GameObject car in cars)
        {
            if(!areActive[i])
            {
                car.SetActive(true);
                areActive[i] = true;
                return car;
            }
            i++;
        }
        return null;
    }
}