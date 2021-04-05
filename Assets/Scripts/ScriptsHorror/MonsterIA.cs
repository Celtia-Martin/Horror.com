using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIA : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private GameObject monstruo;
    [SerializeField] private Transform player;
    [SerializeField] private AudioClip[] sonidos;
    private string[] animaciones = { "Susto","Saludo"};
    private AudioSource audioSource;
    private Animator animatorController;
    private List<ActionMonster> actions;
    private float[,] stages = {{ 10, 30 },{6,28 },{ 5, 20 },{ 3, 15 },{ 3, 10 },{ 2, 5 } };
    private float minAparicion = 6;
    private float maxAparicion = 10;
    private int currentStage = 5;
    private bool finPartida = false;
    //Auxiliares
    private int secondsRandom,currentTarget;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animatorController = GetComponentInChildren<Animator>();
        monstruo.SetActive(false);
        StartCoroutine(Spawn());
        InicializarAcciones();
    }
    //
    IEnumerator Spawn()
    {
        while (!finPartida)
        {
            secondsRandom = (int)Random.Range(stages[currentStage, 0], stages[currentStage, 1]);
            yield return new WaitForSeconds(secondsRandom);
            monstruo.SetActive(true);
            ActionMonster current = actions[Random.Range(0, actions.Count)];
            current.Actuar(audioSource, animatorController);
            currentTarget= (int) Random.Range(0, targets.Length);
            Vector3 posicionNueva = targets[currentTarget].position;
            posicionNueva.y = transform.position.y;
            transform.position = posicionNueva;
            Vector3 positionPlayer = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(positionPlayer);
            secondsRandom = (int)Random.Range(minAparicion, maxAparicion);
            yield return new WaitForSeconds(secondsRandom);
            monstruo.SetActive(false);
            current.DejarActuar(audioSource, animatorController);

        }
        
    }
    void InicializarAcciones()
    {
        actions = new List<ActionMonster>();
        actions.Add(new ActionMonster(2, animaciones[0], sonidos[0]));
        actions.Add(new ActionMonster(2, animaciones[1], sonidos[0]));
        actions.Add(new ActionMonster(2, animaciones[0], sonidos[1]));
        actions.Add(new ActionMonster(2, animaciones[1], sonidos[1]));
    }

}
public class ActionMonster{
    private int peligrosidad;
    private string animacion;
    private AudioClip sonido;

    public ActionMonster(int peligrosidad, string animacion, AudioClip sonido)
    {
        this.peligrosidad = peligrosidad;
        this.animacion = animacion;
        this.sonido = sonido;
    }
    public void Actuar(AudioSource audio, Animator controller)//quizas meterle tambn objetos voladores ( mas adelante)
    {
        if (animacion != null){
            controller.SetBool(animacion, true);
        }
        if(sonido != null)
        {
            audio.clip = sonido;
            audio.Play();
        }
    }
    public void DejarActuar(AudioSource audio, Animator controller)
    {
        if (animacion != null)
        {
            controller.SetBool(animacion, false);
        }
        if (sonido != null)
        {
            audio.Stop();
        }
    }
}
