using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PicnicControl : MonoBehaviour
{
    public bool endGame = false;
    public Animator sceneAnimator;
    public GameObject fadeScreen;


    private void Update()
    {
        if (fadeScreen.GetComponent<UnityEngine.UI.Image>().color.a >= 1)
        {
            endGame = true;
        }
        if (endGame)
        {
            SceneManager.LoadScene(sceneName: "End Credits");
        }
    }

    public void EndGame()
    {
        sceneAnimator.SetBool("playerSat", true);
    }
}
