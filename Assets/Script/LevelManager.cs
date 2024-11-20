using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string monsterTag = "Monster"; // 몬스터 태그 설정
    public Transform[] spawnPoints; // 몬스터가 스폰될 위치들
    public GameObject monsterPrefab; // 스폰할 몬스터 프리팹
    public int baseMonsterCount = 5; // 기본적으로 스폰될 몬스터 수
    private int currentFloor = 1; // 현재 층수

    void Start()
    {
        SpawnMonsters(); // 게임 시작 시 첫 번째 층의 몬스터 스폰
    }

    void Update()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);

        if (monsters.Length == 0)
        {
            LoadNextLevel(); // 모든 몬스터가 제거되면 다음 층으로 이동
        }
    }

    void LoadNextLevel()
    {
        currentFloor++; // 층수 증가
        Debug.Log("다음 층으로 이동! 현재 층수: " + currentFloor);

        SpawnMonsters(); // 새로운 층에 맞는 몬스터 스폰
    }

    void SpawnMonsters()
    {
        int monsterCount = baseMonsterCount + (currentFloor - 1) * 2; // 기본 몬스터 수 + 층수에 따른 추가

        for (int i = 0; i < monsterCount; i++)
        {
            // 랜덤한 위치에서 몬스터 스폰
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
        }

        Debug.Log("몬스터 " + monsterCount + "마리 스폰 완료!");
    }
}