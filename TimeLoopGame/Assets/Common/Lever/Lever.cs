using UnityEngine;

public class Lever : MonoBehaviour
{
    public HingeJoint joint;
    public Rigidbody rb;
    public float force = 10;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (joint.angle < 0) 
        {
            rb.AddForce(Vector3.back * Time.deltaTime * force);
        }
        else
        {
            rb.AddForce(Vector3.forward * Time.deltaTime * force);
        }
    }
}
