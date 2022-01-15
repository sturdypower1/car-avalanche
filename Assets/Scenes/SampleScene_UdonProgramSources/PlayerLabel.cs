
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class PlayerLabel : UdonSharpBehaviour
{
    public Text text;
    [UdonSynced]private string playerName = "None";

    public override void OnDeserialization()
    {
        text.text = playerName;
    }

    public void SetLabelText(string newText)
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        playerName = newText;
        text.text = newText;
    }
}
