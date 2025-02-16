using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;

public class information : MonoBehaviour
{
    public GameObject LightBandit;
    public CinemachineCamera cam;
    public GameObject HeavyBandit;
    public GameObject HeroKnight;
    public static information Reference {get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Reference == null) {
            Reference = this;
            DontDestroyOnLoad(gameObject);
            }
        else {
            Destroy(gameObject);
        }
    }
}

