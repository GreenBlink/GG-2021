using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogs : MonoBehaviour
{
    [SerializeField] private List<string> dialogs;
    [SerializeField] private List<string> dLines;
    [SerializeField] private List<float> dDelay;

    [SerializeField] private GameObject dialog1;
    [SerializeField] private GameObject dialog2;
    [SerializeField] private GameObject dialog3;
    private float delayTexts = 0f;

    // Start is called before the first frame update
    void Start()
    {
        dialog1.SetActive(false);
        dialog2.SetActive(false);
        dialog3.SetActive(false);
        delayTexts = Random.Range(5f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (dDelay.Capacity == 0)
        {
            delayTexts -= Time.deltaTime;
            if (delayTexts <= 0f) dialogParse(dialogs[Random.Range(0, dialogs.Capacity)]);
        }

        if (dDelay.Capacity > 0)
        {
            for (int i = 0; i < dDelay.Capacity;i++)
            {
                dDelay[i] -= Time.deltaTime;
            }

            for (int i = 0; i < dDelay.Capacity; i++)
            {
                if (dDelay[i] <= 0)
                {
                    if (dLines[i].Contains("WIZ1")) sayPlayer1(dLines[i].Split(':')[1]);
                    if (dLines[i].Contains("WIZ2")) sayPlayer2(dLines[i].Split(':')[1]);
                    if (dLines[i].Contains("PIEC")) sayPiece(dLines[i].Split(':')[1]);
                    dLines.RemoveAt(i);
                    dDelay.RemoveAt(i);
                    i--;
                }
            }

            if (dDelay.Capacity<= 0) delayTexts = Random.Range(15f, 25f);
        }
    }

    private void dialogParse(string dialog)
    {
        string[] texts = dialog.Split('\n');
        float delay = 0f;
        foreach(string txt in texts){
            dLines.Add(txt);
            dDelay.Add(delay);
            delay += 3f;
        }
    }

    public void sayPlayer1(string txt)
    {
        if (txt.Contains("END")) dialog1.SetActive(false);
        else
        {
            dialog1.SetActive(true);
            dialog1.GetComponentInChildren<Text>().text = txt;
        }
    }

    public void sayPlayer2(string txt)
    {
        if (txt.Contains("END")) dialog2.SetActive(false);
        else
        {
            dialog2.SetActive(true);
            dialog2.GetComponentInChildren<Text>().text = txt;
        }
    }

    public void sayPiece(string txt)
    {
        if (txt.Contains("END")) dialog3.SetActive(false);
        else
        {
            GameObject[] p = GameObject.FindGameObjectsWithTag("Piece");
            GameObject chosen = p[Random.Range(0,p.Length)];
            dialog3.transform.position = chosen.transform.position;
            dialog3.SetActive(true);
            dialog3.GetComponentInChildren<Text>().text = txt;
        }
    }
}
