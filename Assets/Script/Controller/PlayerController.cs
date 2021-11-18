using System.Collections;
using Assets.Script.Base;
using Assets.Script.Menu;
using UnityEngine;
using Sound;

namespace Script.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask dashLayerMask;
        [SerializeField] public PlayMenu playMenu;

        private PlayerCharacter playerCharacter;
        private Playerinput playerInput;
        private Rigidbody2D Rd;
        private Vector3 MoveDie;
        private Animator animator;
        private bool IsAttacking = false;
        private bool Attack01 = false;
        private bool Attack02 = false;
        private bool Attack03 = false;
        private bool CanDash = true;
        public bool knockback = false;
        

        //ปรับได้
        private float MoveSpeed = 5f;
        float dashAmount = 3f;
        private float dashcooldown = 1;
        private float Attackcooldowntime;
        private float Attackcooldown = 2.5f;

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
            OnEnable();
            //adc
        }

        private void adc()
        {
            var tolalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var VARIABLE in tolalEnemies)
            {
                Destroy(VARIABLE);
            }
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
                animator.SetFloat("MoveX",walk.x);
                animator.SetFloat("MoveY",walk.y);
                animator.SetBool("Walking",true); 
            }
            else
            {
                SoundManager.Instance.Play(SoundManager.Sound.PlayerMovement);
                animator.SetBool("Walking",false); 
            }
            Rd.velocity = MoveDie * MoveSpeed;
            
            if (Attackcooldowntime <= Time.time)
            {
                AttackFinish03();
            }
        }

        private void Menu()
        {
            if (playMenu.isPause == false)
            {
                playMenu.Pause();
            }
            else
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
                    //Debug.Log($"Attack 1");
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
        
        public void AttackButton()
        {
            Attack();
        }
        

        private void Dash()
        {
            if (CanDash)
            {
                CanDash = false;
                SoundManager.Instance.Play(SoundManager.Sound.PlayerDash);
                Vector3 dashPoint = transform.position + MoveDie * dashAmount;

                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, MoveDie, dashAmount,dashLayerMask);
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
            Debug.Log($" candash {CanDash} and w dash");
            yield return new WaitForSeconds(dashcooldown);
            Debug.Log($" candash {CanDash} and dash");
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
            playMenu.Dead();
            Rd.constraints = RigidbodyConstraints2D.FreezeAll;
            OnDisable();
            //Time.timeScale = 0;
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
