using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Haptics;

public class hapticstest : MonoBehaviour
{   public HapticClip hapticClip; // 指定要使用的Haptic Clip
    private HapticClipPlayer clipPlayerLeft;
    private HapticClipPlayer clipPlayerRight;
    public AudioSource rain_umbrella;

    protected virtual void Start()
    {
        // 為左右手創建Haptic Clip播放器
        clipPlayerLeft = new HapticClipPlayer(hapticClip);
        clipPlayerRight = new HapticClipPlayer(hapticClip);
        clipPlayerRight.isLooping =true;
        clipPlayerLeft.isLooping = true;
    }

    // 當控制器抓取物體時呼叫這個函式來開始播放haptic
    public void StartHapticFeedback(OVRInput.Controller controller)
    {
        if (controller == OVRInput.Controller.LTouch)
        {
            clipPlayerLeft.Play(Controller.Left);
        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            clipPlayerRight.Play(Controller.Right);
        }
    }

    // 當控制器放開物體時呼叫這個函式來停止播放haptic
    public void StopHapticFeedback(OVRInput.Controller controller)
    {
        if (controller == OVRInput.Controller.LTouch)
        {
            clipPlayerLeft.Stop();
        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            clipPlayerRight.Stop();
        }
    }

    protected virtual void OnDestroy()
    {
        clipPlayerLeft.Dispose();
        clipPlayerRight.Dispose();
    }

    protected virtual void OnApplicationQuit()
    {
        Haptics.Instance.Dispose();
    }
    
    public void starthaptic()
    {
        clipPlayerRight.Play(Controller.Right);
        clipPlayerLeft.Play(Controller.Left);
        rain_umbrella.Play();
    }
    public void stophaptic()
    {
        clipPlayerRight.Stop();
        clipPlayerLeft.Stop();
        rain_umbrella.Stop();
    }
}
