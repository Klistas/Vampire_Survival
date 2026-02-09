using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("타일 관련")]
    public Tilemap ObstacleTilemap; // 장애물이 배치될 타일맵
    public TileBase[] ObstacleTiles; // 장애물이 될 타일들

    [Header("설정값")]
    public int GenerateRadius; // 장애물 생성 범위
    [Range(1,100)]public int SpawnRate; // 장애물 생성 확률

    private void Start()
    {
        // 최초 생성
        GenerateObstacle();
        Debug.Log("생성");
    }
    /// <summary>
    /// 장애물을 초기화하고 생성하는 함수
    /// </summary>
    public void GenerateObstacle()
    {
        // 기존에 만약 타일이 있었다면 전부 제거
        ObstacleTilemap.ClearAllTiles();

        // GenerateRadius x 2 만큼 y축 순회
        for(int y = -GenerateRadius; y<= GenerateRadius; y++)
        {
            // GenerateRadius x 2 만큼 x축 순회
            for (int x = -GenerateRadius; x <= GenerateRadius; x++)
            {
                // 정해진 확률보다 값이 적을 때 생성
                if(Random.Range(0,100) < SpawnRate)
                {
                    // 정해진 타일들 중에 랜덤 생성
                    TileBase tile = ObstacleTiles[Random.Range(0, ObstacleTiles.Length)];

                    // 현재 위치 확인 및 생성
                    Vector3Int pos = new Vector3Int(x, y);
                    ObstacleTilemap.SetTile(pos, tile);
                }
            }
        }
    }
}
