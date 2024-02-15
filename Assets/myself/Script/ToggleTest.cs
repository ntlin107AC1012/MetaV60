using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Samples;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTest : MonoBehaviour
{
    private Toggle m_Toggle;//获取到Toggle组件
    public GameObject targetObject;
    test cup;
    testanchor cuptest;
    public GameObject mirror;
    void Start()
    {
        //找到组件
        m_Toggle = GameObject.Find("Toggle").GetComponent<Toggle>();
        //动态添加监听
        m_Toggle.onValueChanged.AddListener(ToggleOnValueChanged);
        // 获取目标对象上的脚本组件
        cup = targetObject.GetComponent<test>();
        cuptest =targetObject.GetComponent<testanchor>();
    }

	//监听事件
    private void ToggleOnValueChanged(bool isOn)
    {
        if (isOn)
        {
            //cup.PlaceObjectOnTableSurface();
            //mirror.SetActive(true);
            cup.PlaceObjectOnTableSurface();
        }
        else
        {
            Debug.Log("关");
            return;
        }
    }

}
