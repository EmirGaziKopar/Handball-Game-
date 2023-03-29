using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Transform sceneTransform;

    // public GameObject normalCamera, ballCamera;    Topu attýktan sonra topu takip eden kamera düþüncesi düþüncesi
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

    //baslangýcta true yaptýk çünkü karakterin elindeyken rakip oyuncu topa yönelmesin diye. Zaten rakibin elindeyken çarpýþmalarý sonucu score yazmasý da mantýksýz olurdu
    public static bool isFastBall = true; //Bu deðiþken karakterimiz topu attýðý zaman rakip oyuncuya top doðrudan temas ederse true kalacak ve score yazýlacak
                                           //ama eðer top arkadan veya yere sektikten sonra rakibe gelirse o zaman bu deðer false olacak ve score olarak etki etmeyecek
                                           //ayrýca bu deðer true olduðu süre boyunca rakip oyuncu topu eline alamaz. 
                                           //computer içerisinde de benzer bir þekilde isFastBall2 oluþturulmalý ve rakip karakter için de ayný adýmlar izlenmelidir.
    
    

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
        isHoldBall = true; //Top kimin elinden baþlýyorsa onun referansýnda bu deðer true olmalý 
        //isHoldBallPlayer2 = true; //Top kimin elinden baþlýyorsa onun referansýnda bu deðer true olmalý (Bu bir static deðer olduðu için ayrý ayrý referanslandýrýlamaz bu nedenle 2 tane ayrý static deðiþken atadýk 2.oyuncu için)
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
            BallCollider.enabled = false; //Top eldeyken karaktere çarpmasýn diye
        }
        
        if (isHoldBall == true)
        {
            rigidbody.Sleep();
            BallCollider.enabled = false; //Top eldeyken karaktere çarpmasýn diye
        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("shot") && isHoldBall) //Bu kod burada yazan mevcut animasyon yürütüldüðü sýrada animasyonun ortalarýnda 1 kez çalýþýr ve true deðeri verir.
        {
            sayac1++;
            //Debug.Log("Animasyon Runnn : "+sayac1);
            time += Time.deltaTime; //Animasyonda karakterin topu elinden çýkarmasý için gereken süre
            if (time < 0.1) //Burada yazan kodlar gereksiz çünkü zaten bu satýrlar 1 kez çaðrýlýr.
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
                BallMovement.isHoldBall = false; //Top elden çýktý
                BallCollider.enabled = true; //Top elden çýktýktan sonra tekrar çarpýþabilir. Top eldeyken çarpýþabilir olmasý karakterin yüzeyine çarpýp hatalý çalýþmalara sebep oluyor.
                //isHoldBall sisteminin yapýlma sebebi karakterde ve topta ayný anda rigidbody olduðunda birbirlerine enerji aktarýp oyun içinde hataya sebep oluyor olmalarýdýr.
                isFastBall = true;
            }
            
        }
        else
        {

            
            
            time = 0f;      
            Debug.Log("playing");
             //karakter top atýþý yaparken hareket dursun diye

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
