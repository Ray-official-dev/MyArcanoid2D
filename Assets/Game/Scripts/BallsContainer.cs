using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;
using System.Collections;

public class BallsContainer : MonoBehaviour
{
    public event Action OnAllBallsDestroyed;

    [SerializeField] private Ball _prefab;

    private List<Ball> _balls;

    private void Awake()
    {
        _balls = new List<Ball>();
    }

    private void OnEnable()
    {
        EventBus.Instance.OnEffectAdded += BaffAdded;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnEffectAdded -= BaffAdded;
    }

    private void BaffAdded(Effect baff)
    {
        if (baff.Type != Effect.EffectType.Multiball)
            return;

        StopAllCoroutines();
        StartCoroutine(DeleteMultiballsAfterTime(baff.Duration));

        if (_balls.Count > 1)
            return;

        for (int count = 2; count > 0; count--)
        {
            var ball = CreateBall();
            _balls.Add(ball);
            ball.Push(GetRandomDirection());
            ball.transform.position = _balls[0].transform.position;
            ball.OnDestroyed += BallDestroeyd;
        }

        StartCoroutine(DeleteMultiballsAfterTime(baff.Duration));
    }

    private IEnumerator DeleteMultiballsAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        IList<Ball> ballsToRemove = new List<Ball>();
        var countBallsToRemove = _balls.Count - 1;

        foreach (var ball in _balls)
        {
            if (ballsToRemove.Count == countBallsToRemove)
                break;

            ballsToRemove.Add(ball);
        }

        foreach (var ball in ballsToRemove)
        {
            _balls.Remove(ball);
            Destroy(ball.gameObject);
        }
    }

    private Vector2 GetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        return new Vector2(randomX, randomY).normalized;
    }

    public void SpawnBall()
    {
        StopAllCoroutines();
        DestroyAllBalls();
        var ball = CreateBall();
        _balls.Add(ball);
    }

    private void DestroyAllBalls()
    {
        foreach (var ball in _balls)
            Destroy(ball.gameObject);

        _balls.Clear();
    }

    private Ball CreateBall()
    {
        var ball = Instantiate(_prefab);
        ball.OnDestroyed += BallDestroeyd;

        return ball;
    }

    private void BallDestroeyd(Ball ball)
    {
        _balls.Remove(ball);

        if (_balls.Count == 0)
            OnAllBallsDestroyed?.Invoke();
    }

    public void PushBall()
    {
        _balls[0].Push(Vector2.up);
    }

    public void Clear()
    {
        foreach (var ball in _balls)
            Destroy(ball.gameObject);

        _balls.Clear();
    }
}