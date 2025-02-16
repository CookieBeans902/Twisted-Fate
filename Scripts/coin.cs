using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class coin : MonoBehaviour
{
    public TextMeshProUGUI coins;
    void Update()
    {
        coins.text = GameManager.Instance.coinBalance.ToString();
    }
}
