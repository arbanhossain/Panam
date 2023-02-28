using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIHandler : MonoBehaviour
{
    public TMP_InputField InputField;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerNickname"))
        {
            InputField.text = PlayerPrefs.GetString("PlayerNickname");
        }
    }

    // Update is called once per frame
    public void OnJoinGameClicked()
    {
        PlayerPrefs.SetString("PlayerNickname", InputField.text);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }
}
