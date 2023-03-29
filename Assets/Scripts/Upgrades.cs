using System;
using UnityEngine;
using UnityEngine.UI;

namespace LegendOfSlimeClone
{


    public class Upgrades : SingletonBase<Upgrades>
    {
        [SerializeField] private Bag m_PlayerBag;
        [SerializeField] private Slime m_PlayerSlime;
        [SerializeField] private TurretProperties m_PlayerTurretProperties;
        [Header("CostTexts")]
        [SerializeField] private Text m_HealthUpgradeCostText;
        [SerializeField] private Text m_AttackUpgradeCostText;
        [SerializeField] private Text m_AttackSpeedUpgradeCostText;
        [SerializeField] private Text m_HealthRegenUpgradeCostText;
        [Header("LevelTexts")]
        [SerializeField] private Text m_AttackSpeedUpgradeLevelText;
        [SerializeField] private Text m_HealthUpgradeLevelText;
        [SerializeField] private Text m_AttackUpgradeLevelText;
        [SerializeField] private Text m_HealthRegenLevelText;
        [Header("Buttons")]
        [SerializeField] private Button m_HealthUpgradeButton;
        [SerializeField] private Button m_AttackUpgradeButton;
        [SerializeField] private Button m_AttackSpeedUpgradeButton;
        [SerializeField] private Button m_HealthRegenUpgradeButton;

        [SerializeField] private Text m_GoldText;


        public Action ChangeUpgradeTexts;

        private int m_HealthUpgradeCost = 5;
        private int m_AttackSpeedUpgradeCost = 30;
        private int m_AttackUpgradeCost = 50;
        private int m_HealthRegenUpgradeCost = 15;

        private int m_AttackUpgradeLevel = 1;
        private int m_AttackSpeedUpgradeLevel = 1;
        private int m_HealthUpgradeLevel = 1;
        private int m_HealthRegenLevel = 1;

        private void Start()
        {
            LoadUpgrades();
            UpdateTexts();
            ChangeUpgradeTexts += UpdateTexts;
            ChangeUpgradeTexts();
        }


        private void UpdateTexts()
        {
            m_HealthUpgradeCostText.text = $"Cost: {m_HealthUpgradeCost}";
            m_AttackUpgradeCostText.text = $"Cost: {m_AttackUpgradeCost}";
            m_AttackSpeedUpgradeCostText.text = $"Cost: {m_AttackSpeedUpgradeCost}";
            m_HealthRegenUpgradeCostText.text = $"Cost: {m_HealthRegenUpgradeCost}";

            m_HealthUpgradeLevelText.text = $"Level: {m_HealthUpgradeLevel}";
            m_AttackUpgradeLevelText.text = $"Level: {m_AttackUpgradeLevel}";
            m_AttackSpeedUpgradeLevelText.text = $"Level: {m_AttackSpeedUpgradeLevel}";
            m_HealthRegenLevelText.text = $"Level: {m_HealthRegenLevel}";

            m_HealthUpgradeButton.interactable = m_PlayerBag.GolD >= m_HealthUpgradeCost;
            m_AttackUpgradeButton.interactable = m_PlayerBag.GolD >= m_AttackUpgradeCost;
            m_AttackSpeedUpgradeButton.interactable = m_PlayerBag.GolD >= m_AttackSpeedUpgradeCost;
            m_HealthRegenUpgradeButton.interactable = m_PlayerBag.GolD >= m_HealthRegenUpgradeCost;

            m_GoldText.text = m_PlayerBag.GolD.ToString() ;
        }

        public void UpgradeHealth(int amount)
        {
            if (m_PlayerBag.GolD >= m_HealthUpgradeCost)
            {
                m_PlayerBag.RemoveGold(m_HealthUpgradeCost);
                m_PlayerSlime.SetMaxHealth(amount);
                m_HealthUpgradeCost += 5;
                m_HealthUpgradeLevel += 1;
                ChangeUpgradeTexts();
                SaveUpgrades();
            }
        }

        public void UpgradeAttack(int amount)
        {
            if (m_PlayerBag.GolD >= m_AttackUpgradeCost)
            {
                m_PlayerBag.RemoveGold(m_AttackUpgradeCost);
                m_PlayerTurretProperties.IncreaseDamage(amount);
                m_AttackUpgradeCost += 50;
                m_AttackUpgradeLevel += 1;
                ChangeUpgradeTexts();
                SaveUpgrades();
            }
        }

