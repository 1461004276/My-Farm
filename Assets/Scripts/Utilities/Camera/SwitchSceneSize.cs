using Cinemachine;
using UnityEngine;

public class SwitchSceneSize : MonoBehaviour
{
    // todo ： 切换场景时切换该方法调用
    private void Start()
    {
        SwichConfinerShape();
    }

    private void SwichConfinerShape()
    {
        PolygonCollider2D polyCollider =
            GameObject.FindGameObjectWithTag("SizeConfiner").GetComponent<PolygonCollider2D>();
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = polyCollider;
        // 在切换场景时要清除缓存
        confiner.InvalidatePathCache();
    }
    
}
