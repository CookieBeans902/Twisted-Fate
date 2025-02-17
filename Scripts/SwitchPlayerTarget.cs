using Pathfinding;
using Unity.Cinemachine;
using UnityEngine;

public class SwitchTarget : MonoBehaviour

{
    public AIDestinationSetter ai;

    void Start()
    {
        ai = GetComponent<AIDestinationSetter>();
    }
    void Update() {
        
        if(GameManager.Instance.character==1) {
            ai.target = GameManager.Instance.LightBanditPosition;
        }
        else if(GameManager.Instance.character==2) {
            ai.target = GameManager.Instance.HeavyBanditPosition;
        }
        else {
            ai.target = GameManager.Instance.KnightPosition;
        }
    }   
}
