using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Debug.Log("Game is exiting...");
    }
}