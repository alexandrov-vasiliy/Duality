using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    public void LoadGameplay()
    {
        SceneManager.LoadScene(1);
    }
}
