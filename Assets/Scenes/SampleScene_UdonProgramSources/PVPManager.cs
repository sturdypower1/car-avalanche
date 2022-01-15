
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PVPManager : UdonSharpBehaviour
{
    private bool isRunner = false;
    private bool isDropper = false;
    private int runnerIndex;
    [UdonSynced] private int runnerCount = 0;
    [UdonSynced] private int dropperCount = 0;
    [UdonSynced] private bool[] runnersAlive = new bool[1];

    public Transform runnerTeleport;
    public Transform dropperTeleport;
    public Transform spawnTeleport;

    public PlayerLabel[] runnerLabels;
    public PlayerLabel[] dropperLabels;
    public GameObject playerSelect;

    private bool isSetup = false;
    [UdonSynced] private bool isStarted = false;

    public void DropperVictor()
    {
        ResetMap();
    }
    public void RunnerVictory()
    {

    }

    public void ResetMap()
    {
        var player = Networking.LocalPlayer;
        if(isRunner || isDropper)
        {
            player.TeleportTo(spawnTeleport.position, Quaternion.identity);
        }
    }

    public override void OnPlayerRespawn(VRCPlayerApi player)
    {
        if (isRunner)
        {
            runnersAlive[runnerIndex] = false;
        }
        isRunner = false;
        isDropper = false;
    }

    private void Update()
    {
        if (!isSetup && isStarted && (isRunner || isDropper))
        {
            var player = Networking.LocalPlayer;

            if (isRunner)
            {
                player.TeleportTo(runnerTeleport.position, Quaternion.identity);
            }
            if (isDropper)
            {
                player.TeleportTo(dropperTeleport.position, Quaternion.identity);
            }
            playerSelect.SetActive(false);
            isSetup = true;
        }

        // should only update if it is the owner
        if (Networking.IsOwner(Networking.LocalPlayer, gameObject))
        {
            bool isAllDead = true;
            foreach(bool isAlive in runnersAlive)
            {
                if (isAlive)
                {
                    isAllDead = false;
                    break;
                }
            }
            if (isAllDead)
            {
                DropperVictor();
            }
        }
    }
    public void StartGame()
    {
        if(runnerCount > 0 && dropperCount > 0)
        {
            var player = Networking.LocalPlayer;
            Networking.SetOwner(player, gameObject);
            isStarted = true;
            isSetup = true;

            playerSelect.gameObject.SetActive(false);

            runnersAlive = new bool[runnerCount];
            for(int i = 0; i < runnerCount; i++)
            {
                runnersAlive[i] = true;
            }

            if (isRunner)
            {
                player.TeleportTo(runnerTeleport.position, Quaternion.identity);
            }
            if (isDropper)
            {
                player.TeleportTo(dropperTeleport.position, Quaternion.identity);
            }
        }
    }
    public void AddRunner()
    {
        var player = Networking.LocalPlayer;
        if (!isRunner && !isDropper && runnerCount <= 8)
        {
            Networking.SetOwner(player, gameObject);
            runnerLabels[runnerCount].SetLabelText(player.displayName);
            isRunner = true;
            runnerCount++;
        }
    }
    public void AddDropper()
    {
        var player = Networking.LocalPlayer;
        if (!isRunner && !isDropper && dropperCount <= 2)
        {
            Networking.SetOwner(player, gameObject);
            dropperLabels[dropperCount].SetLabelText(player.displayName);
            isDropper = true;
            dropperCount++;
        }
    }
    public bool HasPlayer(VRCPlayerApi[] list, VRCPlayerApi player)
    {
        foreach(VRCPlayerApi currentPlayer in list)
        {
            if(player == currentPlayer)
            {
                return true;
            }
        }
        return false;
    }

    public void ClearList(VRCPlayerApi[] list)
    {
        for(int i = 0; i < list.Length; i++)
        {
            list[i] = null;
        }
    }
}
