
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SphereUSharp : UdonSharpBehaviour
{
    public Transform respawnPosition;
    Rigidbody rigid;
    public AudioSource carImpactSound;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!carImpactSound.isPlaying)
        {
            carImpactSound.Play();
        }
        
    }
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        
        player.TeleportTo(respawnPosition.position, Quaternion.identity);
    }

    public override void OnPlayerCollisionEnter(VRCPlayerApi player)
    {
        player.TeleportTo(respawnPosition.position, Quaternion.identity);
    }

}