        public void UpgradeAttackSpeed(int amount)
        {
            if (m_PlayerBag.GolD >= m_AttackSpeedUpgradeCost)
            {
                m_PlayerBag.RemoveGold(m_AttackSpeedUpgradeCost);
                m_PlayerTurretProperties.DecreseRefireTime(amount);
                m_AttackSpeedUpgradeCost += 30;
                m_AttackSpeedUpgradeLevel += 1;
                ChangeUpgradeTexts();
                SaveUpgrades();
            }
        }

        public void UpGradeHealthRegen()
        {
            if (m_PlayerBag.GolD >= m_HealthRegenUpgradeCost)
            {
                m_PlayerBag.RemoveGold(m_HealthRegenUpgradeCost);
                m_HealthRegenUpgradeCost += 15;
                m_PlayerSlime.IncreaseHealthRegen();
                ChangeUpgradeTexts();
                SaveUpgrades();
            }
        }

        public void SaveUpgrades()
        {
            PlayerPrefs.SetInt("Damage", m_PlayerTurretProperties.Damage);
            PlayerPrefs.SetInt("HealthRegen", m_PlayerSlime.HealthRegenAmount);
            PlayerPrefs.SetInt("Health", m_PlayerSlime.MaxHealth);
            PlayerPrefs.SetFloat("AttackSpeed", m_PlayerTurretProperties.RateOfFire);
            SaveLevelUpgrades();
        }

        public void LoadUpgrades()
        {
            if (PlayerPrefs.GetInt("Damage") != 0)
            {
                m_PlayerTurretProperties.SetDamage(PlayerPrefs.GetInt("Damage"));
            }
            if (PlayerPrefs.GetInt("HealthRegen") != 0)
            {
                m_PlayerSlime.SetHealthRegen(PlayerPrefs.GetInt("HealthRegen"));
            }
            if (PlayerPrefs.GetInt("Health") != 0)
            {
                m_PlayerSlime.SetMaxHealth(PlayerPrefs.GetInt("Health"));
            }
            if (PlayerPrefs.GetFloat("AttackSpeed") != 0)
            {
                m_PlayerTurretProperties.SetRateOfFire(PlayerPrefs.GetFloat("AttackSpeed"));
            }
            LoadLevelUpgrades();
        }

        public void SaveLevelUpgrades()
        {
            PlayerPrefs.SetInt("LevelDamage", m_AttackUpgradeLevel);
            PlayerPrefs.SetInt("LevelHealth", m_HealthUpgradeLevel);
            PlayerPrefs.SetInt("LevelAttackSpeed", m_AttackSpeedUpgradeLevel);
            PlayerPrefs.SetInt("LevelHealthRegen", m_HealthRegenLevel);
        }

        private void LoadLevelUpgrades()
        {
            if (PlayerPrefs.GetInt("LevelDamage") != 0)
            {
                m_AttackUpgradeLevel = PlayerPrefs.GetInt("LevelDamage");
                m_AttackUpgradeCost *= m_AttackUpgradeLevel;
            }
            if (PlayerPrefs.GetInt("LevelHealth") != 0)
            {
                m_HealthUpgradeLevel = PlayerPrefs.GetInt("LevelHealth");
                m_HealthUpgradeCost *= m_HealthUpgradeLevel;
            }
            if (PlayerPrefs.GetInt("LevelAttackSpeed") != 0)
            {
                m_AttackSpeedUpgradeLevel = PlayerPrefs.GetInt("LevelAttackSpeed");
                m_AttackSpeedUpgradeCost *= m_AttackSpeedUpgradeLevel;
            }
            if (PlayerPrefs.GetInt("LevelHealthRegen") != 0)
            {
                m_HealthRegenLevel = PlayerPrefs.GetInt("LevelHealthRegen");
                m_HealthRegenUpgradeCost *= m_HealthRegenLevel;
            }
        }

        private void OnApplicationPause()
        {
            SaveUpgrades();
            SaveLevelUpgrades();
        }
    }
}