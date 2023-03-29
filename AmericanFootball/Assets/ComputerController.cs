using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    public bool Sabit;
    public float timer2;
    public int tekShot;
    public int randomShootTime;
    public Animator shootAnim;
    Collider BallCollider;
    Rigidbody rigidbody;
    Rigidbody BallRigidbody;
    public Transform Everything;
    public int TriggerSayac;
    public int computerShotCounter;

    public bool ComputerIsShothing = false;

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

    public static bool isFastShotComputer;


    /*Catch The Ball*/
    public Transform rightHand;
    public static bool isTouchingComputer;
    //BallMovement ballMovement; Buraya ters yönlü kendi fonksiyonunu yazman lazým trigger ile referansýný alýrsýn zaten onu sakla ve sonrasý için kullan 
    public static bool isHoldTheBallComputer; //trigger enter olduðunda true exit olduðunda false olacak


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ball") //henüz topu tutmadýysa çalýþsýn
        {
            if (TriggerSayac < 1 && BallMovement.isFastBall == false)/*Henuz fastball saðlýklý test edilmedi*/
            {
                Debug.Log("isTriggerGiriþ");
                isHoldTheBallComputer = true;
                Ball.transform.parent = rightHand.transform;
                Ball.transform.position = rightHand.transform.position;
                Ball.transform.rotation = Quaternion.Euler(0, 0, 0);
                Ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                TriggerSayac++;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            Debug.Log("isTriggerCikis");
            TriggerSayac = 0; //eðer çalýþýrsa atýþ mekaniðini giriþ çýkýþ üzerinden kurmayý deneyebilirsin.
        }
    }



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
        isFastShotComputer = false; //**Son eklenen
        ComputerIsShothing = false;
        isHoldTheBallComputer = false;
        BallCollider = Ball.GetComponent<Collider>();
        BallRigidbody = Ball.GetComponent<Rigidbody>();
        ballDistance = 6f;
        rigidbody = GetComponent<Rigidbody>();
        //StartCoroutine(getRandomTimeAndMove(randomTime)); //Bu fonksiyona geldikten sonra fonksiyon içerisindeki kodlar belirlenen süre kadar eþyordamlý olarak çalýþmaya devam eder.
        //yani awake->start->fixedUpdate->update ... farketmeksizin tüm iþleveler olmasý gerektiði gibi çalýþýrken bu kod start'da bir kere çaðrýlmasýna kkarþýn eþ yordamlý olarak çalýþmaya devam eder.
    }

    private void FixedUpdate()
    {
        Debug.Log("True : " + isFastShotComputer);
        if (ballDistance < 7f) //Buraya top rakipten çýktýktan sonra herhangi bir objeye çarpýncaya kadar true olacak bir deðer gir****
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
        //Top elimde deðilse ve hýzlý deðilse ve bana yakýnsa ona yönelmem gerekir.
        //Topu tuttuktan sonra topun peþindeen gitmeye devam ederse eli bedeninin önünde olduðu için sonsuza kadar düz devam eder. 
        
        if (Ball.position.z <= 3.8f && ballDistance < 8f && !BallMovement.isFastBall && !isHoldTheBallComputer && transform.position.z > -5f) //Buraya top rakipten çýktýktan sonra herhangi bir objeye çarpýncaya kadar true olacak bir deðer gir****
        {          
            Debug.Log("Yavaþ ve yakýn");
            //arkamda mý önümde mi hesaplamam lazým 
            if (Ball.position.z - transform.position.z < 0)
            {
                moveRight = 0;
            }
            else
            {
                moveRight = 1;
            }
            //transform.position = Vector3.MoveTowards(transform.position, Ball.position, 5f);
            //transform.position = new Vector3(transform.position.x, 1.49f, transform.position.z);

        }


        if (this.shootAnim.GetCurrentAnimatorStateInfo(0).IsName("shot") && isHoldTheBallComputer && ComputerIsShothing) //computer is shooting rastgele bir süre de true olup sonra hemen false olan bir deðer.(sayaclý)
        {
            shoot();
            isFastShotComputer = true;
            ComputerIsShothing = false;
            timer2 = 0; //tekrar edebilmesi için
            shootAnim.SetBool("shot", false);//yukarýda yer alan iþlemler bittikten sonra da tekrar false yapabiliriz.
            TriggerSayac = 0;
        }

        //Ball.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        Debug.Log("True : " + isFastShotComputer);
        RandomTime();
        if (isHoldTheBallComputer && (timer2 < randomShootTime))
        {
            Debug.Log("Whohhoohohoh");
            timer2 += Time.deltaTime;
        }
        if(isHoldTheBallComputer && !(timer2 < randomShootTime))
        {
            Debug.Log("ohhoohohoh");
            ComputerIsShothing = true;
            shootAnim.SetBool("shot", true);
            
            tekShot = 0;
        }


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
            
            if (transform.position.z < -5f)
            {
                moveRight = 1;
            }
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
        /*
        if(BallRigidbody != null)
        {
            shoot(); //****Bunun için ayrý bir enumerator fonksiyonu aç ve 1,5 arasý bir saniye sonra top atmasýný saðla o top atýlýþý yapýlýrken zýplamada olduðu gibi isGrounded gibi bir þart koy ki atýþ bitmeden hareket baþlamasýn.
        }
        */
        
    }


    void shoot()//Animasyon kýsmýný buraya da ekle
    {
        Debug.Log("computer : "+isHoldTheBallComputer);
        Debug.Log("player : "+BallMovement.isHoldBall);
        if (BallMovement.isHoldBall == false && isHoldTheBallComputer)
        {
            Debug.Log("Buraya Giriyor");
            
            
            Ball.transform.parent = Everything;
            BallRigidbody.constraints = RigidbodyConstraints.None;
            BallRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            BallCollider.enabled = true;
            isHoldTheBallComputer = false;
            BallRigidbody.velocity = new Vector3(0f, 0f, -20f); //Bu kýsým kýsýtlamalar kalktýktan sonra çalýþmalýdýr. 
            //shootAnim.SetBool("shot", true);
            isFastShotComputer = true;
        }
        
    }


    void RandomTime()
    {
        if (tekShot < 1)
        {
            randomShootTime = Random.Range(1, 4); //1,2,3
            tekShot++;
        }
        
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
