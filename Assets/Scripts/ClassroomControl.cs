using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ClassroomControl : MonoBehaviour
{
    public bool goHome = false;
    public Animator sceneAnimator;
    public GameObject fadeScreen;

    private void Update()
    {
        if (fadeScreen.GetComponent<UnityEngine.UI.Image>().color.a >= 1)
        {
            goHome = true;
        }
        if (goHome)
        {
            SceneManager.LoadScene(sceneName: "Journal Scene");
        }
    }

    public void SitDown()
    {
        sceneAnimator.SetBool("playerSat", true);
    }
}
