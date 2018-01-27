using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkController))]
public class UserInterfaceController : MonoBehaviour
{
    public InputField usernameInput;
    public InputField addressInput;
    public InputField portInput;

    public Text infoText;

    private void Start()
    {
        int _;
        portInput.onValidateInput += (input, characterIndex, addedChar) => int.TryParse(addedChar.ToString(), out _) ? addedChar : '\0';
        usernameInput.onValidateInput += (input, characterIndex, addedChar) => char.IsLetterOrDigit(addedChar) ? addedChar :'\0';
    }

    public void ClickedSinglePlayer()
    {
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
