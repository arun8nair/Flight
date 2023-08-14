using UnityEngine;

public class MoveBack : MonoBehaviour
{
    public GameObject player;
    public int clearDistance = 15;  // distance from player at which to destroy the object
    private GameManager gameManager;
    private CourseGenerator courseGenerator;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        courseGenerator = gameManager.GetComponent<CourseGenerator>();
    }
    void Update()
    {
        float speed = courseGenerator.courseSpeed;
        if(gameManager.isGameActive)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            if(transform.position.z < player.transform.position.z - clearDistance && gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
