
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PartyJoinButton : UdonSharpBehaviour
{
    public PVPManager manager;
    public bool isDropperJoin = false;
    public void AddPlayer()
    {
        if (!isDropperJoin)
        {
            manager.AddRunner();
        }
        else
        {
            manager.AddDropper();
        }
        
    }
}
