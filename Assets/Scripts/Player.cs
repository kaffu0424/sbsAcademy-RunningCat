using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    bool click = false;
    bool onGround = true;
    Camera MainCamera;
    public Animator animator;
    public int score = 0;
    public float limitSpeed,distance;
    public GameObject getEffect,runEffect,wallCrashEffect;
    public TextMeshProUGUI scoreText,speedText,distanceText;
    public Slider speedSlider, progressSlider;

    void Start()
    {
        animator.SetFloat("speed", Global.ScrollSpeed);
        MainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        runEffect.SetActive(false);
    }

	void Update ()
    {

        speedSlider.value = Global.ScrollSpeed / limitSpeed;
        speedText.text = Global.ScrollSpeed.ToString("00");        
        distance+= (Global.ScrollSpeed*0.01f);
        distanceText.text = distance + "m";
        progressSlider.value = distance / GameManager.instance.goalDistance;

        animator.SetFloat("speed", Global.ScrollSpeed);
        if (distance >= GameManager.instance.goalDistance)
        {
            GameManager.instance.finalScore = score;
            GameManager.instance.StageClear();
        }


        if(Input.GetMouseButton(1))
        {
            Global.ScrollSpeed = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            //animator.SetBool("FastRun", true);
            runEffect.SetActive(true);

            if (GetComponent<Rigidbody>().velocity.y == 0)
            {
                if (Global.ScrollSpeed < limitSpeed)
                {
                    Global.ScrollSpeed += 1f * Time.deltaTime;
                }
                click = true;                
            }            
        }
        else
        {           
            if (click)
            {               
                GetComponent<Rigidbody>().AddForce(Vector3.up * Global.ScrollSpeed * 50f);
                //animator.SetBool("Jump", true);
                //animator.SetBool("FastRun", false);
            }

            if (Global.ScrollSpeed > 10f)
            {
                Global.ScrollSpeed -= 10f * Time.deltaTime;
            }
            click = false;
            //animator.SetBool("FastRun", false);
            runEffect.SetActive(false);
        }

        //if(Global.ScrollSpeed<1)
        //{
        //    Global.ScrollSpeed = 1; //최저값 제한
        //}

        if (Global.ScrollSpeed >= 10f)
            MainCamera.fieldOfView = 80f + ((Global.ScrollSpeed - 10f) * 4f);
        else
            MainCamera.fieldOfView = 80f;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Ground")
        {
            onGround = true;
           //animator.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name=="Ground")
        {
            onGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("lightBall"))
        {
            GameObject GetEffect=Instantiate(getEffect, transform.position, Quaternion.identity)as GameObject;
            Destroy(GetEffect, 1.6f);
            score++;
            scoreText.text = score.ToString();
        }

        if (other.gameObject.CompareTag("wall"))
        {
            GameObject WallCrashEffect = Instantiate(wallCrashEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(WallCrashEffect, 2);
            Global.ScrollSpeed -= 6;    //벽과 부딪혔을 때의 페널티
            StartCoroutine(cameraITween());
        }
    }

    IEnumerator cameraITween()
    {
        iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("x", 0.4, "y", 0.4, "time", 0.5f));
        yield return new WaitForSeconds(0.5f);
        MainCamera.transform.localPosition = Vector3.zero;
        MainCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
