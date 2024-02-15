using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Get_position : MonoBehaviour
{
    public float Pos_x, mirrorPos_x, Pos_y, mirrorPos_y, Pos_z, mirrorPos_z;
    public float Rot_x, mirrorRot_x, Rot_y, mirrorRot_y, Rot_z, mirrorRot_z;
    public GameObject _object, mirror_object;


    


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        
                Pos_x = _object.GetComponent<Transform>().position.x;
                Pos_y = _object.GetComponent<Transform>().position.y;
                Pos_z = _object.GetComponent<Transform>().position.z;



                mirrorPos_x = -Pos_x;
                mirrorPos_y = Pos_y;
                mirrorPos_z = Pos_z;
                Rot_x = _object.GetComponent<Transform>().rotation.x;
                Rot_y = _object.GetComponent<Transform>().rotation.y;
                Rot_z = _object.GetComponent<Transform>().rotation.z;
                //mirrorRot_x = Rot_x;
                //mirrorRot_y = Rot_y;
                //mirrorRot_z = -Rot_z;
            
        }

    }
