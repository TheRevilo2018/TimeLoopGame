using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float maxAccel = 10f;
    public float maxDistance = 2;

    public MonoBehaviour targetObject;
    private IMoveTarget target;

    private void Start()
    {
        target = (IMoveTarget)targetObject;
    }

    private void FixedUpdate()
    {
        var posDif = target.Target.position - transform.position;
        if (posDif.magnitude > maxDistance) return;

        var newVel = posDif * (1 / Time.fixedDeltaTime);
        var velDif = newVel - rb.linearVelocity;
        rb.linearVelocity += clampVector3(velDif, maxAccel);
    }

    private Vector3 clampVector3(Vector3 v, float clampVal)
    {
        var mag = v.magnitude;
        if (mag < clampVal) return v;

        return v * (maxAccel / mag);
    }
}
