using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleDescription : MonoBehaviour
{
    [SerializeField] private Text m_name;
    [SerializeField] private Text m_description;

    public void Initiate(Rule rule)
    {
        m_name.text = rule.m_name;
        m_description.text = rule.m_description;
    }
}
