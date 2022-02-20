using System;
using System.Collections;
using System.IO;
using Script.Base;
using Script.Menu;
using Script.Sound;
using TMPro;
using UnityEngine;

namespace Script.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask dashLayerMask;
        [SerializeField] public PlayMenu playMenu;

        private PlayerCharacter playerCharacter;
        public static Playerinput playerInput;
        private Rigidbody2D Rd;
        private Vector3 MoveDie;
        private Animator animator;
        private bool IsAttacking = false;
        private bool Attack01 = false;
        private bool Attack02 = false;
        private bool Attack03 = false;
        public static bool CanDash = true;
        public bool knockback = false;
        

        //ปรับได้
        private float MoveSpeed = 5f;
        float dashAmount = 3f;
        private float dashcooldown = 1;
        private float Attackcooldowntime;
        private float Attackcooldown = 2.5f;
        private bool Soundplay;

        private void Awake()
        {
            Time.timeScale = 1f;
            animator = GetComponent<Animator>();
            Rd = GetComponent<Rigidbody2D>();
            playerCharacter = GetComponent<PlayerCharacter>();
            playerInput = new Playerinput();
            playerInput.PlayerAction.Attack.performed += context => Attack();
            playerInput.PlayerAction.Dash.performed += context => Dash();
            playerInput.PlayerAction.Pause.performed += context => Menu();
            playerInput.PlayerAction.Cheat.performed += context => Cheat();
            OnEnable();
        }
        private void Start()
        {
            SoundManager.Instance.Play(SoundManager.Sound.PlayerMovement);
        }

        private void Cheat()
        {
            playerCharacter.Gold += 100;
            playerCharacter.Hp += 100;
        }

        private void Update()
        {
            dashcooldown = playerCharacter.DashCd;
            MoveSpeed = playerCharacter.Speed;
            //Walk
            var walk = playerInput.PlayerAction.Move.ReadValue<Vector2>();
            
            MoveDie = walk.normalized;
            
            if (walk != Vector2.zero)
            {
                if (Soundplay)
                {
                    SoundManager.Instance.Play(SoundManager.Sound.PlayerMovement);
                    Soundplay = false;
                }
                animator.SetFloat("MoveX",walk.x);
                animator.SetFloat("MoveY",walk.y);
                animator.SetBool("Walking",true);
            }
            else
            {
                SoundManager.Instance.Stop(SoundManager.Sound.PlayerMovement);
                Soundplay = true;
                animator.SetBool("Walking",false); 
            }

            if (Attackcooldowntime <= Time.time)
            {
                AttackFinish03();
            }
        }

        private void FixedUpdate()
        {
            Rd.velocity = MoveDie * MoveSpeed;
        }

        private void Menu()
        {
            if (playMenu.isPause == false)
            {
                playMenu.Pause();
            }
            else if (playMenu.isPause == true)
            {
                playMenu.Resume();
            }
        }

        private void Attack()
        {
            if (IsAttacking == false)
            {
                if (Attack01 == false)
                {
                    SoundManager.Instance.Play(SoundManager.Sound.PlayerHit1);
                    IsAttacking = true;
                    Attack01 = true;
                    animator.SetBool("Attacking",true); 
                    animator.SetBool("Attack01",true);
                    Attackcooldowntime = Time.time + Attackcooldown;
                    
                }
                else if (Attack02 == false)
                {
                    SoundManager.Instance.Play(SoundManager.Sound.PlayerHit2);
                    IsAttacking = true;
                    Attack02 = true;
                    animator.SetBool("Attacking",true);
                    animator.SetBool("Attack02",true);
                    Attackcooldowntime = Time.time + Attackcooldown;
                    //Debug.Log($"Attack 2");
                }
                else if (Attack03 == false)
                {
                    SoundManager.Instance.Play(SoundManager.Sound.PlayerHit3);
                    IsAttacking = true;
                    Attack03 = true;
                    animator.SetBool("Attacking",true); 
                    animator.SetBool("Attack03",true);
                    knockback = true;
                    //Debug.Log($"Attack 3");
                }

            }
        }
        
        private void Dash()
        {
            if (CanDash)
            {
                CanDash = false;
                SoundManager.Instance.Play(SoundManager.Sound.PlayerDash);
                Vector3 dashPoint = transform.position + MoveDie * dashAmount;

                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, MoveDie, 
                    dashAmount,dashLayerMask);
                if (raycastHit2D.collider != null)
                {
                    dashPoint = raycastHit2D.point;
                }
                Rd.MovePosition(dashPoint);
                StartCoroutine(DashCooldown());
            }
        }

        IEnumerator DashCooldown()
        {
            yield return new WaitForSeconds(dashcooldown);
            CanDash = true;
        }
        
        public void DashButton()
        {
            Dash();
        }

        private void AttackFinish()
        {
            animator.SetBool("Attacking",false);
            IsAttacking = false;
        }
        
        public void AttackFinish03()
        {
            IsAttacking = false;
            Attack01 = false;
            Attack02 = false;
            Attack03 = false;
            animator.SetBool("Attacking",false);
            animator.SetBool("Attack01",false);
            animator.SetBool("Attack02",false);
            animator.SetBool("Attack03",false);
            Attackcooldowntime = 0;
        }

        public void Dead()
        {
            SoundManager.Instance.Play(SoundManager.Sound.PlayerDie);
            playMenu.Dead();
            Rd.constraints = RigidbodyConstraints2D.FreezeAll;
            OnDisable();
        }

        private void OnEnable()
        {
            playerInput.Enable();
        }

        private void OnDisable()
        {
            playerInput.Disable();
        }
        
    }
}
