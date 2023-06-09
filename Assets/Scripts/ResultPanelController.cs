using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LegendOfSlimeClone
{



    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_KillsText;
        [SerializeField] private Text m_ScoreText;
        [SerializeField] private Text m_TimeText;
        [SerializeField] private Text m_ResultText;
        [SerializeField] private Text m_ButtonNextText;
        [SerializeField] private Text m_BonusScoreForTime;
        [SerializeField] private Text m_TotalScore;


        private int m_BonusScore;
        private bool m_succes = false;

        public UnityEvent OnSaveStats;
        public int TotalScoreForSave { get; private set; }
        public int TotalKillsForSave { get; private set; }

        public void ShowResults(bool succes)
        {
            gameObject.SetActive(true);

            /*int bonusTime = LevelController.Instance.ReferenceTime - playerStatistics.Time;
            if (bonusTime > 0)
            {
                m_BonusScore = bonusTime * 10;
            }
            else
            {
                bonusTime = 0;
                m_BonusScore = 0;
            } 
            */
            m_succes = succes;

            m_ResultText.text = m_succes ? "������!" : "��������!";
            m_ButtonNextText.text = m_succes ? "�����" : "�������";
            //m_KillsText.text = "����������: " + playerStatistics.Kills.ToString();
            //m_ScoreText.text = "����: " + playerStatistics.Score.ToString();
            //float timeConditionForResultPanel = playerStatistics.Time;
            //float minutes = Mathf.FloorToInt(timeConditionForResultPanel / 60);
            //float seconds = Mathf.FloorToInt(timeConditionForResultPanel % 60);
            //m_TimeText.text = string.Format("�����:  {0:00} : {1:00}", minutes, seconds);
            //m_BonusScoreForTime.text = "����� �� �����: " + m_BonusScore;
            //m_TotalScore.text = "�����: " + (m_BonusScore + playerStatistics.Score);
            //TotalScoreForSave += m_BonusScore + playerStatistics.Score;
            //TotalKillsForSave += playerStatistics.Kills;
            OnSaveStats.Invoke();

            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);

            if (m_succes == true)
            {
                SceneLoader.Instance.AdvanceLevel();
            }
            else
            {
                SceneLoader.Instance.ResetLevel();
            }
        }

        public void OnButtonToMainMenuAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            SceneLoader.Instance.ToMainMenu();
        }


        void Start()
        {
            OnSaveStats.AddListener(SaveTotalStatistics);
            LoadTotalStatistics();
            gameObject.SetActive(false);
        }

        private void SaveTotalStatistics()
        {
            PlayerPrefs.SetInt("TotalKills", TotalKillsForSave);
            PlayerPrefs.SetInt("TotalScore", TotalScoreForSave);
        }

        private void LoadTotalStatistics()
        {
            TotalScoreForSave = PlayerPrefs.GetInt("TotalScore");
            TotalKillsForSave = PlayerPrefs.GetInt("TotalKills");
        }

        private void OnDestroy()
        {
            OnSaveStats.RemoveListener(SaveTotalStatistics);
        }

    }
}
