using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerControllers : CreatureController
{
    protected Coroutine _coSkill;
    protected override void Init()
    {
        base.Init();
    }
    protected override void UpdateAnimation()
    {
        if (_animator == null || _sprite == null)
            return;

        if (State == CreatureState.Idle)
        {
            switch (Dir)
            {
                case MoveDir.Up:
                    _animator.Play("idle_up");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    _animator.Play("idle_down");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    _animator.Play("idle_side");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Right:
                    _animator.Play("idle_side");
                    _sprite.flipX = true;
                    break;
            }
        }
        else if (State == CreatureState.Moving)
        {
            switch (Dir)
            {
                case MoveDir.Up:
                    _animator.Play("walk_up");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    _animator.Play("walk_down");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    _animator.Play("walk_side");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Right:
                    _animator.Play("walk_side");
                    _sprite.flipX = true;
                    break;
            }
        }
        else if (State == CreatureState.Skill)
        {
            switch (Dir)
            {
                case MoveDir.Up:
                    _animator.Play("weapon_sword_up");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    _animator.Play("weapon_sword_down");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    _animator.Play("weapon_sword_side");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Right:
                    _animator.Play("weapon_sword_side");
                    _sprite.flipX = true;
                    break;
            }
        }
        else if (State == CreatureState.Dead)
        {

        }
    }
    protected override void UpdateController()
    {
        base.UpdateController();
    }

    public void UseSkill(int skillId)
    {
        if (skillId == 1)
        {
            StartCoroutine("CoStartSword");
        }
        else if (skillId == 2)
        { 
        }
    }
    protected virtual void CheckUpdatedFlag()
    {
    }
    IEnumerator CoStartSword()
    {
        //이부분은 서버로 이전
        //GameObject go = Managers.Object.Find(GetFrontCellPos(1));
        //if (go != null)
        //{
        //    CreatureController cc = go.GetComponent<CreatureController>();
        //    if (cc != null)
        //        cc.OnDamaged();
        //}
        State = CreatureState.Skill;
        yield return new WaitForSeconds(0.5f);
        State = CreatureState.Idle;
        _coSkill = null;
        CheckUpdatedFlag();
    }
}
