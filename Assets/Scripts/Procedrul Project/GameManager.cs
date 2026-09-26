using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private RoomGenerator roomGenerator;
    public int numberofbuttons;
    public int numberofskulls;
    public int numberofchests;
    private PlayerMovement player;
    public GameObject button;
    public Sprite buttonon;
    private List<GameObject> buttons = new List<GameObject>();
    private List<GameObject> buttonsremove = new List<GameObject>();
    private int levelnumber;
    public GameObject Exit;
    public Sprite ExitOpen;
    public GameObject Skull;
    private List<GameObject> skulls = new List<GameObject>();
    public GameObject Chest;
    private List<GameObject> chests = new List<GameObject>();
    private Vector3 exitcoordinates;
    public int areaincrease;
    private bool[,] occupiedTile;
    private GameObject exitinstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roomGenerator = GetComponent<RoomGenerator>();
        roomGenerator.generate();
        occupiedTile = new bool[roomGenerator.AreaWidth, roomGenerator.AreaHeight];
        for (int x = 0; x < roomGenerator.AreaWidth; x++)
        {
            for (int y = 0; y < roomGenerator.AreaHeight; y++)
            {
                occupiedTile[x, y] = false;
            }
        }
        numberofbuttons = PlayerPrefs.GetInt("numberofbuttons", numberofbuttons);
        numberofskulls = PlayerPrefs.GetInt("numberofskulls", numberofskulls);
        numberofchests = PlayerPrefs.GetInt("numberofchests", numberofchests);
        levelnumber = PlayerPrefs.GetInt("levelnumber", 1);
        populatelevels();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            float distance = Vector3.Magnitude(player.transform.position - buttons[i].transform.position);
            if(distance < 1)
            {
                buttonsremove.Add(buttons[i]);

                
            }
        }
        for(int i = 0;i < buttonsremove.Count; i++)
        {
            buttons.Remove(buttonsremove[i]);
            buttonsremove[i].GetComponent<SpriteRenderer>().sprite = buttonon;
        }
        if(buttons.Count == 0)
        {
            exitinstance.GetComponent<SpriteRenderer>().sprite = ExitOpen;//TODO: optimise this
            float distance = Vector3.Magnitude(player.transform.position - exitcoordinates);
            if(distance < 1)
            {
                levelnumber++;
                PlayerPrefs.SetInt("levelnumber", levelnumber);
                int roomattempt = PlayerPrefs.GetInt("roomattempt", roomGenerator.roomAttempt);
                roomattempt++;
                PlayerPrefs.SetInt("roomattempt", roomattempt);
                int AreaWidth = PlayerPrefs.GetInt("areawidth", roomGenerator.AreaWidth);
                AreaWidth += areaincrease;
                PlayerPrefs.SetInt("areawidth", AreaWidth);
                int AreaHeight = PlayerPrefs.GetInt("areaheight", roomGenerator.AreaHeight);
                AreaHeight += areaincrease;
                PlayerPrefs.SetInt("areaheight", AreaHeight);
                int Numberofbuttons = PlayerPrefs.GetInt("numberofbuttons", numberofbuttons);
                if (levelnumber % 3 == 0)
                {
                    Numberofbuttons++;
                }
                PlayerPrefs.SetInt("numberofbuttons", Numberofbuttons);
                int NumberOfSkulls = PlayerPrefs.GetInt("numberofskulls", numberofskulls);
                if (levelnumber % 3 == 0)
                {
                    NumberOfSkulls++;
                }
                PlayerPrefs.SetInt("numberofskulls", NumberOfSkulls);
                int NumberOfChests = PlayerPrefs.GetInt("numberofchests", numberofchests);
                if (levelnumber % 5 == 0)
                {
                    NumberOfChests++;
                }
                PlayerPrefs.SetInt("numberofchests", NumberOfChests);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }
        
    }
    
    private void populatelevels()
    {
        List<RectInt> rooms = roomGenerator.getrooms();
        player = FindAnyObjectByType<PlayerMovement>();
        placeobjects(rooms, numberofbuttons, button, buttons);

        placeobjects(rooms, numberofskulls, Skull, skulls);

        int randomnumberofchests = UnityEngine.Random.Range(0, numberofchests);
        placeobjects(rooms, randomnumberofchests, Chest, chests);

        var randomroom = rooms[0];
        player.transform.position = roomGenerator.ChooseTile(randomroom, occupiedTile);
        var exitroom = rooms[1];
        exitcoordinates = roomGenerator.ChooseTile(exitroom, occupiedTile);
        exitinstance = GameObject.Instantiate(Exit);
        exitinstance.transform.position = new Vector3(exitcoordinates.x, exitcoordinates.y, 0);

    }

    private void placeobjects(List<RectInt> rooms, int numberofobjects, GameObject objectprefab, List<GameObject> objectlist)
    {
        List<int> RoomsWithObjects = new List<int>();
        for (int i = 0; i < numberofobjects; i++)
        {
            int randomroomint = UnityEngine.Random.Range(0, rooms.Count - 1);
            int repetition = 10;
            while (RoomsWithObjects.Contains(randomroomint) && repetition > 0)
            {
                randomroomint = UnityEngine.Random.Range(0, rooms.Count - 1);
                repetition--;
            }
            if (repetition == 0)
            {
                Debug.LogError("PlaceObjects could not find a spare room.");
            }
            RoomsWithObjects.Add(randomroomint);
            var randomroom1 = rooms[randomroomint];
            var objecttile = roomGenerator.ChooseTile(randomroom1, occupiedTile);
            var newobject = GameObject.Instantiate(objectprefab);
            objectlist.Add(newobject);
            newobject.transform.position = new Vector3(objecttile.x, objecttile.y, 0);
        }
    }

}
