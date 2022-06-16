using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_meteor : MonoBehaviour
{
    public GameObject MeteorPrefab;
    public float respawnTime = 0.5f;
    //private Vector2 screenBounds;
    public float spawnDistance = 10;
    public Transform planet = null;

    // Start is called before the first frame update
    void Start()
    {
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(meteorWave());
    }

    private void spawnMeteor()
    {
        GameObject a = Instantiate(MeteorPrefab) as GameObject;
        //a.transform.position = new Vector2(screenBounds.x * -2, Random.Range(-screenBounds.y, screenBounds.y));
        float angle = Random.Range(0, 360);
        Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
        Vector3 distance = direction * spawnDistance;
        Vector3 newPosition = planet.position + distance;
        a.transform.position = newPosition;
    }

    IEnumerator meteorWave()
    {
        while (true) {
            yield return new WaitForSeconds(respawnTime);
        spawnMeteor();
        }
    }


}
