using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Slammer : MonoBehaviour
{
    float number;
    public GameObject slammerPiece;
    public Text powerIndicator;
    public Slider slider;
    public GameObject highlighter;
    float maxPowerBarValue = 150;
    public float currentPowerBarValue;
    public float barSpeed = 1f;
    float frequency = 0.02f;
    public bool isIncreasing;
    public bool isShooting;
    public bool isPositioning;
    bool shootReady;

    void Start()
    {
        isIncreasing = true;
        currentPowerBarValue = 0;
        isPositioning = true;
        isShooting = false;
        shootReady = true;

    }
    void Update()
    {
        if (isPositioning)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(x, 0, y);
            transform.Translate(movement * Time.deltaTime);
        }

        powerIndicator.text = $"Power: {currentPowerBarValue.ToString("F0")}";
        slider.value = currentPowerBarValue;
        Debug.Log(isShooting);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPositioning = !isPositioning;
            if (isShooting)
            {
                StartCoroutine(launchPog());
                isShooting = false;
            }
            if (!isShooting)
            {
                ResetBar();
            }
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
        highlighter.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        LaunchVoid(_currentPowerBarValue);
        StopCoroutine(UpdateBar());
        currentPowerBarValue = 0;

    }

    IEnumerator UpdateBar()
    {
        while (isShooting)
        {
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
            Debug.Log("fuck");
        }
        yield return null;
        Debug.Log("fuck");
    }

    void ResetBar()
    {
        highlighter.SetActive(true);
    }

    public void startButton()
    {
        isShooting = true;
        StartCoroutine(UpdateBar());

    }
    void LaunchVoid(float power)
    {
        GameObject pog = Instantiate(slammerPiece, transform.position, Quaternion.identity);
        pog.GetComponent<Rigidbody>().AddForce(Vector3.down * power, ForceMode.Impulse);
        Destroy(pog, 4);
    }
}
