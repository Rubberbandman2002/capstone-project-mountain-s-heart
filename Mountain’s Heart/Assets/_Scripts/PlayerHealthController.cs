﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{

    private int health = 8;
    private Rigidbody2D rb;
    private Vector2 recoil;
    private bool invincible = false;
    private int countdown = 60;
    [SerializeField] float recoilModifier = 30;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyStats>() != null && !invincible)
        {
            health -= collision.gameObject.GetComponent<EnemyStats>().getDamage();
            recoil = new Vector2(recoilModifier * (this.transform.position.x - collision.gameObject.transform.position.x),
                                    recoilModifier * (this.transform.position.y - collision.gameObject.transform.position.y));
            invincible = true;
            GetComponent<Rigidbody2D>().AddForce(recoil);

            if (health < -1)
                health = -1;
        }
    }

    public int getHealth()
    {
        return health;
    }

    private void Update()
    {
        if (invincible)
        {
            countdown--;
            if(countdown <= 0)
            {
                invincible = false;
                countdown = 60;
            }
        }


        if (health < 0)
            SceneManager.LoadScene(sceneName: "Main Menu");
    }

}
