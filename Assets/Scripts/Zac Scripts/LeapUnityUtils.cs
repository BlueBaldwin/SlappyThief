using UnityEngine;

public static class LeapUnityUtils
{
    public static Vector3 LeapV3ToUnityV3(Leap.Vector v)
    {
        //plugs values of leap vector into unity vector
        return new Vector3(v.x, v.y, v.z);
    }
    
    public static Quaternion LeapQuatToUnityQuat(Leap.LeapQuaternion q)
    {
        //plugs values of leap quaternion into unity quaternion
        return new Quaternion(q.x, q.y, q.z, q.w);
    }
}
