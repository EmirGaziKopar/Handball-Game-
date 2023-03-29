using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    Rigidbody rigidbody;
    float randomTime;
    float timer;
    int sayac;
    int sayac2;
    int sayac3;
    public float moveRight;
    public Transform front;
    public Transform back;
    public Transform Ball;
    float distanceFront;
    float distanceBack;
    float ballDistance;
    public Animator walk;

    float smoothness = 0;

    public static int jumpStopCounter;

    public bool isGrounded;


    float slowlyWalkEffect;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "stadium")
        {

            isGrounded = true;
            walk.SetBool("backflip", false); // yere düþünce zýplama animasyonu durmalý
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
                walk.SetFloat("Vertical", 0); //sýçrama sýrasýnda yürüme animasyonu kapanmalý zýplama animasyonu çalýþmalý
                walk.SetBool("backflip",true);
                //walk.SetBool("backflip", false);

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
            randomTime = Random.Range(0.5f, 0.8f);//1,2 0.2f , 0.5f
            moveRight = Random.Range(0, 3); //0 or 1 , added 2 for stability ; Buradan gelen deðere göre animasyon deðerlerini oluþtur (Blend tree ile)           
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
                    sayac3 = 0;
                    if (sayac2 < 1)
                    {
                        smoothness = 0;
                        sayac2++; //tekrar tekrar move geldiðinde ayný yöne giderken kesik kesik durmalar olmasýn diye eklendi
                    }
                    
                    walk.SetFloat("Vertical", -1);
                    if (smoothness < 1)
                    {
                        smoothness += Time.deltaTime*4f;
                    }
                    

                    rigidbody.velocity = new Vector3(0f, 0f, smoothness * 3f);
                }
                else if(moveRight == 2)
                {
                    sayac2 = 0; //sayac 2 sýfýrlanmalý çünkü rakip artýk durdu tekrar kalkýþa geçtiðinde smoothness 0 olacak ve yavaþ yavaþ kalkacak
                    sayac3 = 0;
                    walk.SetFloat("Vertical", 0f);
                    rigidbody.velocity = new Vector3(0f, 0f, 0f);
                }
                else
                {
                    sayac2 = 0; //Bu sayaçlar tek yöne iki defa art arda gittiði zaman animasyon durmasýn diye böyle yazýldý
                    if (sayac3 < 1)
                    {
                        smoothness = 0;
                        sayac3++; //tekrar tekrar move geldiðinde ayný yöne giderken kesik kesik durmalar olmasýn diye eklendi
                    }
                    if (smoothness < 1)
                    {
                        smoothness += Time.deltaTime*3f;
                    }
                    walk.SetFloat("Vertical", 1);
                    rigidbody.velocity = new Vector3(0f, 0f, -(smoothness * 5f));
                }
                print("WaitAndPrint " + Time.time);
            }
            
            yield return new WaitForFixedUpdate();
        }
        //smoothness = 0;
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
