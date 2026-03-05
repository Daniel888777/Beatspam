using UnityEngine;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public static string PlayerName;

    public void OnNameEntered()
    {
        PlayerName = nameInputField.text.Trim();
        if (string.IsNullOrEmpty(PlayerName))
        {
            PlayerName = "Player"; // default name
        }
        Debug.Log("Player name set to: " + PlayerName);
    }
}
