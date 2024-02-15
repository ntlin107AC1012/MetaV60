using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using Oculus.Interaction.Input;
using Unity.Collections;
using UnityEngine;

public class mirrortest : MonoBehaviour
{
public GameObject leftHand;
    public GameObject rightHand;
    public float Pos_x, Pos_y, Pos_z;
    public float Rot_x, Rot_y, Rot_z;
    public GameObject leftHandAnchor;
    public GameObject rightHandAnchor;

    public Transform playerTransform;
    
    Vector3 currentPosition;
void Update()
    {
        Transform left = leftHand.GetComponent<Transform>();
        Transform right = rightHand.GetComponent<Transform>();
        MirrorFromTo(right, left);

    }
public void MirrorFromTo(Transform sourceTransform, Transform destTransform)
{
    // 计算鏡像位置
    Vector3 mirroredPosition = MirrorPosition(sourceTransform.position, playerTransform);
    destTransform.position = mirroredPosition;

    // 计算鏡像旋转
   // Quaternion mirroredRotation = MirrorRotation(sourceTransform.rotation, playerTransform);
   // destTransform.rotation = mirroredRotation;
}
Vector3 MirrorPosition(Vector3 sourcePosition, Transform userTransform)
{
    // 计算源位置相对于用户位置的局部坐标
    Vector3 localPos = userTransform.InverseTransformPoint(sourcePosition);

    // 反转 X 轴以创建鏡像效果
    localPos.x = -localPos.x;
    
    // 将局部坐标转换回世界坐标
    return userTransform.TransformPoint(localPos);
}
Quaternion MirrorRotation(Quaternion sourceRotation, Transform userTransform)
{
   // 将源旋转转换为相对于用户朝向的局部旋转
    Quaternion localRotation = Quaternion.Inverse(userTransform.rotation) * sourceRotation;

    // 获取局部旋转的欧拉角
    Vector3 localEulerAngles = localRotation.eulerAngles;

    // 反转 Y 和 Z 轴的旋转分量
    localEulerAngles.y = -localEulerAngles.y;
    localEulerAngles.z = -localEulerAngles.z;

    // 将调整后的局部旋转转换回全局旋转
    Quaternion mirroredRotation = userTransform.rotation * Quaternion.Euler(localEulerAngles);

    // 添加一个180度的旋转到X轴
    mirroredRotation *= Quaternion.Euler(180, 0, 0);
    return mirroredRotation;
}


float NormalizeAngle(float angle)
{
    while (angle > 180f) angle -= 360f;
    while (angle < -180f) angle += 360f;
    return angle;
}

}
