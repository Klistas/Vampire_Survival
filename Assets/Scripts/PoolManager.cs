using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 리스트로 특정 게임오브젝트들의 풀(풀장,오브젝트를 꺼내서 쓸수있는)을 만든다.
    public static PoolManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject[] prefabs; // 사용할 오브젝트의 프리팹

    private List<GameObject>[] pools; // 게임오브젝트(우리가 사용할 오브젝트 ex. 적, 투사체)를 넣어둘 보관함들.

    private void Start()
    {
        // 우리가 만들 프리팹 개수만큼 리스트를 만들어야 한다.
        pools = new List<GameObject>[prefabs.Length];

        // 프리팹의 개수만큼
        for(int i = 0; i < prefabs.Length; i++)
        {
            // 리스트 초기화
            pools[i] = new List<GameObject>();
        }
    }

    /// <summary>
    /// 풀을 확인해서 만약 사용중이지 않은 오브젝트가 있으면, 해당 오브젝트를 꺼내오고, 없으면 생성
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public GameObject Get(int index,Vector3 pos)
    {
        GameObject select = null;

        foreach(GameObject obj in pools[index])
        {
            if(obj.activeSelf == false)
            {
                // 리스트 내부에서 비활성화된 오브젝트가져와서
                select = obj;
                // 위치 설정
                select.transform.position = pos;
                // 활성화 시킴
                select.SetActive(true);
                return select;
            }
        }
        if(select == null)
        {
            //없을 때 생성되는 로직부터 작성
            // 게임오브젝트를 생성
            select = Instantiate(prefabs[index], transform);
            //위치 설정
            select.transform.position = pos;
            // 우리가 넣어야할 리스트에 넣어줌
            pools[index].Add(select);
            return select;
        }
        return select;
    }

    /// <summary>
    /// 이미 사용한 오브젝트를 다시 반환하는 오브젝트
    /// </summary>
    /// <param name="index"></param>
    public void Return(GameObject obj,int index)
    {
        // 우리가 반환할 풀에 Add해줌.
        pools[index].Add(obj);
        // 비활성화
        obj.SetActive(false);
    }
}
