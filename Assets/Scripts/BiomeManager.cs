using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

public class BiomeManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> straightTiles = new List<GameObject>();

    [SerializeField]
    List<GameObject> pooledTiles = new List<GameObject>();

    [SerializeField]
    Transform player;

    List<GameObject> activeTiles = new List<GameObject>();

    const float tileHeight = 40f;

    void Start()
    {
        EnqueueTile(GetStraightTile());
        EnqueueTile(GetStraightTile());
    }

    private void EnqueueRandomTile()
    {
        List<GameObject> tiles = new List<GameObject>(pooledTiles);

        foreach (var active in activeTiles)
            tiles.Remove(active);

        EnqueueTile(tiles[Random.Range(0, tiles.Count - 1)]);
    }

    void FixedUpdate()
    {
        if (player.position.y > activeTiles[0].transform.position.y + tileHeight)
        {
            Debug.Log(GameManager.Instance.State);

            switch (GameManager.Instance.State)
            {
                case GameState.Menu:    EnqueueTile(GetStraightTile()); break;
                case GameState.Rafting: EnqueueRandomTile(); break;
            }

            DequeueFirstTile();
        }
    }

    void EnqueueTile(GameObject tile)
    {
        if (activeTiles.Count > 0)
        {
            tile.transform.position = GetLastTile().position + tileHeight * Vector3.up;
        }
        else
        {
            tile.transform.position = Vector3.zero;
        }

        tile.SetActive(true);
        activeTiles.Add(tile);
    }

    void DequeueFirstTile()
    {
        activeTiles[0].SetActive(false);
        activeTiles.RemoveAt(0);
    }

    Transform GetLastTile()
    {
        return activeTiles[activeTiles.Count - 1].transform;
    }

    GameObject GetStraightTile()
    {
        return straightTiles.Where(item => !item.gameObject.activeInHierarchy).ToList().First();
    }
}
