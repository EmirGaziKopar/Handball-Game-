using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Transform sceneTransform;

    // public GameObject normalCamera, ballCamera;    Topu att�ktan sonra topu takip eden kamera d���ncesi d���ncesi
       Transform Bodytransform;
    [SerializeField] float speed;
    Animator anim;
    [SerializeField] GameObject Character;
    new Rigidbody rigidbody;
    bool isFlaying = false;
    new ParticleSystem particleSystem;
    [SerializeField] GameObject particleSystemPointer;
    public float baslangic = 0f;
    [SerializeField] GameObject cube;
    CatchBall catchBall;
    float AnimTime;
    public static bool isHoldBall;
    int sayac;
    public Collider BallCollider;
    int sayac1;

    //baslang�cta true yapt�k ��nk� karakterin elindeyken rakip oyuncu topa y�nelmesin diye. Zaten rakibin elindeyken �arp��malar� sonucu score yazmas� da mant�ks�z olurdu
    public static bool isFastBall = true; //Bu de�i�ken karakterimiz topu att��� zaman rakip oyuncuya top do�rudan temas ederse true kalacak ve score yaz�lacak
                                           //ama e�er top arkadan veya yere sektikten sonra rakibe gelirse o zaman bu de�er false olacak ve score olarak etki etmeyecek
                                           //ayr�ca bu de�er true oldu�u s�re boyunca rakip oyuncu topu eline alamaz. 
                                           //computer i�erisinde de benzer bir �ekilde isFastBall2 olu�turulmal� ve rakip karakter i�in de ayn� ad�mlar izlenmelidir.
    
    

    float time = 0f;
   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("stadium"))
        {
            Debug.Log(CatchBall.isTouching);
            CatchBall.isTouching = false;
           // Debug.Log(CatchBall.isTouching);

        }



    }
    private void OnCollisionEnter(Collision collision)
    {
        isFastBall = false;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "character")
        {
            if(sayac < 1)
            {
                sayac++;
                rigidbody.velocity = new Vector3(0f, 0.1f, 0f);
            }
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "character")
        {
            sayac = 0;

        }
    }
    */
    private void Start()
    {
        isHoldBall = true; //Top kimin elinden ba�l�yorsa onun referans�nda bu de�er true olmal� 
        //isHoldBallPlayer2 = true; //Top kimin elinden ba�l�yorsa onun referans�nda bu de�er true olmal� (Bu bir static de�er oldu�u i�in ayr� ayr� referansland�r�lamaz bu nedenle 2 tane ayr� static de�i�ken atad�k 2.oyuncu i�in)
        catchBall = GetComponent<CatchBall>();
        anim = Character.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        Bodytransform = cube.GetComponent<Transform>();
        particleSystem = particleSystemPointer.GetComponent<ParticleSystem>();
        var emission = particleSystem.emission;
        emission.rateOverTime = baslangic;
    }


    
    
    private void FixedUpdate()
    {
        
        if(ComputerController.isHoldTheBallComputer == true)
        {
            rigidbody.Sleep();
            BallCollider.enabled = false; //Top eldeyken karaktere �arpmas�n diye
        }
        
        if (isHoldBall == true)
        {
            rigidbody.Sleep();
            BallCollider.enabled = false; //Top eldeyken karaktere �arpmas�n diye
        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("shot") && isHoldBall) //Bu kod burada yazan mevcut animasyon y�r�t�ld��� s�rada animasyonun ortalar�nda 1 kez �al���r ve true de�eri verir.
        {
            sayac1++;
            //Debug.Log("Animasyon Runnn : "+sayac1);
            time += Time.deltaTime; //Animasyonda karakterin topu elinden ��karmas� i�in gereken s�re
            if (time < 0.1) //Burada yazan kodlar gereksiz ��nk� zaten bu sat�rlar 1 kez �a�r�l�r.
            {
                
                Debug.Log(time);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                //Debug.Log("not playing");
                transform.parent = sceneTransform.transform;
                rigidbody.constraints = RigidbodyConstraints.None;
                rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                
                
                transform.Translate(transform.forward * Time.deltaTime * speed);
                shot();
                var emission = particleSystem.emission;
                emission.rateOverTime = 500f;
                BallMovement.isHoldBall = false; //Top elden ��kt�
                BallCollider.enabled = true; //Top elden ��kt�ktan sonra tekrar �arp��abilir. Top eldeyken �arp��abilir olmas� karakterin y�zeyine �arp�p hatal� �al��malara sebep oluyor.
                //isHoldBall sisteminin yap�lma sebebi karakterde ve topta ayn� anda rigidbody oldu�unda birbirlerine enerji aktar�p oyun i�inde hataya sebep oluyor olmalar�d�r.
                isFastBall = true;
            }
            
        }
        else
        {

            
            
            time = 0f;      
            Debug.Log("playing");
             //karakter top at��� yaparken hareket dursun diye

        }
        
        
    }
 /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("stadium") || other.gameObject.CompareTag("character"))
        {
            isFlaying = false;
            normalCamera.gameObject.SetActive(true);
            ballCamera.gameObject.SetActive(false);


        }
        else
        {

            onFly();




        }
    }
   
    
    void onFly()
    {


        normalCamera.gameObject.SetActive(false);
        ballCamera.gameObject.SetActive(true);


    }
 */
    void shot()
    {
        if(isHoldBall == true)
        {
            Vector3 a = new Vector3(Bodytransform.forward.x, 0.0f, Bodytransform.forward.z); //Topun karsiya gitmesini saglayan z. //1.5f eski y vectoru

            rigidbody.velocity = a * speed;
        }
        
    }
}
