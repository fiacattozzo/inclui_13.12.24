using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoseDetector : MonoBehaviour
{
    public List<ActiveStateSelector> poses;  // Lista de gestos o poses
    public TextMeshProUGUI text;  // Referencia al componente TextMeshProUGUI para UI (o TextMeshPro si es 3D)

    // Start is called before the first Frame update
    void Start()
    {
        // Verificar si las poses están correctamente asignadas
        if (poses == null || poses.Count == 0)
        {
            Debug.LogError("¡No se han asignado poses en la lista!");
            return;
        }

        // Depuración: Verifica si se están cargando todas las poses
        Debug.Log("Poses cargadas: " + poses.Count);

        // Recorremos todas las poses
        foreach (var item in poses)
        {
            // Depuración: Verifica cada pose
            Debug.Log("Pose detectada: " + item.gameObject.name);  // Verifica que todas las poses están siendo cargadas

            // Asignar los eventos de selección y deselección
            item.WhenSelected += () =>
            {
                // Depuración: Mensaje cuando un gesto es seleccionado
                Debug.Log("Gesto seleccionado: " + item.gameObject.name);
                SetTextToPoseName(item.gameObject.name);
            };

            item.WhenUnselected += () =>
            {
                // Depuración: Mensaje cuando un gesto es deseleccionado
                Debug.Log("Gesto deseleccionado");
                SetTextToPoseName("");  // Limpiar texto cuando se deselecciona
            };
        }
    }

    // Cambiar el texto del componente TextMeshPro
    private void SetTextToPoseName(string newText)
    {
        if (text != null)
        {
            text.text = newText;  // Establecer el texto dinámicamente
        }
        else
        {
            Debug.LogError("¡El componente TextMeshPro no está asignado!");
        }
    }
}
