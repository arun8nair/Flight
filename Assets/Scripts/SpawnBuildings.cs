using UnityEngine;

public class SpawnBuildings : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    private float trackLength;

    void Start()
    {
        Transform track = GetComponent<Transform>();
        trackLength = GetComponent<MeshRenderer>().bounds.size.z;
        //Debug.Log("Lngth" + trackLength);

        Vector3[] spawnPoints = new Vector3[2];

        //spawnPoints[0] = new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z - trackLength * 0.25f);
        //spawnPoints[1] = new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z + trackLength * 0.25f);
        //spawnPoints[2] = new Vector3(transform.position.x + 10f, transform.position.y, transform.position.z - trackLength * 0.75f);
        //spawnPoints[3] = new Vector3(transform.position.x + 10f, transform.position.y, transform.position.z + trackLength * 0.75f);
        spawnPoints[0] = new Vector3(transform.position.x + 10f, transform.position.y, transform.position.z);
        spawnPoints[1] = new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z);

        int j;
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            int buildingType = Random.Range(0, buildingPrefabs.Length);
            j = i <= 1 ? -1 : 1;
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + j * 90, transform.rotation.z);
            var building = (GameObject)Instantiate(buildingPrefabs[buildingType], spawnPoints[i], rotation);
            building.transform.SetParent(transform);
        }
    }
}
