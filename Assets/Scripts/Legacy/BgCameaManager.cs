using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BgCameaManager : MonoBehaviour
{
    public Camera bgCamera;

    public MeshRenderer[] gameBGMeshRenderers;
    private Material[] gameBgMaterials;

    private RenderTexture bgRenderTexture;

    private void Start()
    {
        SetupCam();

        if (Application.isPlaying)
            Init();
    }

    private void SetupCam()
    {
        if (gameBGMeshRenderers == null || gameBGMeshRenderers.Length == 0)
            return;

        for (int i = 0; i < gameBGMeshRenderers.Length; i++)
        {
            gameBGMeshRenderers[i].sortingOrder = -30;
        }
    }

    private void Init()
    {
        bgRenderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        bgCamera.targetTexture = bgRenderTexture;

        gameBgMaterials = new Material[gameBGMeshRenderers.Length];

        for (int i = 0; i < gameBGMeshRenderers.Length; i ++)
        {
            gameBGMeshRenderers[i].sortingOrder = -30;
            gameBgMaterials[i] = gameBGMeshRenderers[i].material;
            gameBgMaterials[i].SetTexture("_MainTex", bgRenderTexture);
        }
    }
}
