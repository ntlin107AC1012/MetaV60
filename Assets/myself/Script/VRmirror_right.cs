using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using Oculus.Interaction.Input;
using Unity.Collections;
using UnityEngine;

public class VRmirror_right : MonoBehaviour
{


    public GameObject leftHand;
    public GameObject rightHand;
    public float Pos_x, Pos_y, Pos_z;
    public float Rot_x, Rot_y, Rot_z;
    public GameObject leftHandAnchor;
    public GameObject rightHandAnchor;

    public Transform playerTransform;
    
    Vector3 currentPosition;



    public void ResetMirror()
    {
        leftHand.transform.position = leftHandAnchor.transform.position;
        leftHand.transform.rotation = leftHandAnchor.transform.rotation;
        rightHand.transform.position = rightHandAnchor.transform.position;
        rightHand.transform.rotation = rightHandAnchor.transform.rotation;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform left = leftHand.GetComponent<Transform>();
        Transform right = rightHand.GetComponent<Transform>();

        //EnableMirror();

        //MirrorFromTo(left, right);
        MirrorFromTo(right, left);



    }
    public void EnableMirror()
    {

        // rightHand.SetActive(true);
        // OVRHand ovrHand = GetComponent<OVRHand>();
        // OVRHand.Hand handType = GetComponent<OVRHand>().handType.HandRight;
       // leftHand.GetComponent<OVRHand>().handType = OVRHand.Hand.HandRight;

    }
    public void SetCurrentTransform()
    {
        currentPosition = playerTransform.right;
    }

//    public void MirrorFromTo(Transform sourceTransform, Transform destTransform)
// {
//     // 计算鏡像位置
//     //Vector3 mirroredPosition = MirrorPosition(sourceTransform.position, playerTransform.position);
//     // 计算鏡像位置
//     Vector3 mirroredPosition = MirrorPosition(sourceTransform.position, playerTransform);
//     destTransform.position = mirroredPosition;

//     // 计算鏡像旋转
//     //Quaternion mirroredRotation = MirrorRotation(sourceTransform.rotation, playerTransform.rotation);
//     //Quaternion mirroredRotation = MirrorRotation(sourceTransform.rotation, playerTransform.rotation);
//     Quaternion mirroredRotation = MirrorRotation(sourceTransform.rotation, playerTransform);
//     // destTransform.rotation = mirroredRotation;
//     // 保持鏡像手的旋转不变，只改变位置
//     destTransform.rotation = sourceTransform.rotation;
// }
public void MirrorFromTo(Transform sourceTransform, Transform destTransform)
{
    // // 直接计算鏡像位置，不考虑玩家头部的旋转
    // Vector3 mirroredPosition = sourceTransform.position;
    // mirroredPosition.x = -mirroredPosition.x; // 反转 X 轴以创建鏡像效果
    // destTransform.position = mirroredPosition;

    // // 保持鏡像手的旋转与源手相同
    // destTransform.rotation = sourceTransform.rotation;

     // 计算鏡像位置
    Vector3 mirroredPosition = MirrorPosition(sourceTransform.position, playerTransform);
    destTransform.position = mirroredPosition;

    // 保持鏡像手的旋转与源手相同
    destTransform.rotation = sourceTransform.rotation;
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


// Quaternion MirrorRotation(Quaternion sourceRotation)
// {
//     // 这里简单地使用源旋转，您可能需要根据您的具体需求进行调整
//     //return sourceRotation;
//     // 反转 Y 轴和 Z 轴的旋转分量
//     return new Quaternion(sourceRotation.x, -sourceRotation.y, -sourceRotation.z, -sourceRotation.w);
// }
    // Quaternion MirrorRotation(Quaternion sourceRotation, Quaternion userRotation)
    // {
    // // 将源旋转转换为相对于用户朝向的局部旋转
    // Quaternion localRotation = Quaternion.Inverse(userRotation) * sourceRotation;

    // // 反转局部旋转的某些分量
    // Vector3 localEuler = localRotation.eulerAngles;
    // localEuler.y = -localEuler.y;
    // localEuler.z = -localEuler.z;

    // // 将局部旋转转换回全局旋转
    // return userRotation * Quaternion.Euler(localEuler);
    // }
//     Quaternion MirrorRotation(Quaternion sourceRotation, Transform referenceTransform)
// {
//     // 将源旋转转换为相对于参考Transform的局部旋转
//     Quaternion localRotation = Quaternion.Inverse(referenceTransform.rotation) * sourceRotation;

//     // 在局部空间中进行鏡像旋转：这里我们反转Y和Z轴
//     Vector3 localEulerAngles = localRotation.eulerAngles;
//     localEulerAngles.y = 180 - localEulerAngles.y;
//     localEulerAngles.z = -localEulerAngles.z;

//     // 创建鏡像后的局部旋转
//     Quaternion mirroredLocalRotation = Quaternion.Euler(localEulerAngles);

//     // 将鏡像后的局部旋转转换回世界空间
//     return referenceTransform.rotation * mirroredLocalRotation;
// }
// Quaternion MirrorRotation(Quaternion sourceRotation, Transform referenceTransform)
// {
//     // // 转换为局部空间的旋转
//     // Quaternion localRotation = Quaternion.Inverse(referenceTransform.rotation) * sourceRotation;

//     // // 获取局部空间的旋转轴和角度
//     // localRotation.ToAngleAxis(out float angle, out Vector3 axis);

//     // // 镜像旋转轴
//     // axis = referenceTransform.InverseTransformDirection(axis); // 转换到局部空间
//     // axis.x = -axis.x; // 镜像X轴
//     // axis = referenceTransform.TransformDirection(axis); // 转换回全局空间

//     // // 创建镜像后的旋转
//     // Quaternion mirroredRotation = Quaternion.AngleAxis(angle, axis);

//     // return mirroredRotation;
//     // 将源旋转转换为相对于玩家的局部旋转
//     Quaternion localRotation = Quaternion.Inverse(referenceTransform.rotation) * sourceRotation;

//     // 鏡像局部旋转的前方（Z轴）
//     Vector3 mirroredForward = Vector3.Scale(referenceTransform.InverseTransformDirection(localRotation * Vector3.forward), new Vector3(-1, 1, 1));

//     // 鏡像局部旋转的上方（Y轴）
//     Vector3 mirroredUp = Vector3.Scale(referenceTransform.InverseTransformDirection(localRotation * Vector3.up), new Vector3(1, 1, -1));

//     // 使用鏡像的前方和上方向量创建新的鏡像旋转
//     Quaternion mirroredRotation = Quaternion.LookRotation(referenceTransform.TransformDirection(mirroredForward), referenceTransform.TransformDirection(mirroredUp));

//     return mirroredRotation;
// }
Quaternion MirrorRotation(Quaternion sourceRotation, Transform referenceTransform)
{
    // 获取源旋转的前向和上向向量
    Vector3 sourceForward = sourceRotation * Vector3.forward;
    Vector3 sourceUp = sourceRotation * Vector3.up;

    // 将前向和上向向量映射到鏡像方向（在X轴上反转）
    Vector3 mirroredForward = Vector3.Reflect(sourceForward, referenceTransform.right);
    Vector3 mirroredUp = Vector3.Reflect(sourceUp, referenceTransform.right);

    // 创建鏡像旋转
    Quaternion mirroredRotation = Quaternion.LookRotation(mirroredForward, mirroredUp);

    return mirroredRotation;
}
}
