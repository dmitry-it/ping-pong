using TMPro;
using UnityEngine;

namespace UIElements
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