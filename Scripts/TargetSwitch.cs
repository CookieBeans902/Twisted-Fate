using Pathfinding;
using Unity.Cinemachine;
using UnityEngine;

public class TargetSwitch : MonoBehaviour

{
    public CinemachineCamera cam;
    
    public void Switch() {
        
        if(GameManager.Instance.character==1) {
            cam.Follow = GameManager.Instance.LightBanditPosition;
        }
        else if(GameManager.Instance.character==2) {
            cam.Follow = GameManager.Instance.HeavyBanditPosition;
        }
        else {
            cam.Follow = GameManager.Instance.KnightPosition;
        }
    }   
}
