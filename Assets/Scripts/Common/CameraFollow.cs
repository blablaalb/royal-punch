using UnityEngine;
using System;

//[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public Transform LookAt;
    public Transform MoveTo;
    public Transform RollTo;
    public float ChaseTime = 0.5f;


private Vector3 _lookAtOffset;
    private Vector3 _lookAtPosition
    {
        get
        {
            return LookAt.position + LookAt.rotation * _lookAtOffset;
        }
    }
    private Vector3 mRollVelocity;
    public float speed = 3;

    internal void Awake()
    {
        RollTo = LookAt;
        _lookAtOffset = -LookAt.transform.position;
    }


    //#if UNITY_EDITOR
    //        void Update()
    //        {
    //            if (!Application.isPlaying)
    //            {
    //                if (MoveTo)
    //                    transform.position = MoveTo.position;
    //                if (LookAt)
    //                {
    //                    if (!RollTo) transform.LookAt (LookAt);
    //                    else transform.LookAt (LookAt, RollTo.up);
    //                }
    //                // if (RollTo)
    //                //     transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, RollTo.rotation.eulerAngles.z));
    //            }
    //        }
    //#endif


    void LateUpdate()
    {
        if (MoveTo)
        {
            transform.position = Vector3.Lerp(transform.position, MoveTo.position, speed * Time.deltaTime);
            // transform.position=Vector3.SmoothDamp(transform.position, MoveTo.position, ref mVelocity, ChaseTime);
        }
        if (LookAt)
        {
            transform.LookAt(_lookAtPosition, Vector3.SmoothDamp(transform.up, RollTo.up, ref mRollVelocity, ChaseTime));


            //                if (!RollTo) transform.LookAt (LookAt);
            //                else transform.LookAt (LookAt, Vector3.SmoothDamp(transform.up, RollTo.up, ref mRollVelocity, ChaseTime));
        }

        if (RollTo)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, RollTo.rotation.eulerAngles.z));
        }
    }
}