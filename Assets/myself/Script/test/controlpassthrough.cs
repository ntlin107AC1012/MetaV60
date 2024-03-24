using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class controlpassthrough : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public GameObject cubePrefab; // 引用 Cube 预制体
    private GameObject currentCube; // 存储当前 Cube 的引用

    public GameObject hoopPrefab;
    private GameObject currenthoop;
    public GameObject ball;
    private bool checkbasketball = false;
    private bool control_passthrough = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //closepassthrough();
        if(control_passthrough == true)
        {
            Vector3 rayOrigin = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Vector3 rayDirection = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
            RaycastHit hit = new RaycastHit();
            MRUK.Instance?.GetCurrentRoom()?.Raycast(new Ray(rayOrigin, rayDirection), Mathf.Infinity, out hit, out MRUKAnchor anchorHit);
            closepassthrough(hit);
        }
        if(checkbasketball == true)
        {
            Vector3 rayOrigin = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Vector3 rayDirection = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
            RaycastHit hit = new RaycastHit();
            MRUK.Instance?.GetCurrentRoom()?.Raycast(new Ray(rayOrigin, rayDirection), Mathf.Infinity, out hit, out MRUKAnchor anchorHit);
            ShowHitNormal(hit);
        }                    
    }
//    private void closepassthrough()
// {
//     Vector3 rayOrigin = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
//     Vector3 rayDirection = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
//     RaycastHit hit;

//     // 假设 MRUK.Instance.GetCurrentRoom().Raycast() 类似于 Unity 的 Physics.Raycast()
//     if (MRUK.Instance?.GetCurrentRoom()?.Raycast(new Ray(rayOrigin, rayDirection), Mathf.Infinity, out hit) == true)
//     {
//         if (currentCube == null)
//         {
//             currentCube = Instantiate(cubePrefab, hit.point, Quaternion.identity);
//         }
//         else
//         {
//             currentCube.transform.position = hit.point;
//         }

//         if (OVRInput.GetDown(OVRInput.Button.One))
//         {
//             // 尝试获取这个物体上的 Mesh Renderer 组件
//             GameObject hitObject = hit.collider.gameObject;
//             MeshRenderer meshRenderer = hitObject.GetComponent<MeshRenderer>();

//             // 如果找到了 Mesh Renderer 组件，则关闭它
//             if (meshRenderer != null)
//             {
//                 meshRenderer.enabled = false;
//             }
//         }
//     }
// }
private void closepassthrough(RaycastHit hit)
{
    

   
        // 确保确实有 Collider 被击中
        if (hit.point != Vector3.zero)
        {   
            Debug.Log("not zero");
            if (currentCube == null)
            {
                // 如果不存在，则实例化一个新的 Cube
                currentCube = Instantiate(cubePrefab, hit.point, Quaternion.identity);
                
            }
            else
            {   
                currentCube.transform.position = hit.point;
                Vector3 rayOrigin = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                Vector3 rayDirection = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
                RaycastHit hit2;

                if (Physics.Raycast(new Ray(rayOrigin, rayDirection), out hit2, Mathf.Infinity))
                {
                    // 确保确实有 Collider 被击中
                    if (hit2.collider != null)
                    {
                        // 安全地获取射线击中的物体
                        GameObject hitObject = hit2.collider.gameObject;
                        Debug.Log("Hit object: " + hitObject.name);

                        // 如果需要，以下是关闭 Mesh Renderer 的代码
                        MeshRenderer meshRenderer = hitObject.GetComponent<MeshRenderer>();
                        Debug.Log(meshRenderer);
                        if (OVRInput.GetDown(OVRInput.Button.One))
                        {
                            if (meshRenderer != null)
                            {
                                // 切换 Mesh Renderer 的启用/禁用状态
                                meshRenderer.enabled = !meshRenderer.enabled;
                            }
                        }
                    }
                
                //}
                }
            }
            // 安全地获取射线击中的物体
            
        }
    
}


    void ShowHitNormal(RaycastHit hit)
    {   
            Debug.Log("yes");
            if (hit.point != Vector3.zero) // 检查是否确实有碰撞点
            {
                if (currenthoop == null)
            {
                // 如果不存在，则实例化一个新的 Cube
                currenthoop = Instantiate(hoopPrefab, hit.point, Quaternion.identity);
                
            }
            else
            {
                // // 如果已存在，更新 Cube 的位置到碰撞点
                // currentCube.transform.position = hit.point;
                // // 可选：根据碰撞法线调整 Cube 的旋转
                // currentCube.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                // 更新篮球框的位置到碰撞点
                currenthoop.transform.position = hit.point;
                // 计算篮球框面向玩家的方向
                Vector3 directionToPlayer = Camera.main.transform.position - hit.point;
                directionToPlayer.y = 0; // 通常情况下，您可能希望忽略垂直方向的差异，保持篮球框水平
                
                // 设置篮球框的旋转，使其面向玩家
                Quaternion lookAtPlayerRotation = Quaternion.LookRotation(directionToPlayer);
                currenthoop.transform.rotation = lookAtPlayerRotation;
                if(OVRInput.GetDown(OVRInput.Button.One))
                {
                    Vector3 controllerPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                    Quaternion controllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch); // 获取右手控制器的旋转
                    Instantiate(hoopPrefab, hit.point,  currenthoop.transform.rotation);
                    Instantiate(ball, controllerPos, controllerRotation);
                    checkbasketball = false;
                }
            }
            }
        
    }
    public void basketballstart() 
    {
        checkbasketball = !checkbasketball;
    }
    public void passthroughstart() 
    {
        control_passthrough = !control_passthrough;
    }
}
