using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public CanvasScaler canvas;
    private float target;
    private float startTime;

    private void Start()
    {
        int _;
        portInput.onValidateInput += (input, characterIndex, addedChar) => int.TryParse(addedChar.ToString(), out _) ? addedChar : '\0';
        usernameInput.onValidateInput += (input, characterIndex, addedChar) => char.IsLetterOrDigit(addedChar) ? addedChar :'\0';

        GetComponent<NetworkController>().ServerConnected += HideUi;
        GetComponent<NetworkController>().ServerStopped += ShowUi;

        // Show UI if the player went game over and was playing single player
        GetComponent<GameController>().GameOver += success => {
            if (!GetComponent<NetworkController>().Connected) { ShowUi(); }
        };
    }

    private void HideUi() { target = 0f; startTime = Time.time; }
    private void ShowUi() { target = 1f; startTime = Time.time; }

    private void Update()
    {
        // Animation transition in and transition out
        var interpolated = Mathf.Lerp(1f - target, target, Time.time - startTime);
        background.color = new Color(background.color.r, background.color.g, background.color.b, interpolated);
        canvas.scaleFactor = interpolated;
        if (canvas.gameObject.activeInHierarchy && interpolated <= 0f) { canvas.gameObject.SetActive(false); }
        if (!canvas.gameObject.activeInHierarchy && interpolated > 0f) { canvas.gameObject.SetActive(true); }
    }

    public void ClickedSinglePlayer()
    {
        HideUi();
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

    private void ShowWarning(string warning) => StartCoroutine(DisplayWarning(warning));

    private IEnumerator DisplayWarning(string warning)
    {
        infoText.text = warning;
        infoText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);
        infoText.gameObject.SetActive(false);
    }
}
