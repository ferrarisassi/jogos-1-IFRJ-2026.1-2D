using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemController : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [Tooltip("Velocidade máxima que o personagem alcança correndo.")]
    [SerializeField] private float velMaxima = 8f;
    
    [Tooltip("Quão rápido o personagem ganha velocidade. Valores maiores tornam a resposta mais instantânea.")]
    [SerializeField] private float aceleracao = 25f;
    
    [Tooltip("Quão rápido o personagem para quando você solta o botão. Valores maiores evitam que ele deslize.")]
    [SerializeField] private float desaceleracao = 35f;

    [Header("Configurações de Pulo")]
    [SerializeField] private float jumpForce = 12f;

    [Header("Referências")]
    [SerializeField] private GroundCheck groundCheckScript;
    
    private Rigidbody2D rb2d;
    private Animator anim;
    private float horizontalInput;
    private bool devePular;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        if (groundCheckScript == null)
        {
            Debug.LogError("Por favor, atribua o GroundCheckScript no Inspector deste objeto!");
        }

        if (anim == null)
        {
            Debug.LogError("Nenhum componente Animator foi encontrado neste objeto!");
        }
    }

    void Update()
    {
        
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && groundCheckScript != null && groundCheckScript.isOnGround)
        {
            devePular = true;
        }
        
        GirarSprite();
        AtualizarAnimacoes();
    }

    void FixedUpdate()
    {
        // Executa a movimentação horizontal precisa
        Mover();

        // Executa o pulo se foi solicitado no Update
        if (devePular)
        {
            EfetuarPulo();
        }
    }

    private void Mover()
    {
        float velocidadeAlvo = horizontalInput * velMaxima;
        float taxaVariacao = (horizontalInput != 0) ? aceleracao : desaceleracao;
        float novoX = Mathf.MoveTowards(rb2d.velocity.x, velocidadeAlvo, taxaVariacao * Time.fixedDeltaTime);

        rb2d.velocity = new Vector2(novoX, rb2d.velocity.y);
    }

    private void EfetuarPulo()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        devePular = false; 
    }

    private void GirarSprite()
    {
        
        if (horizontalInput > 0f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput < 0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void AtualizarAnimacoes()
    {
        if (anim == null) return;

        bool estaCorrendo = horizontalInput != 0;
        anim.SetBool("isRunning", estaCorrendo);

        if (groundCheckScript != null)
        {
            anim.SetBool("isGrounded", groundCheckScript.isOnGround);
        }

        anim.SetFloat("yVelocity", rb2d.velocity.y);
    }
}