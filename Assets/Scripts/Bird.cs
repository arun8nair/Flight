using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    GameObject[] featherEmitters = new GameObject[3];
    GameObject fEmitter;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Flying", 0, Random.value);
        fEmitter = Resources.Load("featherEmitter", typeof(GameObject)) as GameObject;
        GenerateFeathers();
    }

    void GenerateFeathers()
    {
        for (int i = 0; i < 3; i++)
        {
            featherEmitters[i] = Instantiate(fEmitter, Vector3.zero, Quaternion.identity) as GameObject;
            featherEmitters[i].transform.parent = transform;
            featherEmitters[i].SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die(collision.gameObject.transform.position);
            StartCoroutine("RemoveBirdBody");
        }
    }

    void Die(Vector3 pos)
    {
        Rigidbody birdRb = GetComponent<Rigidbody>();
        birdRb.isKinematic = false;
        birdRb.useGravity = true;
        birdRb.AddForce(
             pos - new Vector3(
                pos.x - Random.Range(-2, 2),
                pos.y - 2,
                pos.z + Random.Range(50, 70)
                )
            );
        birdRb.AddTorque(
            pos - new Vector3(
                pos.x - Random.Range(-2, 2),
                pos.y - 2,
                pos.z + Random.Range(50, 70)
                )
            ); ;
        anim.SetTrigger("die");
        foreach (GameObject fEmit in featherEmitters)
        {
            if (!fEmit.activeSelf)
            {
                fEmit.transform.position = transform.position ;
                fEmit.SetActive(true);
                StartCoroutine("DeactivateFeathers", fEmit);
                break;
            }
        }
    }

    IEnumerator RemoveBirdBody()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    IEnumerator DeactivateFeathers(GameObject featherEmit)
    {
        yield return new WaitForSeconds(2.0f);
        featherEmit.SetActive(false);
    }
}
