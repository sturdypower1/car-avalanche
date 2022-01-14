
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ObjectSpawner : UdonSharpBehaviour
{
    public Transform respawnTransform;

    public GameObject[] prefabs;
    public float spawnRate;
    float timePassed;

    public Transform[] spawnPositions;

    private void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > spawnRate)
        {
            int spawnIndex = Random.Range(0, spawnPositions.Length);
            int prefabIndex = Random.Range(0, prefabs.Length);
            Vector3 newRotation = new Vector3(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));

            timePassed = 0;
            GameObject spawnedObject = VRCInstantiate(prefabs[prefabIndex]);
            spawnedObject.transform.position = spawnPositions[spawnIndex].position;
            
            spawnedObject.GetComponent<SphereUSharp>().respawnPosition = respawnTransform;
            spawnedObject.transform.Rotate(newRotation);
        }
    }
}
