using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }
}
