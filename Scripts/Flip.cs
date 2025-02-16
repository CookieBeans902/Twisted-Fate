using UnityEngine;
using Pathfinding;

public class Flip : MonoBehaviour
{
    public AIPath aiPath;
    void Start()
    {
        aiPath = GetComponentInParent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
