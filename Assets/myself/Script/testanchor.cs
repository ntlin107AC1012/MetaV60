using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using Meta.XR.MRUtilityKit;
using Oculus.Interaction.Input;
using Unity.Collections;
using UnityEngine;
namespace Oculus.Interaction.Samples
{
public class testanchor : MonoBehaviour
{
    // Start is called before the first frame update
    

    public GameObject objectToPlace; // 您想要放置的对象
    public GameObject Menu;
    float minRadius = 0.0f;
    [SerializeField, Interface(typeof(IHmd))]
    private UnityEngine.Object _hmd;
    private IHmd Hmd { get; set; }
    [SerializeField]
        private ActiveStateSelector[] _poses;

    //public MRUKAnchor.SceneLabels Labels = ~(MRUKAnchor.SceneLabels)0;
    [SerializeField]
    private float heightOffset = 0.1f; // 增加的高度

    [SerializeField]
    private float backwardOffset = 0.1f; // 向后的偏移
    [SerializeField]
    private AudioSource _showMenuAudio;

    [SerializeField]
    private AudioSource _hideMenuAudio;
    // 公共方法，用于放置对象
    public GameObject canvasObject; // 您想要显示的 Canvas
    
    public Hand hand; // 您需要提供对 Hand 类的引用
    public AudioSource[] audioSource;
public HandRef rightHandRef; // 右手引用，需要在编辑器中设置
public Vector3 offsetFromHand = new Vector3(0.1f, 0.1f, 0.1f); // 距离手部的偏移量，根据需要进行调整
public float distanceInFrontOfUser = 0.5f;
public Camera testcam;
    protected virtual void Awake()
        {
            Hmd = _hmd as IHmd;
        }
    void start()
    {
        
        this.AssertField(Hmd, nameof(Hmd));
    }
    public void PlaceObjectOnTableSurface()
    {
        // 获取当前房间
        MRUKRoom currentRoom = MRUK.Instance.GetCurrentRoom();
        List<MRUKAnchor> table = currentRoom.GetRoomAnchors();
        var roomAnchors = currentRoom.GetRoomAnchors();
        foreach (var anchor in roomAnchors)
        {
            // 檢查錨點是否標記為桌子
            if (anchor.HasLabel("TABLE"))
            {   
                //關鍵假設是 GetBoundsFaceCenters() 方法能夠返回所有面的中心點。
                var faceCenters = anchor.GetBoundsFaceCenters();
                // 簡單假設：最高點的面中心是桌面中心
                Vector3 tableTopCenter = Vector3.zero;
                float highestY = float.MinValue;
                foreach (var center in faceCenters)
                {
                    if (center.y > highestY)
                    {
                        highestY = center.y;
                        tableTopCenter = center;
                    }
                }
                 GameObject spawnedObject = Instantiate(objectToPlace);
                
                // 將物件放置在桌子的中心
                spawnedObject.transform.position = tableTopCenter;

                // 如果需要，您可以在此處調整物件的旋轉
                spawnedObject.transform.rotation = Quaternion.identity; // 或其他需要的旋轉

                Debug.Log("找到桌面的中心: " + tableTopCenter);
                
                // 執行您想要的操作，例如放置物件等
                // ...

                break; // 假設只需要找到一個桌子
            }
        }
        Debug.Log(table);
        //MRUKAnchor TABLE =currentRoom.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP,minRadius,LabelFilter.FromEnum(Labels),out Vector3 position,out Vector3 normal);
        // 找到最大的桌子表面
        MRUKAnchor largestTableSurface = currentRoom.FindLargestSurface("TABLE");

        // if (largestTableSurface != null && objectToPlace != null)
        // {
        //     // 获取桌子表面的中心点
        //     Vector3 tableCenter = largestTableSurface.GetAnchorCenter();

        //     GameObject spawnedObject = Instantiate(objectToPlace);
        //     // 将对象放置在桌子的中心
        //     spawnedObject.transform.position = tableCenter;

        //     // 如果需要，您可以在此处调整对象的旋转
        //     spawnedObject.transform.rotation = Quaternion.identity; // 或其他需要的旋转
        // }
        // else
        // {
        //     Debug.LogError("Table surface or object to place is missing.");
        // }
    }
    public void ObjectMenu()
    {
        MRUKRoom currentRoom = MRUK.Instance.GetCurrentRoom();
       
        
        //MRUKAnchor TABLE =currentRoom.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP,minRadius,LabelFilter.FromEnum(Labels),out Vector3 position,out Vector3 normal);
        // 找到最大的桌子表面
        MRUKAnchor largestTableSurface = currentRoom.FindLargestSurface("TABLE");

        if (largestTableSurface != null && Menu.active!= true)
        {
            // 获取桌子表面的中心点
            _showMenuAudio.Play();
            Vector3 tableCenter = largestTableSurface.GetAnchorCenter();
            Vector3 offsetPosition = tableCenter + Vector3.up * heightOffset;
            offsetPosition = offsetPosition += Vector3.forward * 0.25f;
            Menu.SetActive(true);
            // 将对象放置在桌子的中心
            Menu.transform.position = offsetPosition;
            // 使 UI 面向玩家
            FaceTowardsPlayer(Menu);
            // 如果需要，您可以在此处调整对象的旋转
           // Menu.transform.rotation = Quaternion.identity; // 或其他需要的旋转
        }
        else
        {   
            _hideMenuAudio.Play();
            Menu.SetActive(false);
            //Debug.LogError("Table surface or object to place is missing.");
        }
    }
    private void FaceTowardsPlayer(GameObject menu)
    {
    // 假设 Camera.main 是玩家的视角
    Transform playerTransform = Camera.main.transform;

    // 计算面向玩家的方向
    Vector3 directionToFace = playerTransform.position - menu.transform.position;

    // 确保不改变菜单的垂直方向
    directionToFace.y = 0;

    // 设置菜单的旋转
    menu.transform.rotation = Quaternion.LookRotation(directionToFace);
    }   
    // 调用这个函数来显示 Canvas 在手的附近
    public void ShowCanvasNearHand()
    {
        if (canvasObject.gameObject.activeSelf)
        {
            // 如果当前是激活状态，隐藏它
            audioSource[1].Play();
            canvasObject.gameObject.SetActive(false);
            return; // 退出函数
        }
    // 获取手的当前位置和旋转
        if (!Hmd.TryGetRootPose(out Pose hmdPose))
            {
                return;
            }
            Vector3 spawnSpot = hmdPose.position + hmdPose.forward;
            canvasObject.transform.position = spawnSpot;
            canvasObject.transform.LookAt(2 * canvasObject.transform.position - hmdPose.position);

            var hands = _poses[0].GetComponents<HandRef>();
            Vector3 visualsPos = Vector3.zero;
            foreach (var hand in hands)
            {
                hand.GetRootPose(out Pose wristPose);
                Vector3 backward = -wristPose.forward; // 假设 forward 指向手掌方向，则使用 -forward 作为向后方向
                Vector3 right = -wristPose.right;
                Vector3 forward = hand.Handedness == Handedness.Left ? wristPose.right : -wristPose.right;
                visualsPos += wristPose.position + backward * .1f + Vector3.up * .1f +Vector3.forward*.5f;// 增加这里的偏移量
                //visualsPos += wristPose.position + forward * .2f + Vector3.up * .02f;
            }
             canvasObject.transform.position = visualsPos / hands.Length;
             canvasObject.gameObject.SetActive(true);
             audioSource[0].Play();
    }
    public void showui()
    {
       // 获取用户头盔的当前位置和旋转
        Camera mainCamera = testcam; // 假设主摄像机是用户的头盔
        if (mainCamera == null) return; // 确保摄像机存在
        // 在用户前方 distanceInFrontOfUser 的位置计算 Canvas 应该出现的点
        Vector3 canvasPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceInFrontOfUser;

        // 激活 Canvas 对象并设置其位置
        canvasObject.SetActive(true);
        canvasObject.transform.position = canvasPosition;
        Vector3 toUser = mainCamera.transform.position - canvasPosition;
        Quaternion canvasRotation = Quaternion.LookRotation(toUser);
      //  canvasObject.transform.rotation = Quaternion.Euler(0, canvasRotation.eulerAngles.y, 0);

// 设置 Canvas 旋转以面向用户
// 计算从 Canvas 到用户的反方向，确保 UI
    }
    }

}
