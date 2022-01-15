
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DropperKiller : UdonSharpBehaviour
{
    public GameObject DropperPlatform;

    public override void Interact()
    {
        DropperPlatform.SetActive(false);
    }
}
