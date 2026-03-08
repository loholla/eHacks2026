using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootTheTarget : Minigame
{
    [SerializeField] private GameObject cubePrefab;
    private GameObject[,] cubes = new GameObject [2, 2];
    private GameObject greenCube;
    [SerializeField] private Camera cam;
    private bool mouseClick = false;

    [SerializeField] public FlashCard prompt;
    public Decks Deck;
    private List<FlashCard> currentDeck;
    public TextMeshProUGUI question;
    int ans, counter;

    protected override void Start()
    {
        base.Start();

        currentDeck = Deck.flashcards;
        ans = (int)Mathf.Abs(Random.Range(0f, currentDeck.Count - 1));
        counter = 0;
        
        foreach (var card in currentDeck)
        {
            if (counter == ans)
            {
                prompt = card;
                counter++;
            }
            else
            {
                counter++;
            }
        }

        currentDeck.RemoveAt(ans);
        question.SetText(prompt.definition);

        SpawnGrid();
    }
    private void OnEnable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnActionPressed += HandleAction;
        }
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnActionPressed -= HandleAction;
        }
    }

    private void HandleAction(string actionName)
    {
        if (actionName == "LeftClick")
        {
            mouseClick = true;
        }
    }

    protected override void Update()
    {
        //Calls parent update function
        base.Update();

        if (mouseClick)
        {
            mouseClick = false;
            CheckShot();
        }
    }

    private void CheckShot()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.gameObject == greenCube)
            {
                Debug.Log("Hit Green! Winner!");
                WonGame();
            }
            else
            {
                Debug.Log("Hit Red! Loser!");
                LostGame();
            }
        }
    }

    void SpawnGrid()
    {
        float spacing = 2f;
        Vector3 center = cam.transform.position + cam.transform.forward * 5f;

        int greenX = Random.Range(0,2);
        int greenY = Random.Range(0,2);

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Vector3 pos = center + new Vector3((i - 0.5f) * spacing, (j - 0.5f) * spacing, 0);
                GameObject cube = Instantiate(cubePrefab, pos, Quaternion.identity, transform);
                cube.GetComponent<Renderer>().material.color = Color.black;

                if (i == greenX && j == greenY)
                {
                    greenCube = cube;
                    greenCube.GetComponentInChildren<TextMeshProUGUI>().SetText(prompt.word);

                }
                else
                {
                    ans = (int)Mathf.Abs(Random.Range(0f, currentDeck.Count - 1));
                    cube.GetComponentInChildren<TextMeshProUGUI>().SetText(currentDeck[ans].word);
                }

                cubes[i, j] = cube;
            } 
        } 
    } 
} 