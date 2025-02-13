using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using Tools;

public class BallsContainer : MonoBehaviour
{
    public static event Action OnAllBallsDestroyed;

    [SerializeField] private Ball _prefab;

    private List<Ball> _balls;

    private void Awake()
    {
        _balls = new List<Ball>();
    }

    private void OnEnable()
    {
        Effect.OnAdd += EffectAdded;
    }

    private void OnDisable()
    {
        Effect.OnAdd -= EffectAdded;
    }

    private void EffectAdded(Effect effect)
    {
        if (effect.Type != Effect.EffectType.Multiball)
            return;

        StopAllCoroutines();
        StartCoroutine(DeleteMultiballsAfterTime(effect.Duration));

        if (_balls.Count > 1)
            return;

        for (int count = 2; count > 0; count--)
        {
            var ball = CreateBall();
            _balls.Add(ball);
            ball.Push(Extensions.GetRandomDirection());
            ball.transform.position = _balls[0].transform.position;
            ball.OnDestroying += BallDestroeyd;
        }

        StartCoroutine(DeleteMultiballsAfterTime(effect.Duration));
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
        ball.OnDestroying += BallDestroeyd;

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
