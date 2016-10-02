using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{
    public void ReloadScene()
    {
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
