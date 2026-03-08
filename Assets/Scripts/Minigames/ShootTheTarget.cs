using UnityEngine;
using UnityEngine.InputSystem;

public class ShootTheAnswer : Minigame
{
    [SerializeField] private GameObject cubePrefab;
    private GameObject[,] cubes = new GameObject [2, 2];
    private GameObject greenCube;
    private bool mouseClick = false;

    protected override void Start()
    {
        //Calls parent start function
        base.Start();
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
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
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
        float spacing = 1.5f;
        Vector3 center = Camera.main.transform.position + Camera.main.transform.forward * 5f;

        int greenX = Random.Range(0,2);
        int greenY = Random.Range(0,2);

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Vector3 pos = center + new Vector3((i - 0.5f) * spacing, (j - 0.5f) * spacing, 0);
                GameObject cube = Instantiate(cubePrefab, pos, Quaternion.identity, transform);
                cube.transform.localScale = Vector3.one;

                if (i == greenX && j == greenY)

                {
                    cube.GetComponent<Renderer>().material.color = Color.green;
                    greenCube = cube;
                }
                else
                {

                    cube.GetComponent<Renderer>().material.color = Color.red;
                }

                cubes[i, j] = cube;
            } 
        } 
    } 
} 