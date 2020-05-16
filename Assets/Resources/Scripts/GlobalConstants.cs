using DiscordRPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConstants : MonoBehaviour
{
    public static GlobalConstants instance;
    DiscordRpcClient Client;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        instance = this;
        Client = new DiscordRpcClient("my_client_id");
    }

    void setDiscordPresense(string details, string state)
    {
        Client.SetPresence(new RichPresence()
        {
            Details = details,
            State = state
        });
    }
}
