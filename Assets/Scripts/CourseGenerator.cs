using System.Collections;
using UnityEngine;

public class CourseGenerator : MonoBehaviour
{
    public GameObject trackPrefab;
    public float courseSpeed = 15.0f;
    public float speedChangeAmount = 3f;
    public float maxSpeed = 50.0f;
    private Vector3 spawnPos = Vector3.zero;
    private float trackPieceLength;
    private int initialPathLength = 20;
    private float startDelay = 5.0f;
    private float speedChangeInterval = 5.0f;

    void Start()
    {
        GenerateInitialPath();
        StartCoroutine("GenerateCourse");
        StartCoroutine("IncreaseCourseSpeed");
    }

    void GenerateInitialPath()
    {
        GameObject trackPiece = (GameObject) Instantiate(trackPrefab, spawnPos, trackPrefab.transform.rotation);
        Destroy(trackPiece.GetComponent<SpawnObstacle>());      // starting track pieces do not spawn obstacles
        trackPieceLength = trackPiece.GetComponent<MeshCollider>().bounds.size.z;
        spawnPos = new Vector3(0, 0, trackPieceLength * initialPathLength);
        for(int i = 1; i < initialPathLength; i++)      // create initial track pieces following the first one
        {
            var startingTrack = Instantiate(trackPrefab, new Vector3(0, 0, trackPieceLength * i), trackPrefab.transform.rotation);
            Destroy(startingTrack.GetComponent<SpawnObstacle>());
        }
    }

    IEnumerator IncreaseCourseSpeed()
    {
        yield return new WaitForSeconds(startDelay);
        while(true)
        {
            if(courseSpeed <= maxSpeed)
            {
                courseSpeed += speedChangeAmount;
                yield return new WaitForSeconds(speedChangeInterval);
            } else
            {
                StopCoroutine(IncreaseCourseSpeed());
                break;
            }
        }
    }

    IEnumerator GenerateCourse()
    {
        while (true)
        {
            GameObject newTrackPiece = Instantiate(trackPrefab, spawnPos, trackPrefab.transform.rotation);
            yield return new WaitUntil(() => TrackHasPassed(newTrackPiece.transform));
        }
    }

    bool TrackHasPassed(Transform newTrackPiece)
    {
        if(Mathf.Round(newTrackPiece.position.z + trackPieceLength)  <= spawnPos.z)
        {
            return true;
        }
        return false;
    }

    public void StopGenerating()
    {
        StopAllCoroutines();
    }
}
