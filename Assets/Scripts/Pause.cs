using UnityEngine;

namespace LegendOfSlimeClone
{


    public class Pause : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnGamePause()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void OnGameResume()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        public void OnButonToMainMenu()
        {
            Time.timeScale = 1;
            SceneLoader.Instance.ToMainMenu();
        }
    }
}