using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance;

    private Checkpoint[] checkPoints;
    //Mảng lưu những CheckPoint có trên bảng đồ

    public Vector3 spawnPoint;
    //Lưu điểm hồi sinh

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        checkPoints = FindObjectsOfType<Checkpoint>();
        //Tìm tất cả Checkpoint hiện có trong màn chơi

        spawnPoint = PlayerController.instance.transform.position;
        //Điểm hồi sinh đầu tiên nếu chưa có Checkpoint
    }

    void Update()
    {
        
    }

    public void DeactiveCheckPoints()
        //Hàm Reset Checkpoint
    {
        for(int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].ResetCheckPoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
        //Hàm vị trí hồi sinh
    {
        spawnPoint = newSpawnPoint;
    }
}
