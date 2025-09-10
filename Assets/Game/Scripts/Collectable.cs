using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[HelpURL("https://doc.clickup.com/9017157017/p/h/8cqdtct-30337/d473739efa49988")]
public class Collectable : MonoBehaviour
{
    [SerializeField] private AnimationCurve movementCurve;
    [SerializeField] private float movementSpeed = 2.5f;

    [Header("Events")]
    [SerializeField] private UnityEvent _onSpawn;
    [SerializeField] private UnityEvent _onStartMove;
    [SerializeField] private UnityEvent _onCollect;

    Vector3 startPoint;
    private void Awake()
    {
        startPoint = transform.position;
        _onSpawn.Invoke();
    }
    public void Collect()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToCollect());
    }
    private IEnumerator MoveToCollect()
    {
        var player = FindObjectOfType<Player>();
        float lerpValue = 0;

        _onStartMove.Invoke();

        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime * movementSpeed;
            transform.position = Vector3.Lerp(startPoint, player.transform.position, movementCurve.Evaluate(lerpValue));
            yield return new WaitForEndOfFrame();
        }
        _onCollect.Invoke();
        player.Anim.SetTrigger("Collect");

        yield return new WaitForSeconds(10);
        _onSpawn.Invoke();
        transform.position = startPoint;
    }
}
