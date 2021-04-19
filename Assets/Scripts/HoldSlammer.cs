using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HoldSlammer : MonoBehaviour
{
    float number;
    public Image PowerBarMask;
    public GameObject slammerPiece;
    // public Text powerIndicator;
    public GameObject highlighter;
    public Slider slider;
    [SerializeField] private float maxPowerBarValue = 100;
    public float currentPowerBarValue;
    public float barSpeed = 1f;
    float frequency = 0.02f;
    public bool isIncreasing;
    public bool isShooting;
    public bool isPositioning;
    bool shootReady;
    int counter;
    public GameObject[] respawns;
    public GameObject respawnLoc;
    void Start()
    {
        respawns = GameObject.FindGameObjectsWithTag("Pog");
        counter = 0;
        isIncreasing = true;
        currentPowerBarValue = 0;
        isPositioning = true;
        isShooting = false;
        shootReady = false;
        StartCoroutine(UpdateBar());
    }
    void Update()
    {
        if (isPositioning)   // pog Movement
        {
            float x = Input.GetAxis("Horizontal");
            //float y = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(x, 0, 0);
            transform.Translate(movement * Time.deltaTime);
        }

        if (Input.anyKey)
        {
            highlighter.SetActive(true); // pog show after going away
        }
        // powerIndicator.text = $"Power: {currentPowerBarValue.ToString("F0")}";
        slider.value = currentPowerBarValue / maxPowerBarValue; // just change value of the power
        Debug.Log(isShooting);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = true;
            counter++;  // just to keep the coroutine from not going over and over again
            Debug.Log("heyo: " + counter);
            if (counter == 1)
            {
                StartCoroutine(UpdateBar());
                highlighter.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isShooting = false;
            Debug.Log("hello");
            StartCoroutine(launchPog());
            counter = 0;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            LaunchVoid(120f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKeyDown(KeyCode.Z))
            currentPowerBarValue++;
    }

    IEnumerator launchPog()
    {
        float _currentPowerBarValue = currentPowerBarValue;
        highlighter.SetActive(false); // removes the highlighted pog marker so no confusion
        yield return new WaitForSeconds(0.02f);
        LaunchVoid(_currentPowerBarValue);
        StopCoroutine(UpdateBar()); // stops the update bar from going
        currentPowerBarValue = 0; // resets bar so it would look like it byebye
        yield return new WaitForSeconds(4f);
        PogRespawn();

    }

    IEnumerator UpdateBar()
    {
        while (isShooting)
        {
            float fill = currentPowerBarValue / maxPowerBarValue;
            PowerBarMask.fillAmount = fill;
            if (!isIncreasing)
            {
                currentPowerBarValue -= barSpeed;
                if (currentPowerBarValue <= 0)
                {
                    isIncreasing = true;
                }
            }
            if (isIncreasing)
            {
                currentPowerBarValue += barSpeed;
                if (currentPowerBarValue >= maxPowerBarValue)
                {
                    isIncreasing = false;
                }
            }
            yield return new WaitForSeconds(frequency);
        }
        yield return null;
    }

    void ResetBar()
    {
        highlighter.SetActive(true);
    }
    void LaunchVoid(float power)
    {
        GameObject pog = Instantiate(slammerPiece, transform.position, Quaternion.identity);
        pog.GetComponent<Rigidbody>().AddForce(Vector3.down * power, ForceMode.Impulse);
        Destroy(pog, 4);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Finish")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 100f, ForceMode.Impulse);
        }
    }

    public void PogRespawn()
    {
        foreach (GameObject respawn in respawns)
        {
            StartCoroutine(TransferPog(respawn));
        }
        Debug.Log("hi");

    }

    IEnumerator TransferPog(GameObject respawn)
    {
        respawn.transform.position = respawnLoc.transform.position;
        Debug.Log("hi2");
        yield return new WaitForSeconds(0.5f);

    }


}
