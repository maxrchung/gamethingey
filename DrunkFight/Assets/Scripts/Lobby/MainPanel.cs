using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainPanel : MonoBehaviour {
    public LobbyManager lobbyManager;
    public RectTransform lobbyPanel;
    public GameObject startButton;
    public GameObject hostCover;
    public InputField ipInput;

    public void OnEnable()
    {
        ipInput.onEndEdit.RemoveAllListeners();
        ipInput.onEndEdit.AddListener(onEndEditIP);
    }

    public void OnClickHost()
    {
        lobbyManager.StartHost();
        lobbyManager.isHost = true;
        startButton.SetActive(true);
        hostCover.SetActive(false);
    }

    public void OnClickJoin()
    {
        if (ipInput.text.Length > 0)
        {
            lobbyManager.ChangeTo(lobbyPanel);
            lobbyManager.networkAddress = ipInput.text;
            lobbyManager.StartClient();
            startButton.SetActive(false);
            hostCover.SetActive(true);
        }
    }

    void onEndEditIP(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) && ipInput.text.Length > 0)
        {
            OnClickJoin();
        }
    }
}
