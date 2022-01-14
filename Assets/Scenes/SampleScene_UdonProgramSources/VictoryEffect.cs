
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class VictoryEffect : UdonSharpBehaviour
{
    private ParticleSystem particleSystem;
    private bool isTriggered = false;
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (!isTriggered)
        {
            particleSystem.Play();
        }
        isTriggered = true;
        
    }
}
