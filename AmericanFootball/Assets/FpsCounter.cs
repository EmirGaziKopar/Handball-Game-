using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FpsCounter : MonoBehaviour
{
    int sayac;
    float fpsTime;
    public Text Fps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sayac++;
        fpsTime += Time.deltaTime;
        if (fpsTime >= 1)
        {
            //fps göster ardýndan fpsTime degerini 0 yap
            Fps.text = "FPS: "+sayac.ToString();
            fpsTime = 0;
            sayac = 0; //sayac gösterme iþlemi bittikten sonra 0'lanýrsa ekranda bir anlýðýna 0 deðeri görünmez.
        }

    }
}
