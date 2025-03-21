using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float DmgBullet;
    private float Direction;
    private bool Hit;
    private float TimeDestroyBulletIfNoImpact;
    

    private BoxCollider2D Box;
    private void Awake()
    {
        Box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Hit) return;
        float speed = Speed * Time.deltaTime * Direction;       //Tính khoảng cách di chuyển trong frame này, Direction: quyết định hướng
        transform.Translate(speed, 0, 0);

        TimeDestroyBulletIfNoImpact += Time.deltaTime;
        if(TimeDestroyBulletIfNoImpact > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit = true;
        Box.enabled = false;
        HealthEnemy enemyHealth = collision.GetComponent<HealthEnemy>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDmg((int)DmgBullet);
            gameObject.SetActive(false);
        }
        

    }
    public void SetDirection(float direction)
    {
        TimeDestroyBulletIfNoImpact = 0;
        Direction = direction;
        gameObject.SetActive(true);
        Hit = false;
        Box.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX = -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deativate()
    {
        gameObject.SetActive(false);
    }
}
