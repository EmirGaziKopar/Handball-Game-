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
               
            }

        }
    }

    private void Update()
    {
        //s�n�r �izgiler belirlenecek distance de�eri o de�ere 0.1f'den yak�n oldu�u durumda karakter otomatik olarak y�n de�i�tirecek yani moveRight fonksiyonuna etki edilecek
        if (sayac < 1)
        {
            Debug.Log("Burasasdsadad�");
            sayac++;
            randomTime = Random.Range(0.2f, 0.5f);//1,2
            moveRight = Random.Range(0, 2); //0 or 1; Buradan gelen de�ere g�re animasyon de�erlerini olu�tur (Blend tree ile)           
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
        this.sayac = 0;//Bunu yaparak belirlenen rastgele s�re bitti�inde tekrar rakibin hareket etmesini sa�lar�z.
        randomTime = 0;
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
