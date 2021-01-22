using TMPro;
using UnityEngine;

namespace Assets.Scripts.UIElements
{
    public class CounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;

        public void UpdateCounterValue(int value)
        {
            textField.text = value.ToString();
        }
    }
}