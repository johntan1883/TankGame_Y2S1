using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTank : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (objectToFollow != null)
        {
            rectTransform.anchoredPosition = objectToFollow.localPosition; //sets the anchoredPosition of a UI element (referenced by rectTransform) to match the localPosition of another object (referenced by objectToFollow)
        }
    }
}
