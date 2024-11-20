using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 스폰할 몬스터 프리팹
    public float spawnInterval = 5f; // 몬스터 스폰 간격
    public int monsterCount = 5; // 스폰할 몬스터 수

    private int spawnedMonsters = 0;

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (spawnedMonsters < monsterCount)
        {
            Instantiate(monsterPrefab, transform.position, Quaternion.identity); // 몬스터 생성
            spawnedMonsters++;
            yield return new WaitForSeconds(spawnInterval); // 일정 시간 대기 후 다시 스폰
        }
    }
}