﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;
    AudioSource audioData;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);

        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 13)
        {
            
            winText.text = "You Win!";
            IsWin.Instance.AddWin();
            IsWin.Instance.trapHouseWin = true;
            StartCoroutine("Wait");
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        
        SceneManager.LoadScene("Main");

    }
}