using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Init()
    {
        if(base.Init() == false)
        {
            return false;
        }

        SceneType = Define.Scene.Game;

        // 씬 시작할 때 생성할 게임 오브젝트

        Debug.Log("GameScene::Init()");
        return true;
    }
}