using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerPhysMeasure : UdonSharpBehaviour
{

    public float smooth_vel = 0.2f;
    private Vector3 headPos;
    private Vector3 headVel;
    private Vector3 leftPos;
    private Vector3 leftVel;
    private Vector3 rightPos;
    private Vector3 rightVel;

    private Vector3 latestHeadPos;
    private Vector3 latestHeadVel;
    private Vector3 latestLeftPos;
    private Vector3 latestLeftVel;
    private Vector3 latestRightPos;
    private Vector3 latestRightVel;

    private void Start()
    {
        headPos = Vector3.zero;
        leftPos = Vector3.zero;
        rightPos = Vector3.zero;
        headVel = Vector3.zero;
        leftVel = Vector3.zero;
        rightVel = Vector3.zero;
    }
    private void Update()
    {
        var player = Networking.LocalPlayer;
        if (player == null)
        {
            return;
        }
        else
        {
            var headData = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
            var rightHandData = player.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand);
            var leftHandData = player.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand);
            headPos = headData.position;
            leftPos = leftHandData.position;
            rightPos = rightHandData.position;
            headVel = calcVelocity(headPos, latestHeadPos, latestHeadVel);
            leftVel = calcVelocity(leftPos, latestLeftPos, latestLeftVel);
            rightVel = calcVelocity(rightPos, latestRightPos, latestRightVel);
            latestHeadPos = headPos;
            latestLeftPos = leftPos;
            latestRightPos = rightPos;
            latestHeadVel = headVel;
            latestLeftVel = leftVel;
            latestRightVel = rightVel;
        }
    }

    private Vector3 calcVelocity(Vector3 p, Vector3 lp, Vector3 lv)
    {
        return (1.0f - smooth_vel) * (p - lp) / Time.deltaTime + smooth_vel * lv;
    }
    public Vector3 getHeadPos()
    {
        return headPos;
    }
    public Vector3 getHeadVel()
    {
        return headVel;
    }
    public Vector3 getLeftPos()
    {
        return leftPos;
    }
    public Vector3 getLeftVel()
    {
        return leftVel;
    }
    public Vector3 getRightPos()
    {
        return rightPos;
    }
    public Vector3 getRightVel()
    {
        return rightVel;
    }
}