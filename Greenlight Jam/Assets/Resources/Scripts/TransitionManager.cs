using System.Security.Cryptography;
using System;
using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager {

    public string sceneName;
    public float waitTime;
    public Animator anim;

    IEnumerator ChangeScene(){
        anim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }
}