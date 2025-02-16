using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI health;
    // Update is called once per frame
    void Update()
    {
        health.text = "Player Health: " + GameManager.Instance.PlayerHealth.ToString()+ "/200";
    }
}
