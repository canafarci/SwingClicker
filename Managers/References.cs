using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{
    public static References Instance => s_Instance;
    static References s_Instance;
    public GameConfig GameConfig;
    public List<Gift> Gifts = new List<Gift>();
    void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_Instance = this;
    }
}
