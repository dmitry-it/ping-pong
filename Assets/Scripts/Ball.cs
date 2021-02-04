using System.Collections;
using System.Linq;
using Settings;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private CircleCollider2D ballCollider;
    [SerializeField] private float size;
    [SerializeField] private Color color;
    [SerializeField] private float speed = 1f;
    public UnityEvent contactWithFinishZoneEvent = new UnityEvent();
    public readonly CountableSurfacesContactEvent CountableSurfacesContactEvent = new CountableSurfacesContactEvent();
    private Vector3 _force = Vector3.zero;

    private void Awake()
    {
        Assert.IsNotNull(image);
        Assert.IsTrue(size > 0);
        Assert.IsTrue(speed > 0);
    }

    private void Start()
    {
        image.color = GameSettings.Instance.IsCustomBallColorInUse
            ? GameSettings.Instance.CustomBallColor
            : color;

        gameObject.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        gameObject.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
    }

    private void Update()
    {
        transform.position += _force * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var contactPoint2D = other.contacts.FirstOrDefault();
        _force = Vector3.Reflect(_force, contactPoint2D.normal);
        var countable = other.gameObject.GetComponent<CountableSurface>();
        if (countable != null) CountableSurfacesContactEvent.Invoke(countable.AmountOfPoints);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<FinishRoundZone>() != null) contactWithFinishZoneEvent.Invoke();
    }

    /// <summary>
    ///     Start ball sliding
    /// </summary>
    public void Punch(int startDelay)
    {
        StartCoroutine(PunchBallWithDelay(startDelay));
    }

    private IEnumerator PunchBallWithDelay(int startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        GenerateForce();
    }

    private void GenerateForce()
    {
        var direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        direction /= direction.magnitude;
        _force = direction * speed;
    }
}