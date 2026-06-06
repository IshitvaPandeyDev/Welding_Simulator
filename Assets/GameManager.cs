using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MeshRenderer fillerRenderer;

    public void ResetFillerColor()
    {
        if (fillerRenderer != null)
        {
            Color c = fillerRenderer.material.color;

            c.a = 0f;

            fillerRenderer.material.color = c;

            Debug.Log("Filler reset to transparent.");
        }
        else
        {
            Debug.LogError("No Filler Renderer assigned to GameManager!");
        }
    }
}