using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Trap
{
    [SerializeField] private float Speed;       // tốc độ mũi tên
    [SerializeField] private float resetTime;
    private float LifeTime;

    public void Activate()
    {
        LifeTime = 0;
        gameObject.SetActive(true);     //vừa vào game đạn sẽ bắn luôn
    }
    private void Update()
    {
        float movementSpeed = Speed * Time.deltaTime;       // tốc độ mũi tên
        transform.Translate(movementSpeed, 0, 0);       // mũi tên di chuyển theo trục X
        LifeTime += Time.deltaTime;
        if (LifeTime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);           // base: nếu có 2 phương thức giống nhau nó sẽ lấy từ phương thức Cha trước(Parent class)
        gameObject.SetActive(false);
    }
}
