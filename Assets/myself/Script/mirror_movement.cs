using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class mirror_movement : MonoBehaviour
{
    public float Pos_x, Pos_y, Pos_z;
    public float Rot_x, Rot_y, Rot_z;
    public GameObject _object;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      
                Pos_x = _object.GetComponent<Get_position>().mirrorPos_x;
                Pos_y = _object.GetComponent<Get_position>().mirrorPos_y;
                Pos_z = _object.GetComponent<Get_position>().mirrorPos_z;
                Vector3 objposition = new Vector3(Pos_x , Pos_y, Pos_z );
                transform.position = objposition;
                
                Rot_x = _object.transform.localEulerAngles.x;
                Rot_y = _object.transform.localEulerAngles.y;
                Rot_z = _object.transform.localEulerAngles.z;
                Quaternion result = Quaternion.Euler(Rot_x, -Rot_y, -Rot_z );
                transform.rotation = result;
            
        


        
        
    }
}
