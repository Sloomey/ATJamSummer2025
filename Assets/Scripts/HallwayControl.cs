using UnityEngine;
using UnityEngine.SceneManagement;

public class HallwayControl : MonoBehaviour
{
    public bool goToClassroom;

    public void UnlockClassroom()
    {
        goToClassroom = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (goToClassroom)
        {
            Debug.Log("Working");
            SceneManager.LoadScene(sceneName: "ClassroomIdea");
        }
    }
}
