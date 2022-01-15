
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CarDropperButton : UdonSharpBehaviour
{
    public Transform dropLocation;
    public FallingObjectPool carPool;

    public override void Interact()
    {
        Networking.SetOwner(Networking.LocalPlayer, carPool.gameObject);
        var car = carPool.GetFallingObject();
        if(car != null)
        {
            Networking.SetOwner(Networking.LocalPlayer, car);
            Vector3 newRotation = new Vector3(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
            car.transform.position = dropLocation.position;
            car.transform.rotation = Quaternion.Euler(newRotation);
            car.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            
        }
        
    
        
    }
}

