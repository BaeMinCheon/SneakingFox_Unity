﻿using UnityEngine;
using System.Collections;

namespace Completed
{
    public class Bird_Move : MovingObject
    {
        public int playerDamage;
        public AudioClip attackSound1;
        public AudioClip attackSound2;
        private Animator animator;
        private Transform target;
        private bool skipMove;

        protected override void Start()
        {
            //GameManager.instance.AddEnemyToList(this);

            animator = GetComponent<Animator>();

            target = GameObject.FindGameObjectWithTag("Player").transform;

            base.Start();
        }


        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            //Check if skipMove is true, if so set it to false and skip this turn.
            if (skipMove)
            {
                skipMove = false;
                return;
            }

            //Call the AttemptMove function from MovingObject.
            base.AttemptMove<T>(xDir, yDir);
            skipMove = true;
        }


        public void MoveEnemy()
        {
            int xDir = 0;
            int yDir = 0;

            if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
                yDir = target.position.y > transform.position.y ? 1 : -1;

            else
                xDir = target.position.x > transform.position.x ? 1 : -1;

            AttemptMove<Fox_Move>(xDir, yDir);
        }

        protected override void OnCantMove<T>(T component)
        {
            Fox_Move hitPlayer = component as Fox_Move;
            //hitPlayer.LoseFood(playerDamage);
            animator.SetTrigger("enemyAttack");
            //SoundManager.instance.RandomizeSfx(attackSound1, attackSound2);
        }
    }
}
