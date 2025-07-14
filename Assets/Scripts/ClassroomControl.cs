using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassroomControl : MonoBehaviour
{
    public bool goHome;

    public void UnlockClassroom()
    {
        goHome = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (goHome)
        {
            Debug.Log("Working");
            SceneManager.LoadScene(sceneName: "Journal Scene");
        }
    }
}
