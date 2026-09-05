using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour

{
    private Rigidbody2D rd ;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Animator animator;
    public bool isGrounded ;
    private bool facinRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rd = GetComponent <Rigidbody2D>();
        animator = GetComponent <Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rd.linearVelocity = new Vector2(move*moveSpeed,rd.linearVelocity.y);
        float speedAnimation = Mathf.Abs(move);
        animator.SetFloat("Speed",speedAnimation);

        if (move > 0 && !facinRight){
            Flip();
        }else if (move<0  && facinRight){
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isGrounded==true){
            rd.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping",true);
        }
        
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit2D (Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }

    void Flip(){
        facinRight =   ! facinRight ;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }
}
