using UnityEngine;

namespace Assets.Script
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask dashLayerMask;
        private Rigidbody2D Rd;
        private Vector3 MoveDie;
        private Animator animator;
        private bool isDashButtonDown;
        
        //ปรับได้
        private const float MoveSpeed = 5f;
        float dashAmount = 3f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            Rd = GetComponent<Rigidbody2D>();
        }
    

        // Update is called once per frame
        private void Update()
        {
            float MoveX = 0;
            float MoveY = 0;
        
            Rd.MoveRotation(0);

            if (Input.GetKey(KeyCode.W))
            {
                MoveY = +1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                MoveY = -1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                MoveX = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                MoveX = +1f;
            }

            if (Input.GetMouseButton(0))
            {
                Attack();
            }
        
            MoveDie = new Vector3(MoveX,MoveY).normalized;
        
            animator.SetFloat("MoveX",MoveX);
            animator.SetFloat("MoveY",MoveY);
            animator.SetFloat("Speed",MoveDie.sqrMagnitude); ;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDashButtonDown = true;
            }
        
        }

        private void FixedUpdate()
        {
            Rd.velocity = MoveDie * MoveSpeed;

            if (isDashButtonDown == true)
            {
                Vector3 dashPoint = transform.position + MoveDie * dashAmount;

                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, MoveDie, dashAmount,dashLayerMask);
                if (raycastHit2D.collider != null)
                {
                    dashPoint = raycastHit2D.point;
                }
                Rd.MovePosition(dashPoint);
                isDashButtonDown = false;
            }
        }

        private void Attack()
        {
            print($"Attack");
        }
    }
}
