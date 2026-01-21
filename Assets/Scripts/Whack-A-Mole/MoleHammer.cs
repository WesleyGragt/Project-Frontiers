using UnityEngine;

public class MoleHammer : MonoBehaviour
{
    public LerpTo lerp;
    public Transform idleHammerPos;
    public Transform gameOffHammerPos;
    public Transform moleWhackHammerPos;
    public Vector3 moleHammerOffset;
    public float moleWhackResetDistance;



    private void Update()
    {
        if(Vector3.Distance(transform.position, moleWhackHammerPos.position) < moleWhackResetDistance)
        {
            lerp.target = idleHammerPos;
        }
    }


    public void SetIdle()
    {
        lerp.target = idleHammerPos;
    }


    public void PutDown()
    {
        lerp.target = gameOffHammerPos;
    }


    public void SetWhack()
    {
        lerp.target = moleWhackHammerPos;
    }


}
