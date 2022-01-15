
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class DeactivatePVE : UdonSharpBehaviour
{
    public GameObject spawner;

    public override void Interact()
    {
        spawner.SetActive(!spawner.activeSelf);
    }
}
