using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSC : MonoBehaviour
{
    [HideInInspector] GameplaySC gameCtr;
    [SerializeField] List<GameObject> obstacles = new List<GameObject>();
    bool isPauseGame;
    sbyte timeToCount, timeToWaitForStart;
    private float spawnWaitTime; //Depen on level
    private float spawnX, spawnY;
    Vector3 curPlayerPos;
    [SerializeField] DinoSC dino;
    void Start()
    {
    }
    public void CaculatingTimeSpawn()
    {
        float tempCal;
        float baseWaiTime = 3f;
        float minWaitTime = 0.2f;
        float decrement = 0.1f;
        tempCal = baseWaiTime - (gameCtr.curLvl - 1) * decrement;
        spawnWaitTime = 3;
    } 
    private void SpawnObjects()
    {
        isPauseGame = !gameCtr.isEnablePlay;
        if (isPauseGame == false)
        {
            dino = gameCtr.curPlayer;
            curPlayerPos = dino.gameObject.transform.position;
            spawnX = curPlayerPos.x + 20;
            int randObj = Random.Range(0, obstacles.Count);
            Instantiate(obstacles[randObj], new Vector2(spawnX, 5), Quaternion.identity);
        }
    }
    public void StopGameplay() => CancelInvoke(nameof(SpawnObjects));
    public void ResumeGame(bool play) => isPauseGame = !play;
    public void PauseGame(bool play) => isPauseGame = !play;
    public void OnMoveOnScreen(float pPosX)
    {
        float midScreenPosX = pPosX;
        float distnace = transform.position.x - midScreenPosX;
        if(distnace <= 3)
        {
            gameObject.transform.position = new Vector3(transform.position.x +1, transform.position.y, transform.position.z);
        }
    }
    public void OnAssistSpawnerElements()
    {
        gameCtr = GameObject.Find("OBJ_ArcadeMN").GetComponent<GameplaySC>();
        CaculatingTimeSpawn();
        isPauseGame = !gameCtr.isEnablePlay;
        InvokeRepeating(nameof(SpawnObjects), 1f, spawnWaitTime);
    }
}
