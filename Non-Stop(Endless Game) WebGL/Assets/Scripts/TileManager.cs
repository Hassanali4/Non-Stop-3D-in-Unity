// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for managing tiles in the game
public class TileManager : MonoBehaviour
{
    // Array of tile prefabs
    public GameObject[] tilesPrefabs;
    // Distance at which a new tile is spawned
    public float zSpawn = 0;// an array of tile prefabs
    public Transform _player;// a reference to the player's transform


    private List<GameObject> activeTiles = new List<GameObject>();// a list to keep track of the spawned tiles  
    private float tileLength = 30;// the length of a single tile
    private float _NumberOfTiles = 5; // the number of tiles to be spawned at the beginning

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();// find and get the player's transform
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _NumberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);// spawn the first tile at index 0
            else
                SpawnTile(Random.Range(0,tilesPrefabs.Length));// spawn a random tile from the prefabs array
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if the player has moved beyond a certain point and the game has not started yet
        if( _player.transform.position.z - 35 > zSpawn - (_NumberOfTiles * tileLength) )
        {
            SpawnTile(Random.Range(0, tilesPrefabs.Length));// spawn a random tile from the prefabs array
            Delete();// delete the oldest spawned tile
        }
    }
    public void SpawnTile(int index)
    {
        GameObject go = Instantiate(tilesPrefabs[index],transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
    // deletes the oldest spawned tile from the activeTiles list
    public void Delete()
    {
        Destroy(activeTiles[0]);// destroy the oldest spawned tile  
        activeTiles.RemoveAt(0); // remove the oldest spawned tile from the activeTiles list
    }
}
