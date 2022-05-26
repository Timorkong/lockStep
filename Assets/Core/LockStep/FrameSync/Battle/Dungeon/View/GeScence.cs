using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeScence
{
    public BeScence beScence = null;

    public GeDungeon geDungeon = null;

    public List<GeEntity> geEntities = new List<GeEntity>();

    private bool mCanUpdate = false;

    public GeScence(BeScence beScence, GeDungeon geDungeon)
    {
        this.beScence = beScence;

        this.geDungeon = geDungeon;

        geDungeon.geScence = this;

        mCanUpdate = false;

        Init();
    }

    private void Init()
    {
        SceneManager.Instance.onLoadScence += OnSceneLoad;

        SceneManager.Instance.LoadScence(BattleMain.data.level_name);
    }

    public void Update()
    {
        if (mCanUpdate == false) return;

        foreach (var entity in geEntities)
        {
            entity.Update();
        }
    }

    void OnSceneLoad(Scene scene, LoadSceneMode move)
    {
        mCanUpdate = true;

        command_req.CMD_START_GAME_REQ();
    }
}
