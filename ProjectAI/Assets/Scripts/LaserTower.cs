using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{
    public Transform firePostion;                   //����λ��
    public LineRenderer laserRenderer;          //�������
    public GameObject attackEffect;             //������Ч
    public LayerMask laserattack;
    public int maxDistence = 50;                      //��󹥻�����

    public Vector2 respawn_position;
    // Start is called before the first frame update
    void Start()
    {
        laserattack = 1 << 0 | 1 << 3;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;
        Vector2 laserDirection = firePostion.up;
        hit = Physics2D.Raycast(firePostion.position, laserDirection, maxDistence);

        if (hit.collider != null)
        {
            laserRenderer.SetPosition(0, firePostion.position);
            laserRenderer.SetPosition(1, hit.point);
            attackEffect.SetActive(true);
            attackEffect.transform.position = hit.point;
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("dead");
                hit.collider.transform.position = respawn_position;
            }
        }

    }
}
