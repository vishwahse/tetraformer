using UnityEngine;
using UnityEngine.SceneManagement;

public class RefreshLevel : MonoBehaviour
{
    [SerializeField] string levelName;
    public void Refresh()
    {
        SceneManager.LoadScene(levelName);
    }
}
