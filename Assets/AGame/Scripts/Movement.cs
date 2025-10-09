using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform bodyPrefab;
    public Vector3 direction = Vector3.up;
    public float cellSize = 1.0f;
    public float speed = 5;
    private float moveTime = 0;
    public List<Transform> bodyParts = new List<Transform>();
    public int sizeBody;
    public bool isTriggerLeftBoundary = false;
    public bool isTriggerRightBoundary = false;
    private float defaultSpeed = 5;
    public float speedSlow = 30f;
    public Transform bodyPartsParent;
    public Vector2 beginPos;
    public Sprite[] bodySprites;
    public GameObject Guide;

    private void Awake()
    {
        beginPos = transform.position;
    }

    private void Start()
    {

        //throw new NotImplementedException();
        defaultSpeed = speed;
        EventDispatcher.RegisterListener(EventID.EnableMovement, OnEnableMovement);
        EventDispatcher.RegisterListener(EventID.ResetGame, OnResetPosBegin);
        Guide.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveListener(EventID.EnableMovement, OnEnableMovement);
        EventDispatcher.RemoveListener(EventID.ResetGame, OnResetPosBegin);
    }

    private void OnDisable()
    {
        CancelAllTweens();
    }

    private void Update()
    {
        Move();
        ChangeDicrection();
    }

    private void OnResetPosBegin(object data)
    {
        Debug.Log("reset position");
        this.transform.position = beginPos;
        Debug.Log("target pos "+ beginPos);
        //this.GetComponent<SpriteRenderer>().sprite = lstHelmetPlayerSprite[SessionPref.CurrentItemInUse];
        this.transform.DOMove(beginPos, 0.01f).OnComplete(() =>
        {
            Debug.Log("reset position complete");
            //CameraController.Instance.InitProCamera(null);
            ArrangeBodyParts(.1f);
            //Guide.SetActive(true);
        });
    }

    private void OnEnableMovement(object data)
    {
        if(data is not bool) return;

        bool isEnable = (bool) data;
        Debug.Log("enable movement " + isEnable);
        this.enabled = isEnable;

        if (isEnable)
        {
            speed = defaultSpeed;
        }
    }

    public void ChangeDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void OnClickLeft()
    {
        if (isTriggerLeftBoundary)
        {
            Debug.Log("Left");
            direction = Vector3.up;
            return;
        }
        Debug.Log("Left 1");
        direction = Vector3.left;
    }

    public void OnClickRight()
    {
        if (isTriggerRightBoundary)
        {
            Debug.Log("Right");
            direction = Vector3.up;
            return;
        }
        Debug.Log("Right 1");
        direction = Vector3.right;
    }

    public void OnClickUp()
    {
        direction = Vector3.up;
    }

    public void ChangeDicrection()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.y == -1)
        {
            direction = Vector2.down;
        }else if (input.y == 1)
        {
            direction = Vector2.up;
        }else if (input.x == -1)
        {
            direction = Vector2.left;
        }else if (input.x == 1)
        {
            direction = Vector2.right;
        }
    }

    void Move()
    {
        if(bodyParts.Count < sizeBody)
        {
            StartCoroutine(GrowBody());
        }

        if (Time.time > moveTime)
        {



            //StartCoroutine(SmoothMove(targetPosition));
            moveTime = Time.time + 1 / speed;
        }
        Vector3 targetPosition = transform.position + direction * cellSize;

        StartCoroutine(SmoothMoveBodyParts());
        StartCoroutine(SmoothMove(targetPosition));
    }

    void UpdateBodyParts()
    {
        if (bodyParts.Count == 0) return;

        // Di chuyển từng phần thân theo vị trí của phần trước nó
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            bodyParts[i].position = Vector3.Lerp(bodyParts[i].position, bodyParts[i - 1].position, Time.deltaTime * speed);
        }
        // Phần thân đầu tiên đi theo đầu
        bodyParts[0].position = Vector3.Lerp(bodyParts[0].position, transform.position, Time.deltaTime * speed);
    }

    private bool isCompleteSpeed;

    IEnumerator SmoothMoveBodyParts()
    {
        if (speed == defaultSpeed)
        {
            isChangeSpeed = false;
        }
        float duration = 1 / speed;
        List<Vector3> startingPositions = new List<Vector3>();
        for (int i = 0; i < bodyParts.Count; i++)
        {
            startingPositions.Add(bodyParts[i].position);
        }

        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            if (i - 1 >= 0 && i < startingPositions.Count)
            {
                bodyParts[i].DOMove(startingPositions[i - 1], duration).SetEase(Ease.Linear).OnUpdate(() =>
                {
                    if(speed == speedSlow && !isChangeSpeed)
                    {
                        isChangeSpeed = true;
                        bodyParts[i].transform.DOKill();
                    }
                });
            }
        }
        if (bodyParts.Count > 0)
        {
            bodyParts[0].DOMove(transform.position, duration).SetEase(Ease.Linear);
        }

        yield return new WaitForSeconds(duration);
    }

    bool isMoving = false;
    private bool isChangeSpeed;
    IEnumerator SmoothMove(Vector3 targetPosition)
    {
        //Debug.Log("start move");
        transform.DOMove(targetPosition, 1 / speed).SetEase(Ease.Linear).OnStart(() => { }).OnComplete(() =>
        {
            isMoving = false;
            //Debug.Log("completed move");
        }).OnUpdate(() => { isMoving = true; });
        yield return new WaitForSeconds(1 / speed);
    }

    IEnumerator GrowBody()
    {
        Vector2 position = transform.position;
        if (bodyParts.Count != 0)
        {
            position = bodyParts[bodyParts.Count - 1].position;
        }
        var bodyPart = Instantiate(bodyPrefab, position, Quaternion.identity, this.transform.parent);
        bodyParts.Add(bodyPart.transform);
        bodyPart.GetComponent<SpriteRenderer>().sprite = bodySprites[SessionPref.CurrentItemInUse];
        yield return new WaitForSeconds(1);
    }

    public void ArrangeBodyParts(float distance)
    {
        if (bodyParts.Count == 0) return;

        Vector3 currentPosition = transform.position - direction * distance;
        for (int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].position = currentPosition;
            bodyParts[i].GetComponent<SpriteRenderer>().sprite = bodySprites[SessionPref.CurrentItemInUse];
            currentPosition -= direction * distance;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BoundRight"))
        {
            isTriggerRightBoundary = true;
            direction = Vector3.up;
        }else if(other.CompareTag("BoundLeft"))
        {
            isTriggerLeftBoundary = true;
            direction = Vector3.up;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BoundRight"))
        {
            isTriggerRightBoundary = false;
        }else if(other.CompareTag("BoundLeft"))
        {
            isTriggerLeftBoundary = false;
        }
    }

    public void SetSpeedSlowPlayer()
    {
        this.speed = speedSlow;

        Invoke(nameof(SetSpeedDefault), 5);
    }

    private void SetSpeedDefault()
    {
        this.speed = defaultSpeed;
    }

    private void CancelAllTweens()
    {
        DOTween.KillAll();
    }
}
