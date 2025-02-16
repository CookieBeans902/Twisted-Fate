using Pathfinding;
using Unity.Cinemachine;
using UnityEngine;

public class SwitchTarget : MonoBehaviour

{
    public AIDestinationSetter ai;
    
    public void Switch() {
        
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
