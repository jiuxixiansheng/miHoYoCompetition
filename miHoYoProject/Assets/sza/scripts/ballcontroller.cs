using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // 判断碰到的是否是玩家（根据Tag区分）
        if (other.CompareTag("player"))
        {
            EventDispatcher.Instance.Dispatch("getball");
            // 或者直接销毁
            Destroy(gameObject);
        }
    }
}
