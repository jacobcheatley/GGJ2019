using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggTimer : MonoBehaviour
{
    public GameObject canvas;
    public Text text;
    public Image image;
    public GameObject pivot;
    public AudioClip ring;
    public AudioClip tick;
    public AudioSource source;

    private int lastNumber = 0;

    private void Start()
    {
        canvas.transform.parent = transform.parent;
    }

    public void SetTicking(float timeRemaining)
    {
        StartCoroutine(Tick(timeRemaining));
    }

    private IEnumerator Tick(float timeRemaining)
    {
        float startTime = timeRemaining;
        while (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
            text.text = Mathf.Ceil(timeRemaining).ToString();
            image.fillAmount = timeRemaining / startTime;
            pivot.transform.localRotation = Quaternion.Euler(timeRemaining / startTime * 360, 0, 0);
            if ((int)Mathf.Ceil(timeRemaining) != lastNumber) {
                lastNumber = (int)Mathf.Ceil(timeRemaining);
                source.PlayOneShot(tick, 0.15f);
            }
            yield return null;
        }

        source.PlayOneShot(ring);
        GameManager.instance.EndGame();
    }

    private void Update()
    {
        // Update canvas
        canvas.transform.position = transform.position + Vector3.up * 0.2f;
        Vector3 cameraRotation = GameManager.instance.cameraTransform.rotation.eulerAngles;
        canvas.transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
    }
}
