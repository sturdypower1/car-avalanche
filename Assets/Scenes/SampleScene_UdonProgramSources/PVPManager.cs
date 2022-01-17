
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

    [UdonSynced] public int[] runnerIds = new int[8];
    private VRCPlayerApi[] runnerPlayers;
    [UdonSynced] public int[] dropperIds = new int[2];
    private VRCPlayerApi[] dropperPlayers;
    //[UdonSynced] private bool[] runnersAlive = new bool[1];

    public Transform runnerTeleport;
    public Transform dropperTeleport;
    public Transform spawnTeleport;

    public PlayerLabel[] runnerLabels;
    public PlayerLabel[] dropperLabels;
    public GameObject playerSelect;

    private bool isSetup = false;
    [UdonSynced] private bool isStarted = false;


    

    // got help here from pupppet 

    public override void OnDeserialization() => _UpdateTeamPlayers();

    private void _UpdateTeamPlayers()
    {
        runnerPlayers = new VRCPlayerApi[runnerIds.Length];
        for (int i = 0; i < runnerIds.Length; ++i)
        {
            if (runnerIds[i] == 0)
            {
                runnerPlayers[i] = null;
            }
            else
            {
                runnerPlayers[i] = VRCPlayerApi.GetPlayerById(runnerIds[i]);
            }
        }
    }

    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        if (Networking.IsOwner(gameObject))
        {
            Debug.Log("this is the owner");
        }
        for (int i = 0; i < runnerPlayers.Length; ++i)
        {
            if (runnerPlayers[i] == player)
            {
                Debug.Log("runner ID " + runnerIds[i] + " has left the world.");
                runnerIds[i] = 0;
            }
            
        }
    }

    /*public override void OnPlayerLeft(VRCPlayerApi player)
    {
        int runnerIndex = FindRunner(player);
        int dropperIndex = FindDropper(player);
        if (player.GetPlayerTag("runner") == "")
        {
            Debug.Log("was runner");
        }
        else if (player.GetPlayerTag("dropper") == "")
        {
            Debug.Log("was dropper");
        }
    }*/
    public int FindRunner(VRCPlayerApi player)
    {
        /*for(int i = 0; i < runnerIds.Length; i++)
        {
            if(runnerIds[i] == playerid)
            {
                return i;
            }
        }*/
        return -1;
    }
    public int FindDropper(VRCPlayerApi player)
    {
        for (int i = 0; i < dropperIds.Length; i++)
        {
            if (VRCPlayerApi.GetPlayerById(dropperIds[i]) == player)
            {
                return i;
            }
        }
        return -1;
    }
    public void RunnerVictor()
    {
        var player = Networking.LocalPlayer;
        isStarted = false;
        runnerCount = 0;
        dropperCount = 0;
        foreach(PlayerLabel label in runnerLabels)
        {
            label.SetLabelText("None");
        }
        foreach (PlayerLabel label in dropperLabels)
        {
            label.SetLabelText("None");
        }

        Networking.SetOwner(player, gameObject);
        ResetMap();
    }

    public void ResetMap()
    {
        isSetup = false;
        playerSelect.SetActive(true);
        
        var player = Networking.LocalPlayer;
        if(isRunner || isDropper)
        {
            player.TeleportTo(spawnTeleport.position, Quaternion.identity);
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
        else if(!isSetup && isStarted)
        {
            playerSelect.SetActive(false);
            isSetup = true;
        }
        else if(isSetup && !isStarted)
        {
            ResetMap();
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
            runnerIds[runnerCount] = player.playerId;
            player.SetPlayerTag("runner");
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
            player.SetPlayerTag("dropper");
            dropperIds[dropperCount] = player.playerId;
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
