using UnityEngine;
using TMPro;

public class CameraInfo : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI fpsText;

    [Header("Settings")]
    public float updateInterval = 0.5f; // Met à jour le texte toutes les 0.5 secondes

    private float accum = 0f; // Accumulation du temps
    private int frames = 0; // Compteur de frames
    private float timeLeft; // Temps restant avant la prochaine mise à jour

    void Start()
    {
        timeLeft = updateInterval;
    }

    void Update()
    {
        // S'il n'y a pas de text, on passe
        if (fpsText == null)
            return;

        // On utilise unscaledDeltaTime pour que le calcul reste précis même si le jeu est en pause ou au ralenti
        timeLeft -= Time.unscaledDeltaTime;
        accum += Time.unscaledDeltaTime;
        frames++;

        // Quand l'intervalle est écoulé, on met à jour l'affichage
        if (timeLeft <= 0.0)
        {
            float fps = frames / accum;

            // Affiche le FPS sans décimales
            fpsText.text = string.Format("{0:F0} FPS", fps);

            if (fps >= 72f)
                fpsText.color = Color.green;
            else if (fps >= 60f)
                fpsText.color = Color.yellow;
            else
                fpsText.color = Color.red;

            // Réinitialisation pour le prochain calcul
            timeLeft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}