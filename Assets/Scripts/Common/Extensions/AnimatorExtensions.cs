using UnityEngine;

static class AnimatorExtensions
{
    public static void SafePlay(this Animator animator, string name, int layer, float normalizedTime = 0f)
    {
        if (!animator.IsPlaying(name, layer))
        {
            animator.Play(name, layer, normalizedTime);
        }
    }


    public static bool IsPlaying(this Animator animator, string stateName, int layer = 0)
    {
        var info = animator.GetCurrentAnimatorStateInfo(layer);
        return info.IsName(stateName) && info.normalizedTime < 1f;
    }
}