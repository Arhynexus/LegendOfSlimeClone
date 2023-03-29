using UnityEngine;
using UnityEngine.UI;

namespace LegendOfSlimeClone
{

    public class UpdateHealthUI : MonoBehaviour
    {
        [SerializeField] private Image m_HealthImage;
        [SerializeField] private Destructible m_Destructible;
        [SerializeField] private Slime m_PlayerSlime;
        [SerializeField] private Text m_ReceivedDamageText;
        [SerializeField] private Text m_ReceivedRegenText;
        [SerializeField] private Animator m_DamageTextAnimator;
        [SerializeField] private Animator m_RegenTextAnimator;

        private Timer m_TextTimer = new Timer(0);
        private Timer m_RegenTextTimer = new Timer(0);
        private float m_TextTime = 0.3f;

        private void Start()
        {
            m_ReceivedDamageText.enabled = false;
            m_DamageTextAnimator.enabled = false;
            m_Destructible.ChangeHitpoints += UpdateHealth;
            m_Destructible.ReceivingDamage += UpdateDamageText;
            if (m_PlayerSlime)
            {
                m_PlayerSlime.ReceivingRegen += UpdateRegenText;
            }
        }
        private void UpdateHealth()
        {
            int maxHealth = m_Destructible.MaxHealth;
            int currentHealth = m_Destructible.CurrentHealth;
            m_HealthImage.fillAmount = (float) currentHealth / (float) maxHealth;
        }

        private void UpdateDamageText(int damage)
        {
            m_TextTimer.Start(m_TextTime);
            m_ReceivedDamageText.color = Color.red;
            m_ReceivedDamageText.enabled = true;
            m_ReceivedDamageText.text = damage.ToString();
            m_DamageTextAnimator.enabled = true;
            m_DamageTextAnimator.Play(0);
        }

        private void UpdateRegenText(int amount)
        {
            if (m_PlayerSlime)
            {
                if(m_PlayerSlime.CurrentHealth < m_PlayerSlime.MaxHealth)
                {
                    amount = m_PlayerSlime.HealthRegenAmount;
                    m_RegenTextTimer.Start(m_TextTime);
                    m_ReceivedRegenText.color = Color.green;
                    m_ReceivedRegenText.enabled = true;
                    m_ReceivedRegenText.text = amount.ToString();
                    m_RegenTextAnimator.enabled = true;
                    m_RegenTextAnimator.Play(0);
                }
            }
        }

        private void Update()
        {
            m_TextTimer.RemoveTime(Time.deltaTime);
            m_RegenTextTimer.RemoveTime(Time.deltaTime);
            if (m_TextTimer.IsFinished)
            {
                m_ReceivedDamageText.enabled = false;
                m_DamageTextAnimator.enabled = false;
            }

            if (m_ReceivedRegenText)
            {
                if (m_RegenTextTimer.IsFinished)
                {
                    if(m_RegenTextAnimator)
                    {
                        m_ReceivedRegenText.enabled = false;
                        m_RegenTextAnimator.enabled = false;
                    }
                }
            }
        }
    }
}