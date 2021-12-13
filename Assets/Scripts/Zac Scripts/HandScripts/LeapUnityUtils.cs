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

    public static Leap.Vector UnityV3ToLeapV3(Vector3 v)
    {
        //plugs values of unity vector into leapvector
        return new Leap.Vector(v.x, v.y, v.z);
    }

    public static Leap.LeapQuaternion UnityQuatToLeapQuat(Quaternion q)
    {
        //plugs values of leap quaternion into unity quaternion
        return new Leap.LeapQuaternion(q.x, q.y, q.z, q.w);
    }

    public static Leap.LeapTransform UnityTransformToLeapTransform(Transform t)
    {
        //cant have the inverse of this method as Transforms cannot be instantiated without a gameobject.
        Leap.Vector pos = UnityV3ToLeapV3(t.position);
        Leap.Vector scale = UnityV3ToLeapV3(t.localScale);
        Leap.LeapQuaternion quat = UnityQuatToLeapQuat(t.rotation);
        return new Leap.LeapTransform(pos, quat, scale);
    }


}
