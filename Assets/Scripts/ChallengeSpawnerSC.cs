using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeSpawnerSC : MonoBehaviour
{
    [HideInInspector] ChallengeSC challengeCtr;
    [SerializeField] List<GameObject> obstacles = new List<GameObject>();
    bool isPauseGame;
    sbyte timeToCount, timeToWaitForStart;
    private float spawnWaitTime; //Depen on level
    private float spawnX, spawnY;
    Vector3 curPlayerPos;
    [SerializeField] DinoSC dino;
    void Start()
    {
        Invoke(nameof(CaculatingTimeSpawn), 2f);
    }

    // Update is called once per frame
    void Update()
    {    }
    public void CaculatingTimeSpawn()
    {
        float tempCal;
        float baseWaiTime = 3f;
        float minWaitTime = 0.2f;
        float decrement = 0.1f;
        tempCal = Random.Range(2, 5);
        spawnWaitTime = tempCal;
        dino = GameObject.Find("OBJ_Dino_Step(Clone)").GetComponent<DinoSC>();
    }
    private void SpawnObjects()
    {
        isPauseGame = !challengeCtr.isEnablePlay;
        print("2.0 - challengeCtr.isEnablePlay = " + challengeCtr.isEnablePlay);
        print("2.1 - isPauseGame = "+ isPauseGame);
        if (isPauseGame == false)
        {
            curPlayerPos = dino.gameObject.transform.position;
            spawnX = curPlayerPos.x + 2.5f;
            spawnY = curPlayerPos.y + 2.5f;
            int randObj = Random.RandomRange(0, obstacles.Count);
            print("2.2 start spawn dino");
            Instantiate(obstacles[randObj], new Vector2(spawnX, spawnY), Quaternion.identity);
            print("2.3. spawn Object complete");
        }
    }
    public void StartGameOnInit() => InvokeRepeating(nameof(SpawnObjects), 1f, spawnWaitTime);
    public void StopGameplay() => CancelInvoke(nameof(SpawnObjects));
    public void ResumeGame(bool play) => isPauseGame = !play;
    public void PauseGame(bool play) => isPauseGame = !play;
    public void SetGamemode()
    {
        challengeCtr = GameObject.Find("OBJ_CHallengeMN").GetComponent<ChallengeSC>();
    }
}
