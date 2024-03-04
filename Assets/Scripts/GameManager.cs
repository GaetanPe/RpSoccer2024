using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] public float Timer = 60f;
    [SerializeField] private bool GameEnd= false;
    [SerializeField] public string Winner;
    [SerializeField] public string Loser;
    [SerializeField] AudioManager audioManager;
    [SerializeField] public List<GameObject> ListGameObject;
    [SerializeField] private List<int> NbrGoalsPlayers = new List<int>();
    [SerializeField] PauseMenu pauseMenu;
    public static GameManager getInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance;
    }

    private void Awake()
    {
        audioManager.playNextSongs();

    }
    private void Start()
    {
        audioManager.PlaySFX("whistleStart");

    }
    public int getNbrGoalPlayer(int nbr)
    {
        return (NbrGoalsPlayers[nbr]);
    }

    public void setNbrGoalPlayer(int nbr)
    {
        NbrGoalsPlayers[nbr]++;
    }

    public void StartNewRound()
    {
        audioManager.PlaySFX("whistleStart");
        Timer = 60f;
        ListGameObject[2].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ListGameObject[2].gameObject.GetComponent<Rigidbody>().isKinematic = false;
        ListGameObject[0].transform.position = ListGameObject[3].transform.position;
        ListGameObject[0].transform.rotation = Quaternion.Euler(0, 90, 0);
        ListGameObject[1].transform.position = ListGameObject[4].transform.position;
        ListGameObject[1].transform.rotation = Quaternion.Euler(0, -90, 0);
        ListGameObject[2].transform.position = ListGameObject[5].transform.position;
    }

    public IEnumerator EndNewRound()
    {
        float timer = 0;
        while (timer < 2f) 
        {
            timer += Time.deltaTime;
            yield return null;
        }

        ListGameObject[2].gameObject.GetComponent<Rigidbody>().isKinematic = true;
        StartNewRound();
    }

    private void Update()
    {
        if (NbrGoalsPlayers[0] == 5)
        {
            GameEnd = true;
            Winner = "Player One Win";
            Loser = "Player Two Win";
        }
        else if (NbrGoalsPlayers[1] == 5)
        {
            GameEnd = true;
            Winner = "Player Two Win";
            Loser = "Player One Win";
        }
        if (GameEnd == true)
        {
            SceneManager.LoadScene("EndPanel");
        }
        if (Input.GetKey(KeyCode.P))
        {
            Time.timeScale = 0;
            pauseMenu.gameObject.SetActive(true);
        }
    }
}
