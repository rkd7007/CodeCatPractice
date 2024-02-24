using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�̱���
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
        //�̹� �����ϰų� �ڱ� �ڽ��� ����Ű�°� �ƴ϶��
        if (instance != null && instance != this)
        {
            Destroy(gameObject); //����
            return;
        }
        DontDestroyOnLoad(gameObject); //�� �̵��ص� ��������ʰ�
    }

    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
