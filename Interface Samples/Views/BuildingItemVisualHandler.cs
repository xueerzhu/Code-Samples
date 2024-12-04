using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingItemVisualHandler : MonoBehaviour
{
    [Header("Card")] public BuildingItemView itemView;

    [Header("References")] [SerializeField]
    private Transform shakeParent;

    [SerializeField] private Transform tiltParent;
    [SerializeField] private Image cardImage;

    [Header("Follow Parameters")] [SerializeField]
    private float followSpeed = 30;

    [Header("Rotation Parameters")] [SerializeField]
    private float rotationAmount = 20;

    [SerializeField] private float rotationSpeed = 20;
    [SerializeField] private float autoTiltAmount = 30;
    [SerializeField] private float manualTiltAmount = 20;
    [SerializeField] private float tiltSpeed = 20;


    [Header("Select Parameters")] [SerializeField]
    private float selectPunchAmount = 20;

    [Header("Hober Parameters")] [SerializeField]
    private float hoverPunchAngle = 5;

    [SerializeField] private float hoverTransition = .15f;

    [Header("Swap Parameters")] [SerializeField]
    private bool swapAnimations = true;

    [SerializeField] private float swapRotationAngle = 30;
    [SerializeField] private float swapTransition = .15f;
    [SerializeField] private int swapVibrato = 5;
    [NonSerialized] private Canvas canvas;
    [NonSerialized] private Transform cardTransform;

    [NonSerialized] private float curveRotationOffset;

    [Header("Curve")]
    //[SerializeField] private CurveParameters curve;
    [NonSerialized]
    private float curveYOffset;

    [NonSerialized] private bool initalize;
    [NonSerialized] private Vector3 movementDelta;
    [NonSerialized] private Coroutine pressCoroutine;
    [NonSerialized] private Vector3 rotationDelta;
    [NonSerialized] private int savedIndex;

    private void Update()
    {
        if (!initalize || itemView == null) return;

        HandPositioning();
        SmoothFollow();
        FollowRotation();
    }

    public void Initialize(BuildingItemView target, int index = 0)
    {
        //Declarations
        itemView = target;
        cardTransform = target.transform;
        canvas = GetComponent<Canvas>();

        //Initialization
        initalize = true;
    }

    public void UpdateIndex(int length)
    {
        transform.SetSiblingIndex(itemView.transform.parent.GetSiblingIndex());
    }

    private void HandPositioning()
    {
        /*curveYOffset = (curve.positioning.Evaluate(itemView.NormalizedPosition()) * curve.positioningInfluence) * itemView.SiblingAmount();
        curveYOffset = itemView.SiblingAmount() < 5 ? 0 : curveYOffset;
        curveRotationOffset = curve.rotation.Evaluate(itemView.NormalizedPosition());*/
    }

    private void SmoothFollow()
    {
        var verticalOffset = Vector3.up * (false ? 0 : curveYOffset);
        transform.position = Vector3.Lerp(transform.position, cardTransform.position + verticalOffset,
            followSpeed * Time.deltaTime);
    }

    private void FollowRotation()
    {
        var movement = transform.position - cardTransform.position;
        movementDelta = Vector3.Lerp(movementDelta, movement, 25 * Time.deltaTime);
        var movementRotation = (false ? movementDelta : movement) * rotationAmount;
        rotationDelta = Vector3.Lerp(rotationDelta, movementRotation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
            Mathf.Clamp(rotationDelta.x, -60, 60));
    }
}