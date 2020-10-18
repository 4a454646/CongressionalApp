using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSystem : MonoBehaviour {
    public string playerPrefString;
    public GameObject textField;
    private void Start() {
        print($"set {playerPrefString} to {PlayerPrefs.GetString(playerPrefString)} on startup");
        textField.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString(playerPrefString);
    }
    public void saveValueToPrefs() {
        // very wonky save system. couldn't get it to properly work, so the solution here is to set this gameobject's name to be that of the input field (when finished editing), and read off this function. a new gameobject with this script is used for text field
        PlayerPrefs.SetString(playerPrefString, name);
        print($"saved {name} under {playerPrefString}");
        print(PlayerPrefs.GetString(playerPrefString));
    }
}
