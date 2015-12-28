using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainPanel : MonoBehaviour {
    public LobbyManager lobbyManager;
    public RectTransform lobbyPanel;
    public InputField ipInput;

    public void OnEnable()
    {
        ipInput.onEndEdit.RemoveAllListeners();
        ipInput.onEndEdit.AddListener(onEndEditIP);
    }

    public void OnClickHost()
    {
        lobbyManager.StartHost();
    }

    public void OnClickJoin()
    {
        lobbyManager.ChangeTo(lobbyPanel);
        lobbyManager.networkAddress = ipInput.text;
        lobbyManager.StartClient();
        Debug.Log("Joined: " + lobbyManager.networkAddress);
    }


    void onEndEditIP(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClickJoin();
        }
    }
}
