using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = FindObjectOfType<GameManager>();
            if (instance == null)
                instance = new GameObject("IAPManager").AddComponent<GameManager>();
            return instance;
        }
    }

    void Awake()
    {
        //이미 존재하거나 자기 자신을 가르키는게 아니라면
        if (instance != null && instance != this)
        {
            Destroy(gameObject); //삭제
            return;
        }
        DontDestroyOnLoad(gameObject); //씬 이동해도 사라지지않게
    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
