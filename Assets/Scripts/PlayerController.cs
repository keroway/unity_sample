using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    public Text countText;
    public Text winText;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        // 空中では操作不可
        // 以下の理由により本当は違う方法（地面との接触など）で判定を行うべき
        // 1.float同士の==比較は誤字で一致しないかも
        // 2.衝突判定次第で座標が安定しないかも
        // 3.外へ飛び出た際、Y座標が一致すれば空中ジャンプ可能
        if(transform.position.y == 0.5f)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            float moveJump = 0.0f;

            // ジャンプ
            if(Input.GetKeyDown("space"))
            {
                moveJump = 30.0f;
            }

            Vector3 movement = 
                new Vector3(moveHorizontal, moveJump, moveVertical);
            rb.AddForce(movement * speed);
        }
        else if(transform.position.y < -10.0f)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(0.0f, 10.5f, 0.0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
