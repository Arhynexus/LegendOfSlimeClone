using System;
using UnityEngine;

namespace LegendOfSlimeClone
{


    public class Bag : SingletonBase<Bag>
    {
        [SerializeField] private int m_Gold;
        public int GolD { get { return m_Gold; } }

        public Action<int> ChangeGold;
        private void Start()
        {
            LoadGoldData();
            ChangeGold += AddGold;
        }
        private void AddGold(int amount)
        {
            m_Gold += amount;
            SaveGoldData();
            Upgrades.Instance?.ChangeUpgradeTexts();
        }

        public void RemoveGold(int amount)
        {
            m_Gold -= amount;
            SaveGoldData();
            Upgrades.Instance.ChangeUpgradeTexts();
        }

        public void SaveGoldData()
        {
            PlayerPrefs.SetInt("Gold", m_Gold);
        }

        private void OnApplicationPause()
        {
            SaveGoldData();
        }

        public void LoadGoldData()
        {
            m_Gold = PlayerPrefs.GetInt("Gold");
        }

        private void OnDestroy()
        {
            ChangeGold -= AddGold;
        }
    }
}