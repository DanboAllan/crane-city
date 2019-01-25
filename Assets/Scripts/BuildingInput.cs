using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingInput : MonoBehaviour
{

public Tilemap targetTilemap;
    public Camera camera;

    void Start(){
       
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
          Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = targetTilemap.WorldToCell(mouseWorldPos);
       Debug.Log("MouseDOwn - " + coordinate);
        }

           if (Input.GetMouseButtonUp(0))
        {
          Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = targetTilemap.WorldToCell(mouseWorldPos);
       Debug.Log("MouseUp - " + coordinate);
        }
    }
}

