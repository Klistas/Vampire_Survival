using UnityEngine;

public class Gems : MonoBehaviour
{
    // 감지 범위
    public float GemsRange;
    // 플레이어에게 날아오는 속도
    public float GemsSpeed;
    // 경험치 양
    public float GemsExp;

    // 플레이어
    private GameObject player;


    // 시작할때 플레이어의 위치를 확인함.
    private void Start()
    {
        // 플레이어의 위치는 생성되고 태그로 찾음.
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // 해당 위치가 특정 범위 이내라고하면 흡수되도록 한다.
    private void Update()
    {
        // 나(경험치 아이템)와 플레이어간의 거리를 계속 계산한다.
        float distance = Vector2.Distance(transform.position, player.transform.position);

        // 그 거리가 우리가 미리 정한 범위 이내라면
        if (distance < GemsRange)
        {
            //플레이어의 위치로 빨려들어가도록 함.
            transform.position = Vector2.MoveTowards(transform.position,player.transform.position,GemsSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 물체가 플레이어 라면
        if (collision.CompareTag("Player"))
        {
            // 경험치를 올려주고
            Debug.Log("경험치 상승");
            // 나 자신 삭제
            Destroy(gameObject);
        }
    }
}
