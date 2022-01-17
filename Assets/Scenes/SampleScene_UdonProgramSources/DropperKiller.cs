
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DropperKiller : UdonSharpBehaviour
{
    public GameObject DropperPlatform;
    public PVPManager manager;

    public override void Interact()
    {
        manager.RunnerVictor();
        //DropperPlatform.SetActive(false);
    }
}
