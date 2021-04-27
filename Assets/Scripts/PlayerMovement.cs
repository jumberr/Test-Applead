using System.Collections;
using Classes;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private int numberOfLines = 5;
    [SerializeField] private float speed;

    private Transform cachedTransform;
    private int[] platformLines;
    private int currentLine = 2;

    private readonly Vector3 direction = Vector3.right;
    private const int SIDE_STEP = 5;
    private const float DURATION = 0.25f;
    
    private void Awake()
    {
        DOTween.Init();
        
        cachedTransform = transform;

        platformLines = new int[numberOfLines];
        for (var i = 0; i < platformLines.Length; i++)
        {
            platformLines[i] = i;
        }
    }

    private void Update()
    {
        characterController.Move(direction * (speed * Time.deltaTime));
    }

    public void ChangeLine(int index)
    {
        var temp = currentLine + index;
        if (temp > platformLines.Length - 1 || temp < 0) return;
        currentLine += index;

        // changing player pos
        var position = cachedTransform.position;
        position = new Vector3(position.x, position.y, position.z + index * SIDE_STEP);
        
        cachedTransform.DOMove(position, DURATION);
    }
}