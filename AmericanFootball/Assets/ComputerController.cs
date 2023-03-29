using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    Collider BallCollider;

    Rigidbody rigidbody;
    Rigidbody BallRigidbody;
    public Transform Everything;
    public int TriggerSayac;

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


    /*Catch The Ball*/
    public Transform rightHand;
    public static bool isTouchingComputer;
    //BallMovement ballMovement; Buraya ters y�nl� kendi fonksiyonunu yazman laz�m trigger ile referans�n� al�rs�n zaten onu sakla ve sonras� i�in kullan 
    public static bool isHoldTheBallComputer; //trigger enter oldu�unda true exit oldu�unda false olacak


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ball") //hen�z topu tutmad�ysa �al��s�n
        {
            if (TriggerSayac < 1)
            {
                Debug.Log("isTriggerGiri�");
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
            TriggerSayac = 0; //e�er �al���rsa at�� mekani�ini giri� ��k�� �zerinden kurmay� deneyebilirsin.
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "stadium")
        {

            isGrounded = true;
            walk.SetBool("backflip", false); // yere d���nce z�plama animasyonu durmal�

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        BallCollider = Ball.GetComponent<Collider>();
        BallRigidbody = Ball.GetComponent<Rigidbody>();
        ballDistance = 6f;
        rigidbody = GetComponent<Rigidbody>();
        //StartCoroutine(getRandomTimeAndMove(randomTime)); //Bu fonksiyona geldikten sonra fonksiyon i�erisindeki kodlar belirlenen s�re kadar e�yordaml� olarak �al��maya devam eder.
        //yani awake->start->fixedUpdate->update ... farketmeksizin t�m i�leveler olmas� gerekti�i gibi �al���rken bu kod start'da bir kere �a�r�lmas�na kkar��n e� yordaml� olarak �al��maya devam eder.
    }

    private void FixedUpdate()
    {
        if (ballDistance < 7f) 
        {
            if (jumpStopCounter < 1) //Bu de�er karakter shot at�nca 0'lan�r
            {              
                rigidbody.velocity = new Vector3(0f, 7f, 0f);
                isGrounded = false; //yerden y�kseldi�i i�in bu de�er false olmal� bu false iken karakter sa�a sola hareket etmemeli 
                jumpStopCounter++;
                walk.SetFloat("Vertical", 0); //s��rama s�ras�nda y�r�me animasyonu kapanmal� z�plama animasyonu �al��mal�
                walk.SetBool("backflip",true);
                //walk.SetBool("backflip", false);

            }

        }


        //Ball.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        //s�n�r �izgiler belirlenecek distance de�eri o de�ere 0.1f'den yak�n oldu�u durumda karakter otomatik olarak y�n de�i�tirecek yani moveRight fonksiyonuna etki edilecek
        if (sayac < 1)
        {
            Debug.Log("Burasasdsadad�");
            sayac++;
            randomTime = Random.Range(0.5f, 0.8f);//1,2 0.2f , 0.5f
            moveRight = Random.Range(0, 3); //0 or 1 , added 2 for stability ; Buradan gelen de�ere g�re animasyon de�erlerini olu�tur (Blend tree ile)           
            StartCoroutine(getRandomTimeAndMove(randomTime)); 
        }

        Debug.Log("Front : "+distanceFront);
        distanceFront = Vector3.Distance(front.position, transform.position); 
        distanceBack = Vector3.Distance(back.position, transform.position);
        ballDistance = Vector3.Distance(Ball.position, transform.position);
        
        

        if (distanceFront < 0.1f) //Burada yaplan de�i�iklikler farkedilebilsin diye moveRight global de�i�ken olarak ayarland�. Local de�i�ken olsayd� burada olan dinamik de�i�iklikleri yakalayamazd�k.
        {

            moveRight = 1;
            //getRandomTimeAndMove(randomTime, moveRight);
            
        }
        if (distanceBack < 0.1f) //e�er arkaya yeterince yak�nsam �ne do�ru yani left'e do�ru gitmesini istiyorum.
        {       
            moveRight = 0;
            
        }
       
        Debug.Log("Timer : " + timer);
        Debug.Log("RandomTime : " + sayac);
        
    }

    public IEnumerator getRandomTimeAndMove(float randomTime) //gelen random time s�resi boyunca istenen y�nde hareket etmesi i�in yazd�k
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
                        sayac2++; //tekrar tekrar move geldi�inde ayn� y�ne giderken kesik kesik durmalar olmas�n diye eklendi
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
                    sayac2 = 0; //sayac 2 s�f�rlanmal� ��nk� rakip art�k durdu tekrar kalk��a ge�ti�inde smoothness 0 olacak ve yava� yava� kalkacak
                    sayac3 = 0;
                    walk.SetFloat("Vertical", 0f);
                    rigidbody.velocity = new Vector3(0f, 0f, 0f);
                }
                else
                {
                    sayac2 = 0; //Bu saya�lar tek y�ne iki defa art arda gitti�i zaman animasyon durmas�n diye b�yle yaz�ld�
                    if (sayac3 < 1)
                    {
                        smoothness = 0;
                        sayac3++; //tekrar tekrar move geldi�inde ayn� y�ne giderken kesik kesik durmalar olmas�n diye eklendi
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
        this.sayac = 0;//Bunu yaparak belirlenen rastgele s�re bitti�inde tekrar rakibin hareket etmesini sa�lar�z.
        randomTime = 0;
        if(BallRigidbody != null)
        {
            shoot();
        }
        
        
    }


    void shoot()
    {
        Debug.Log("computer : "+isHoldTheBallComputer);
        Debug.Log("player : "+BallMovement.isHoldBall);
        if (BallMovement.isHoldBall == false && isHoldTheBallComputer)
        {
            Debug.Log("Buraya Giriyor");
            BallRigidbody.velocity = new Vector3(0f, 0f, -115f);
            
            Ball.transform.parent = Everything;
            BallRigidbody.constraints = RigidbodyConstraints.None;
            BallRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            BallCollider.enabled = true;
            isHoldTheBallComputer = false;
        }
        
    }

    public void letsMove()
    {
        StartCoroutine(getRandomTimeAndMove(randomTime)); //Bu fonksiyon �al��t�ktan x saniye sonra alt sat�ra ge�er ve 
    }
    /*// Update is called once per frame
    void Update()
    {
        
    }
    */
}
