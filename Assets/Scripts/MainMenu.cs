using UnityEngine;
using UnityEngine.SceneManagement;

namespace LegendOfSlimeClone
{


    public class MainMenu : MonoBehaviour
    {
        public void LoadLevel()
        {
            SceneLoader.Instance.SaveSceneIndex(1);
            SceneManager.LoadScene(1);
        }

        public void QiutGame()
        {
            Application.Quit();
        }
    }
}