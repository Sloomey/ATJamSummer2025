using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ClassroomControl : MonoBehaviour
{
    public bool goHome = false;
    public Animator sceneAnimator;
    public GameObject fadeScreen;

    private GameControl gameControl;

    private void Awake()
    {
        gameControl = GameObject.FindAnyObjectByType<GameControl>();
    }

    private void Update()
    {
        if (fadeScreen.GetComponent<UnityEngine.UI.Image>().color.a >= 1)
        {
            goHome = true;
        }
        if (goHome)
        {
            gameControl.gameWeek += 1;
            if (gameControl.gameWeek >= 4)
            {
                SceneManager.LoadScene(sceneName: "PicnicIdea");
            }
            else
            {
                SceneManager.LoadScene(sceneName: "Journal Scene");
            }
                
        }
    }

    public void SitDown()
    {
        sceneAnimator.SetBool("playerSat", true);
    }
}
