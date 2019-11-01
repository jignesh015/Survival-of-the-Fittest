using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorContoller : MonoBehaviour
{
    public Animator bearAnimator;
    public Animator lionAnimator;
    public Animator polarbearAnimator;
    public Animator currentAnimator;
    public GameObject bear;
    public GameObject lion;
    public GameObject polarbear;
    public enum Animals
    {
        bear,lion,polarbear
    }
    Animals currentAnimals;

    private void Start()
    {
        SetCurrentAnimal(0);
    }

    public void SetCurrentAnimal(int animalDecider)
    {
        switch(animalDecider)
        {
            case 0:
                currentAnimals = Animals.bear;
                currentAnimator = bearAnimator;
               
                break;
            case 1:
                currentAnimals = Animals.lion;
                currentAnimator = lionAnimator;
                
                break;
            case 2:
                currentAnimals = Animals.polarbear;
                currentAnimator = polarbearAnimator;
                
                break;
        }
    }

    public void PlayAnimalAnim(string triggerName)
    {
        currentAnimator.SetTrigger(triggerName);
    }
}
