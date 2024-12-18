using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction.Input;

public class SelectorMultiple : MonoBehaviour
{
    public string CurrentLeftGesture;
    public string CurrentRightGesture;
    public TextMeshPro NombreGesto;  // Texto para feedback
    public List<GameObject> selectores;  // Lista de gestos configurados
    private List<string> validGestures = new List<string>();  // Lista para guardar gestos completados

    void Start()
    {
        foreach (GameObject selector in selectores)
        {
            if (selector.TryGetComponent<ISelector>(out var selector2))
            {
                if (selector.TryGetComponent<HandRef>(out var hand))
                {
                    if (hand.Hand.Handedness == Handedness.Left)
                    {
                        selector2.WhenSelected += () => OnDetectGesture(selector.name, true);
                        selector2.WhenUnselected += () => OnUndetectGesture(selector.name, true);
                    }
                    else
                    {
                        selector2.WhenSelected += () => OnDetectGesture(selector.name, false);
                        selector2.WhenUnselected += () => OnUndetectGesture(selector.name, false);
                    }
                }
            }
        }
    }

    private void OnDetectGesture(string GestureName, bool isLeftHand)
    {
        if (isLeftHand)
            CurrentLeftGesture = GestureName;
        else
            CurrentRightGesture = GestureName;

        // Validar combinación de gestos
        if (CurrentRightGesture == "PinkyPose_R" && CurrentLeftGesture == "PaperPose_L")
        {
            RegisterGesture("PinkyPaper", true);
        }
        else if (CurrentRightGesture == "PaperPose_R" && CurrentLeftGesture == "PinkyPose_L")
        {
            RegisterGesture("PaperPinky", true);
        }
        else
        {
            NombreGesto.text = GestureName;  // Mostrar el gesto actual
            NombreGesto.color = Color.white;  // Color por defecto
        }
    }

    private void OnUndetectGesture(string GestureName, bool isLeftHand)
    {
        if (isLeftHand)
            CurrentLeftGesture = "";
        else
            CurrentRightGesture = "";
    }

    private void RegisterGesture(string gestureName, bool isCorrect)
    {
        if (!validGestures.Contains(gestureName))  // Evitar duplicados
        {
            validGestures.Add(gestureName);
            NombreGesto.text = gestureName + (isCorrect ? " ✓" : " ✗");  // Feedback de gesto
            NombreGesto.color = isCorrect ? Color.green : Color.red;  // Verde si esta bien, rojo si no
            Debug.Log("Gesto guardado: " + gestureName);
        }
    }

    void Update()
    {
        // Solo para depuracion: Mostrar la lista de gestos completados
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Gestos registrados:");
            foreach (string gesture in validGestures)
            {
                Debug.Log(gesture);
            }
        }
    }
}
