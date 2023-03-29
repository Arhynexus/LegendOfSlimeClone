using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LegendOfSlimeClone
{


    public class SceneLoader : SingletonBase<SceneLoader>
    {
        [SerializeField] private Slime m_PlayerSlime;
        public Action ReloadScene;
        private int m_SceneIndex = 1;

        private Timer m_CheckTimer = new Timer(0);
        private float m_CheckTime = 1f;

        private void Start()
        {
            LoadSceneIndex();
            ReloadScene += ResetLevel;
            m_CheckTimer.Start(m_CheckTime);
            Time.timeScale = 1;
        }
        public void ResetLevel()
        {
            SceneManager.LoadScene(m_SceneIndex);
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void Update()
        {
            m_CheckTimer.RemoveTime(Time.deltaTime);
            if(m_CheckTimer.IsFinished)
            {
                if(m_PlayerSlime)
                {
                    if (m_PlayerSlime.CurrentHealth <= 0)
                    {
                        ResultPanelController.Instance.ShowResults(false);
                    }
                }
            }
        }

        public void SaveSceneIndex(int index)
        {
            PlayerPrefs.SetInt("SceneIndex", index);
        }

        private void LoadSceneIndex()
        {
            if(PlayerPrefs.GetInt("SceneIndex") !=0)
            {
                m_SceneIndex = PlayerPrefs.GetInt("SceneIndex");
            }
        }
        public void AdvanceLevel()
        {
            m_SceneIndex += 1;
            SaveSceneIndex(m_SceneIndex);
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var lastSceneIndex = sceneCount - 1; // индекс последней сцены
            if (m_SceneIndex > lastSceneIndex)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                SceneManager.LoadScene(m_SceneIndex);
            }
        }
    }
}