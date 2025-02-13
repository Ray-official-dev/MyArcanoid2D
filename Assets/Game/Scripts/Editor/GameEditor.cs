using System.Reflection;
using UnityEditor;
using UnityEngine;
using System;

public static class GameEditor
{
    [MenuItem("Game / UnlockAllLevels")]
    public static void SetLevelToMax()
    {
        Storage.SaveLastLevel(9);
    }

    [MenuItem("Game / NextLevel")]
    private static void CallNonStaticMethod()
    {
        // ����� ������ � ����������� MyComponent � �����
        var target = GameObject.FindAnyObjectByType<GameRules>();

        if (target == null)
            throw new InvalidOperationException();

        Type type = typeof(GameRules);

        MethodInfo method = type.GetMethod("BricksFinished", BindingFlags.Instance | BindingFlags.NonPublic);

        if (method == null)
            throw new InvalidOperationException();

        // �������� ����� � ���������� �������
        method.Invoke(target, null);
    }
}
