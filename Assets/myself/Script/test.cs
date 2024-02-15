using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using Oculus.Interaction.Input;
using Unity.Collections;
using UnityEngine;
public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void test1 () {
        Debug.Log("1");
    }
    public void test2 () {
        Debug.Log("2");
    }
    public GameObject[] objectToPlace;
    public void PlaceObjectOnTableSurface()
    {
        // 获取当前房间
       // MRUKRoom currentRoom = MRUK.Instance.GetCurrentRoom();
       
        
        //MRUKAnchor TABLE =currentRoom.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP,minRadius,LabelFilter.FromEnum(Labels),out Vector3 position,out Vector3 normal);
        // 找到最大的桌子表面
       // MRUKAnchor largestTableSurface = currentRoom.FindLargestSurface("TABLE");

        // if (largestTableSurface != null && objectToPlace != null)
        // {
        //     // 获取桌子表面的中心点
        //     Vector3 tableCenter = largestTableSurface.GetAnchorCenter();

        //     objectToPlace[0].SetActive(true);
        //     objectToPlace[1].SetActive(true);
        //     // 将对象放置在桌子的中心
        //     objectToPlace[0].transform.position = tableCenter;

        //     // 如果需要，您可以在此处调整对象的旋转
        //     objectToPlace[0].transform.rotation = Quaternion.identity; // 或其他需要的旋转
            
        // }
        // else
        // {
        //     Debug.LogError("Table surface or object to place is missing.");
        // }
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
                // GameObject spawnedObject = Instantiate(objectToPlace);
                       objectToPlace[0].SetActive(true);
                        objectToPlace[1].SetActive(true);
                // 將物件放置在桌子的中心
                objectToPlace[0].transform.position = tableTopCenter;

                // 如果需要，您可以在此處調整物件的旋轉
                objectToPlace[0].transform.rotation = Quaternion.identity; // 或其他需要的旋轉

                Debug.Log("找到桌面的中心: " + tableTopCenter);
                
                // 執行您想要的操作，例如放置物件等
                // ...

                break; // 假設只需要找到一個桌子
            }
        }
        Debug.Log(table);
        //MRUKAnchor TABLE =currentRoom.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP,minRadius,LabelFilter.FromEnum(Labels),out Vector3 position,out Vector3 normal);
        // 找到最大的桌子表面
       // MRUKAnchor largestTableSurface = currentRoom.FindLargestSurface("TABLE");

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
}
