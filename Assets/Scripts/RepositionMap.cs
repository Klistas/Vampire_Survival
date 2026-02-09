using UnityEngine;

public class RepositionMap : MonoBehaviour
{
    // 플레이어의 위치를 측정.
    // 측정 결과에 따라서 맵을 재배치 할 예정입니다.
    public float MapSize; // 현재 맵 사이즈에 따라 어느정도 이동했을때 플레이어 이동방향으로 재배치
    public Transform Player; // 플레이어의 위치를 알아야 하기 때문에.

    void Update()
    {
        MapReposition();
    }

    /// <summary>
    /// 플레이어의 위치를 확인하여 맵의 위치를 이동방향으로 재조정.
    /// </summary>
    private void MapReposition()
    {
        // 플레이어와 맵 간의 거리 계산, 플레이어와 맵의 x값, y값을 각각 계산해서. 밑에서 사용할 예정.
        float diffX = Player.position.x - transform.position.x; // 플레이어의 위치와 맵의 위치 간 X 차이.
        float diffY = Player.position.y - transform.position.y; // 플레이어의 위치와 맵의 위치 간 Y 차이.

        // 어디로 다시 위치할지 알아야한다 == 플레이어의 이동방향을 알아야 하므로, 플레이어의 이동방향 계산.
        Vector3 playerDirect = Player.position - transform.position; // Vector3 형식의 두 위치 간의 차이. X 값이 양수 == 오른쪽/Y값이 양수 == 위쪽
        
        // X축 계산 및 위치조정
        if (Mathf.Abs(diffX) > MapSize) // 어느 방향으로 이동하든, 맵 사이즈만큼 움직이면 동작함.
        {
            if(playerDirect.x < 0) // 진행 방향이 음수(왼쪽)일 때
            {
                transform.Translate(Vector3.left * MapSize * 2);
            }
            else if(playerDirect.x > 0) // 진행 방향이 양수(오른쪽)일 때
            {
                transform.Translate(Vector3.right * MapSize * 2);
            }
        }
        // Y축 계산 및 위치조정
        if (Mathf.Abs(diffY) > MapSize) // 어느 방향으로 이동하든, 맵 사이즈만큼 움직이면 동작함.
        {
            if (playerDirect.y < 0) // 진행 방향이 음수(왼쪽)일 때
            {
                transform.Translate(Vector3.down * MapSize * 2);
            }
            else if (playerDirect.y > 0) // 진행 방향이 양수(오른쪽)일 때
            {
                transform.Translate(Vector3.up * MapSize * 2);
            }
        }
    }
}
