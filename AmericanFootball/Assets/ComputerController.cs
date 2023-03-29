using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    Rigidbody rigidbody;
    float randomTime;
    float timer;
    int sayac;
    public float moveRight;
    public Transform front;
    public Transform back;
    public Transform Ball;
    float distanceFront;
    float distanceBack;
    float ballDistance;

    public static int jumpStopCounter;

    public bool isGrounded;


    float slowlyWalkEffect;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "stadium")
        {

            isGrounded = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ballDistance = 6f;
        rigidbody = GetComponent<Rigidbody>();
        //StartCoroutine(getRandomTimeAndMove(randomTime)); //Bu fonksiyona geldikten sonra fonksiyon içerisindeki kodlar belirlenen süre kadar eþyordamlý olarak çalýþmaya devam eder.
        //yani awake->start->fixedUpdate->update ... farketmeksizin tüm iþleveler olmasý gerektiði gibi çalýþýrken bu kod start'da bir kere çaðrýlmasýna kkarþýn eþ yordamlý olarak çalýþmaya devam eder.
    }

    private void FixedUpdate()
    {
        if (ballDistance < 7f) 
        {
            if (jumpStopCounter < 1) //Bu deðer karakter shot atýnca 0'lanýr
            {              
                rigidbody.velocity = new Vector3(0f, 7f, 0f);
                isGrounded = false; //yerden yükseldiði için bu deðer false olmalý bu false iken karakter saða sola hareket etmemeli 
                jumpStopCounter++;
               
            }

        }
    }

    private void Update()
    {
        //sýnýr çizgiler belirlenecek distance deðeri o deðere 0.1f'den yakýn olduðu durumda karakter otomatik olarak yön deðiþtirecek yani moveRight fonksiyonuna etki edilecek
        if (sayac < 1)
        {
            Debug.Log("Burasasdsadadý");
            sayac++;
            randomTime = Random.Range(0.2f, 0.5f);//1,2
            moveRight = Random.Range(0, 2); //0 or 1; Buradan gelen deðere göre animasyon deðerlerini oluþtur (Blend tree ile)           
            StartCoroutine(getRandomTimeAndMove(randomTime)); 
        }

        Debug.Log("Front : "+distanceFront);
        distanceFront = Vector3.Distance(front.position, transform.position); 
        distanceBack = Vector3.Distance(back.position, transform.position);
        ballDistance = Vector3.Distance(Ball.position, transform.position);
        
        

        if (distanceFront < 0.1f) //Burada yaplan deðiþiklikler farkedilebilsin diye moveRight global deðiþken olarak ayarlandý. Local deðiþken olsaydý burada olan dinamik deðiþiklikleri yakalayamazdýk.
        {
            moveRight = 1;
            //getRandomTimeAndMove(randomTime, moveRight);
            
        }
        if (distanceBack < 0.1f) //eðer arkaya yeterince yakýnsam öne doðru yani left'e doðru gitmesini istiyorum.
        {       
            moveRight = 0;
            
        }
       
        Debug.Log("Timer : " + timer);
        Debug.Log("RandomTime : " + sayac);
        
    }

    public IEnumerator getRandomTimeAndMove(float randomTime) //gelen random time süresi boyunca istenen yönde hareket etmesi için yazdýk
    {
        int sayac = 0;
        while (timer<=randomTime)
        {
            
            timer += Time.deltaTime;
            if (isGrounded) //havadaysa karakter hareket edemesin.
            {
                if (moveRight == 1)
                {
                    rigidbody.velocity = new Vector3(0f, 0f, 5f);
                }
                else
                {
                    rigidbody.velocity = new Vector3(0f, 0f, -(5f));
                }
                print("WaitAndPrint " + Time.time);
            }
            
            yield return new WaitForFixedUpdate();
        }
        timer = 0;
        this.sayac = 0;//Bunu yaparak belirlenen rastgele süre bittiðinde tekrar rakibin hareket etmesini saðlarýz.
        randomTime = 0;
    }


    public void letsMove()
    {
        StartCoroutine(getRandomTimeAndMove(randomTime)); //Bu fonksiyon çalýþtýktan x saniye sonra alt satýra geçer ve 
    }
    /*// Update is called once per frame
    void Update()
    {
        
    }
    */
}
