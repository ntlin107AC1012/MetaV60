using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using System; // 確保引入 MR Utility Kit 的命名空間

public class testball : MonoBehaviour
{   
    private Vector3 originalPosition;
    private Rigidbody rb;
    private Vector3 initialPosition;
    private MRUKAnchor floorAnchor;
    private MRUKRoom currentRoom;

    public TrailRenderer trailrender;
    // Start is called before the first frame update
    void Start()
    {
        // 儲存物件加載時的初始位置
        originalPosition = transform.position;
        // 獲取 Rigidbody 組件
        rb = GetComponent<Rigidbody>();
        
       
    }
    public void Getroom()
    {
         // Get the current room
        currentRoom = MRUK.Instance.GetCurrentRoom();

        // Get the floor anchor
        floorAnchor = currentRoom.GetFloorAnchor();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    // 這個方法可以被呼叫以重設物件位置
    
     public void ResetAndDisableGravity()
    {
        rb.isKinematic = true; // 使 Rigidbody 變為運動學的，暫時忽略重力
        transform.position = originalPosition;
        
    }
     // 當物體被抓取時，恢復正常物理狀態
    public void OnGrabbed()
    {   
        if(rb.isKinematic ==true)
        {
            rb.isKinematic = false; // 恢復 Rigidbody 的動態狀態
        }
        
    }

    void OnCollisionEnter(Collision collision)
{   
    Debug.Log(collision.gameObject.name);
    if (collision.gameObject.name == "FLOOR_EffectMesh")
    {
        Debug.Log("有碰到地板");
        // 重置球的位置
        rb.isKinematic = true; // 使 Rigidbody 變為運動學的，暫時忽略重力
        transform.position = originalPosition;
    }
}
    public void Trailcontroller(Boolean trail)
    {
        if(trail == true)
        {
            trailrender.emitting = true;
        }
        else
        {
            trailrender.emitting = false;   
        }
    }
}
