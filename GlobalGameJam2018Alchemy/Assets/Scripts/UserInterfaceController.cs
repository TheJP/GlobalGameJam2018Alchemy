using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkController))]
[RequireComponent(typeof(GameController))]
public class UserInterfaceController : MonoBehaviour
{
    public bool onlySinglePlayer = false;

    public InputField usernameInput;
    public InputField addressInput;
    public InputField portInput;

    public Text infoText;

    public Image background;
    public Canvas mainMenu;
    public Canvas gameMenu;

    private float target = 1f;
    private float startTime;
    private CanvasGroup mainMenuGroup;

    private void Start()
    {
        mainMenuGroup = mainMenu?.GetComponent<CanvasGroup>();
        Assert.IsNotNull(mainMenuGroup);

        int _;
        portInput.onValidateInput += (input, characterIndex, addedChar) => int.TryParse(addedChar.ToString(), out _) ? addedChar : '\0';
        usernameInput.onValidateInput += (input, characterIndex, addedChar) => char.IsLetterOrDigit(addedChar) ? addedChar :'\0';

        GetComponent<NetworkController>().ServerConnected += SwitchToIngameMenu;
        GetComponent<NetworkController>().ServerStopped += SwitchToMainMenu;

        // Show UI if the player went game over and was playing single player
        GetComponent<GameController>().GameOver += success => {
            if (!GetComponent<NetworkController>().Connected) { SwitchToMainMenu(); }
        };

        if (onlySinglePlayer) { ClickedSinglePlayer(); startTime = -10f; }
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
        GetComponent<NetworkController>().PlaySinglePlayer();
    }

    public void ClickedMultiPlayer()
    {
        var username = usernameInput.text;
        var address = addressInput.text;
        int port = 0;
        if (!string.IsNullOrWhiteSpace(portInput.text)) { int.TryParse(portInput.text, out port); }

        if (string.IsNullOrWhiteSpace(username)) { ShowWarning("Invalid Username"); return; }
        if (string.IsNullOrWhiteSpace(address)) { ShowWarning("Invalid Address"); return; }

        try
        {
            if (port < 0 || port > 65000) { GetComponent<NetworkController>().Connect(username, address); }
            else { GetComponent<NetworkController>().Connect(username, address, port); }
        }
        catch (Exception)
        {
            ShowWarning("Could not connect to Server");
        }

    }

    public void ClickedQuitToMenu() => SwitchToMainMenu();

    private void ShowWarning(string warning) => StartCoroutine(DisplayWarning(warning));

    private IEnumerator DisplayWarning(string warning)
    {
        infoText.text = warning;
        infoText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);
        infoText.gameObject.SetActive(false);
    }
}
