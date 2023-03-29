using UnityEngine;
using UnityEngine.Events;

namespace LegendOfSlimeClone
{


    public class Trigger : MonoBehaviour
    {
        public UnityEvent InteredTrigger;
        public void OnTriggerEnter(Collider other)
        {
            Slime slime = other.transform.root.GetComponent<Slime>();
            if (slime != null)
            {
                InteredTrigger.Invoke();
                Destroy(gameObject);
            }
        }
    }
}