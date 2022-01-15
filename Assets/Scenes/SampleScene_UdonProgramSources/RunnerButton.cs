
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class RunnerButton : UdonSharpBehaviour
{
    public Transform teleportLocation;
    public override void Interact()
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        player.TeleportTo(teleportLocation.position, Quaternion.identity);
    }
}
