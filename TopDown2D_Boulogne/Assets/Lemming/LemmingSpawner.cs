using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingSpawner : MonoBehaviour
{

    [SerializeField] GameObject lemmingPrefab;
    [SerializeField] int lemmingNumber;
    [SerializeField] float spawnDuration;

    float spawnDelay;

    float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnDelay = spawnDuration / lemmingNumber;
        spawnTime = spawnDelay;
        StartCoroutine(SpawnLemmings());
    }

    // Update is called once per frame
    void Update()
    {
        //if(Time.time >= spawnTime && lemmingNumber > 0)
        //{
        //    // SPAWN MON LEMMING
        //    Debug.Log("SPAWN");
        //    // ON DECREMENTE LE NOMBRE DE LEMMING A SPAWN
        //    lemmingNumber--;
        //    // REDEFINIR UN NOUVEAU SPAWNTIME
        //    spawnTime = Time.time + spawnDelay;
        //}
    }

    IEnumerator SpawnLemmings()
    {

        for (int i = 0; i < lemmingNumber; i++)
        {
            Instantiate(lemmingPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }

    }
}
