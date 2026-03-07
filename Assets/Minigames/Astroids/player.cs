using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{


    private float playerDirection;

    [SerializeField] float xBoundLower, xBoundUpper;

    [SerializeField] float speed;

    [SerializeField] GameObject bulletPrefab;

    [SerializeField] Transform bulletSpawnSpot;

    
    private bool walkLeft = false;
    
    private bool walkRight = false;
   

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
        
        if (actionName == "WalkLeftD")
        {
            walkLeft = true;
        }
       
        else if (actionName == "WalkRightD")
        {
            walkRight = true;
        }
       
        else if (actionName == "WalkLeftC")
        {
            walkLeft = false;
        }
        
        else if (actionName == "WalkRightC")
        {
            walkRight = false;
        }

        if(actionName == "LeftClick")
        {
            FireBullet();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = 0;
        if (walkLeft)
        {
            playerDirection--;
        }
        if (walkRight)
        {
            playerDirection++;
        }
        


        if ((transform.position.x <= xBoundLower && playerDirection < 0) || (transform.position.x >= xBoundUpper && playerDirection > 0))
        {
            playerDirection = 0;
        }

        transform.position += new Vector3(playerDirection * speed * Time.deltaTime * GameManager.Instance.speedMultipler, 0, 0);
    }

    private void FireBullet()
    {
        Instantiate(bulletPrefab, bulletSpawnSpot.position, Quaternion.identity);
    }
}
