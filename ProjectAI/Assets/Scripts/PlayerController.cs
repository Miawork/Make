using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Ҫ������ٿ���ң������ק����ק��ԽԶ����ƶ���ԽԶ��ͨ����������������߼�������ƶ����Ҽ���ʹ��ͬ��ɫ��ƽ̨���Ի���ʧ��
    [Header("Dynamically")]
    public Vector2 playerPosition;
    public Vector2 mouseDownPosition;
    public Vector2 mouseDelta;
    public float forceMagnitude;
    public Vector2 distance;
    private Rigidbody2D playerRb2D;
    public CircleCollider2D circleCollider2D;
    public bool aimmode;

    [Header("SoundEffect")]
    public AudioSource moveSound;

    [Header("Change Platform")]
    public ColorIndicator colorIndicator;
    public bool canSwitch = true;
    public bool isTiming = false;
    public GameObject[] obstacles;
    public int currentObstacleIndex = 0;

    [Header("GroundDetection")]
    public bool isGround = false;
    public LayerMask groundLayer;

    public Vector2 respwan_position;




    private void Start()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GroundDetection();
        colorIndicator.colorIndex = currentObstacleIndex;//ͬ������

        if (!aimmode) return;
        mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = new Vector2(transform.position.x, transform.position.y);
        mouseDelta = mouseDownPosition - playerPosition;//��ȡ�����������λ�õ������
        float maxMagnitude = circleCollider2D.radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;//������������Χ������������Ϊ���Χ
        }

        //������̧��ʩ����
        if (Input.GetMouseButtonUp(0))
        {
            aimmode = false;
            if (isGround)
            {
                moveSound.Play();
                playerRb2D.gravityScale = 0.5f;
                playerRb2D.AddForce(mouseDelta * forceMagnitude);
            }
            else { return; }

        }


        

    }
    //������������
    private void OnMouseDown()
    {
        aimmode = true;
        playerRb2D.gravityScale = 0.5f;

    }



    private void GroundDetection()
    {
        var rayCastAll = Physics2D.RaycastAll(transform.position, Vector2.down, 0.5f, groundLayer);
        if (rayCastAll.Length > 0)
        {
            isGround = true;
            Debug.Log("isGround");
        }
        else
        {
            isGround = false;
            Debug.Log("Jumping");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FinishPoint"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }




}

