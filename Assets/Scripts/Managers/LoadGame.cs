using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadScene(int ID)
    {
        SceneManager.LoadScene(ID);
    }
}
