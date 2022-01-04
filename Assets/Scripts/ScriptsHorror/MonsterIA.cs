using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MonsterIA : MonoBehaviour
{
    //referencias
    [SerializeField] private Transform[] targets;
    [SerializeField] private GameObject monstruo;
    [SerializeField] private Transform player;
    [SerializeField] private AudioClip[] sonidos;
    [SerializeField] private AudioClip susto;
    private AudioSource audioSource;
    private Animator animatorController;
    [SerializeField] private LayerMask pared;
    [SerializeField] private AudioMixer audioMixer;
    //propiedades 
    private string[] animaciones = { "Susto", "Saludo" };
    private List<ActionMonster> actions;
    private float[,] stages = { { 10, 20 }, { 6, 16 }, { 5, 12 }, { 3, 10 }, { 3, 7 }, { 2, 5 } };
    private float minAparicion = 2;
    private float maxAparicion = 8;
    private int currentStage = 0;
    private int contApariciones = 0;
    private const int aparicionesStage = 5;
    private bool finPartida = false;
    private bool asustando = false;
    private float alturaNormal;
    private float alturaSusto = 0.5f;
    private float[] valueAudioMixer = {8000,1000, 800, 750, 500, 250 };
    private int count = 0;
    private int secondsRandom,currentTarget;
    private bool haciendoRuido = false;

 

    private void Start()
    {
        alturaNormal = transform.position.y;
        audioSource = GetComponent<AudioSource>();
        animatorController = GetComponentInChildren<Animator>();
        monstruo.SetActive(false);
        StartCoroutine(Spawn());
        StartCoroutine(ControlarPasoParedes());
        InicializarAcciones();
    }
    //   
    IEnumerator ControlarPasoParedes()
    {
        while (!finPartida)
        {
            yield return new WaitUntil(() => haciendoRuido);
            yield return new WaitForSeconds(0.05f);
            Vector3 direccion =  player.transform.position- transform.position ;
            count = Physics.RaycastAll(transform.position, direccion, 300,pared).Length-1;
            count = Mathf.Max(0, count);
            audioMixer.SetFloat("FiltroParedes",  valueAudioMixer[Mathf.Min(count,valueAudioMixer.Length-1)]);
           

        }
    }
    IEnumerator Spawn()
    {
        while (!finPartida)
        {
            if(asustando)
            {
                StartCoroutine(Asustar());
                yield return new WaitUntil(() => !asustando);
            }
           
            secondsRandom = (int)Random.Range(stages[currentStage, 0], stages[currentStage, 1]);
            yield return new WaitForSeconds(secondsRandom);
            haciendoRuido = true;
            monstruo.SetActive(true);
            ActionMonster current = actions[Random.Range(0, actions.Count)];
            current.Actuar(audioSource, animatorController);
            currentTarget= (int) Random.Range(0, targets.Length);
            Vector3 posicionNueva = targets[currentTarget].position;
            posicionNueva.y = alturaNormal;
            transform.position = posicionNueva;
            Vector3 positionPlayer = new Vector3(player.transform.position.x, alturaNormal, player.transform.position.z);
            transform.LookAt(positionPlayer);
            secondsRandom = (int)Random.Range(minAparicion, maxAparicion);
            yield return new WaitForSeconds(secondsRandom);
            monstruo.SetActive(false);
            current.DejarActuar(audioSource, animatorController);
            contApariciones++;
            if (contApariciones>aparicionesStage && currentStage< stages.Length/2 -1)
            {
                currentStage++;
                contApariciones = 0;
                Debug.Log("currentStage= " + currentStage);
            }
            haciendoRuido = false;
        }
        
    }
    public int getCount()
    {
        return count;
    }
    void InicializarAcciones()
    {
        actions = new List<ActionMonster>();
        for(int i =0; i< sonidos.Length; i++)
        {
            actions.Add(new ActionMonster(2, animaciones[0], sonidos[i]));
            actions.Add(new ActionMonster(2, animaciones[1], sonidos[i]));
        }
        

    }
    IEnumerator  Asustar()
    {
        yield return new WaitForSeconds(Random.Range(1,5));
        Debug.Log("Scream");
        Vector3 positionPlayer = new Vector3(player.transform.position.x, alturaNormal-alturaSusto, player.transform.position.z);
        transform.position= positionPlayer +player.forward*0.5f;
        transform.LookAt(positionPlayer);
        monstruo.SetActive(true);
        audioSource.volume = 20;
        audioSource.clip = susto;
        audioSource.Play();    
        Vector3 oldPosition = player.transform.position;
        for(int i=0; i < 10; i++)
        {
            yield return new WaitForSeconds(2/10);
            Vector3 newPosition = new Vector3(transform.position.x + Random.Range(-0.05f,0.05f), transform.position.y + Random.Range(-0.05f, 0.05f),transform.position.z + Random.Range(-0.05f, 0.05f));
            transform.position = newPosition;
        }  
        player.transform.position = oldPosition;
        monstruo.SetActive(false);
        asustando = false;
        yield return new WaitForSeconds(Random.Range(5, 20));  
      
        audioSource.volume =1;
     

    }
    public void setAsustando(bool value)
    {
        asustando = value;
    }


    public bool getAsustando()
    {
        return asustando;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, player.transform.position - transform.position);
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
    public void Actuar(AudioSource audio, Animator controller)
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
