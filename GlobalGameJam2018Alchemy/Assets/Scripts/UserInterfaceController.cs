using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkController))]
[RequireComponent(typeof(GameController))]
public class UserInterfaceController : MonoBehaviour
{
    public InputField usernameInput;
    public InputField addressInput;
    public InputField portInput;

    public Text infoText;

    public Image background;
    public Canvas mainMenu;
    public Canvas gameMenu;

    public event Action GameStarted;

    private float target = 1f;
    private float startTime;
    private CanvasGroup mainMenuGroup;

    private NetworkController network;

    private void Start()
    {
        mainMenuGroup = mainMenu?.GetComponent<CanvasGroup>();
        Assert.IsNotNull(mainMenuGroup);

        // Main menu user input validation
        portInput.onValidateInput += (input, characterIndex, addedChar) => char.IsDigit(addedChar) ? addedChar : '\0';
        usernameInput.onValidateInput += (input, characterIndex, addedChar) => char.IsLetterOrDigit(addedChar) ? addedChar : '\0';

        // Handle network callbacks
        network = GetComponent<NetworkController>();
        network.ServerConnected += () =>
        {
            SwitchToIngameMenu();
            GameStarted?.Invoke();
        };
        network.ServerStopped += () => SceneManager.LoadScene(0);

        // Show UI if the player went game over and was playing single player
        GetComponent<GameController>().GameOver += success =>
        {
            if (!network.Connected) { SceneManager.LoadScene(0); }
        };
    }

    private void SwitchToIngameMenu()
    {
        target = 0f;
        startTime = Time.time;
        mainMenuGroup.interactable = false;
        gameMenu.gameObject.SetActive(true);
    }
    private void SwitchToMainMenu()
    {
        target = 1f;
        startTime = Time.time;
        mainMenuGroup.interactable = true;
        gameMenu.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Animation transition in and transition out
        var interpolated = Mathf.Lerp(1f - target, target, Time.time - startTime);
        mainMenuGroup.alpha = interpolated;
        if (mainMenu.enabled && interpolated <= 0f) { mainMenu.enabled = false; }
        if (!mainMenu.enabled && interpolated > 0f) { mainMenu.enabled = true; }
    }

    public void ClickedSinglePlayer()
    {
        SwitchToIngameMenu();
        GameStarted?.Invoke();
    }

    public void ClickedMultiPlayer()
    {
        var username = usernameInput.text;
        var address = addressInput.text;
        int port = 0;
        if (!string.IsNullOrWhiteSpace(portInput.text)) { int.TryParse(portInput.text, out port); }

        if (string.IsNullOrWhiteSpace(username)) { ShowWarning("Invalid Username"); return; }
        if (string.IsNullOrWhiteSpace(address)) { ShowWarning("Invalid Address"); return; }
        if (port < 0 || port > 65000) { ShowWarning("Invalid Port"); return; }

        try
        {
            if (port == 0) { network.Connect(username, address); }
            else { network.Connect(username, address, port); }
        }
        catch (Exception)
        {
            ShowWarning("Could not connect to Server");
        }

    }

    public void ClickedQuitToMenu()
    {
        if (!network.IsSinglePlayer) { network.Disconnect(); }
        SceneManager.LoadScene(0);
    }

    private void ShowWarning(string warning) => StartCoroutine(DisplayWarning(warning));

    private IEnumerator DisplayWarning(string warning)
    {
        infoText.text = warning;
        infoText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);
        infoText.gameObject.SetActive(false);
    }
}
