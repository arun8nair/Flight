using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject[] obstaclePrefabsList;
    public GameObject[] birdsList;
    private int noOfSpawnPoints = 9;
    private float birdFlockAngle = 60;  //angle formed at the tip of the bird flock

    void Start()
    {
        // choose a random spawn point from the set of points for each prefab
        int spawnPointNumber = Random.Range(0, noOfSpawnPoints);
        Transform spawnPoint = transform.GetChild(spawnPointNumber);

        int obstacleNumber = Random.Range(0, obstaclePrefabsList.Length);
        GameObject obstaclePrefab = obstaclePrefabsList[obstacleNumber];
        if (obstaclePrefab.name == "Low_building") {
            spawnPoint.position = new Vector3(spawnPoint.position.x, 0, spawnPoint.position.z);
        }

        //spawn an obstacle at that point
        var obstacle = (GameObject) Instantiate(obstaclePrefab, spawnPoint.position, obstaclePrefab.transform.rotation);
        obstacle.transform.SetParent(transform);
        if(obstacle.CompareTag("Flock"))
        {
            GenerateFlockOfBirds(obstacle);
        }
    }

    void GenerateFlockOfBirds(GameObject flock)
    {
        // find the two lines of the flock to place birds on them
        Vector3 flockLine1 = Quaternion.Euler(0, 90 + birdFlockAngle, 0) * Vector3.forward;
        Vector3 flockLine2 = Quaternion.Euler(0, 270 - birdFlockAngle, 0) * Vector3.forward;

        int noOfBirds = Random.Range(5, 8);
        int flockRow = 0;
        float birdPosOffset;

        for (int i = 0; i < noOfBirds; i++)
        {
            Vector3 flockLine = i % 2 == 0 ? flockLine1 : flockLine2;
            birdPosOffset = Random.Range(-0.5f, 0.5f);
            Vector3 birdPos = new Vector3(flockLine.x * flockRow, flockLine.y + birdPosOffset, flockLine.z * flockRow * 0.5f) + flock.transform.position;
            int birdType = Random.Range(0, birdsList.Length);
            var bird = Instantiate(birdsList[birdType], birdPos, flock.transform.rotation);
            bird.transform.SetParent(flock.transform);
            flockRow = i % 2 == 0 ? flockRow + 1 : flockRow;
        }
    }
}
