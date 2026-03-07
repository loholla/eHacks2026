using TMPro;
using UnityEngine;

public class astroid : MonoBehaviour
{

    private int id;
    [SerializeField] private TextMeshProUGUI wordText;

    [SerializeField] float minSpeed, maxSpeed;

    private float speed;

    [SerializeField] private AstroidManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed) * GameManager.Instance.speedMultipler;

        manager = FindAnyObjectByType<AstroidManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
    }

    public void SetType(int type, string word)
    {
        id = type;
        if(type != 3)
        {
            wordText.text = word;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            if(id == 0)
            {
                manager.WonGame();
            }else if(id == 1)
            {
                manager.LostGame();
            }

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            
        }
    }

}
