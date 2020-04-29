using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PhysMeasure : UdonSharpBehaviour
{
    public float smooth_vel = 0.2f;
    public float smooth_acc = 0.2f;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 position = Vector3.zero;
    private Vector3 latestAcc = Vector3.zero;
    private Vector3 latestVel = Vector3.zero;
    private Vector3 latestPos = Vector3.zero;

    private void Update()
    {
        position = transform.position;
        velocity = calcVelocity(position, latestPos, latestVel);
        acceleration = calcAcceleration(velocity, latestVel, latestAcc);
        latestPos = position;
        latestVel = velocity;
        latestAcc = acceleration;
    }
    private Vector3 calcVelocity(Vector3 p, Vector3 lp, Vector3 lv)
    {
        return (1.0f - smooth_vel) * (p - lp) / Time.deltaTime + smooth_vel * lv;
    }

    private Vector3 calcAcceleration(Vector3 v, Vector3 lv, Vector3 la)
    {
        return (1.0f - smooth_acc) * (v - lv) / Time.deltaTime + smooth_acc * la;
    }

    public Vector3 getVelocity()
    {
        return velocity;
    }
    public Vector3 getAcceleration()
    {
        return acceleration;
    }
}