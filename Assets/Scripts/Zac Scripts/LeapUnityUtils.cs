using UnityEngine;

public static class LeapUnityUtils
{
    public static Vector3 LeapV3ToUnityV3(Leap.Vector v)
    {
        return new Vector3(v.x, v.y, v.z);
    }
    
    public static Quaternion LeapQuatToUnityQuat(Leap.LeapQuaternion q)
    {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }
}
