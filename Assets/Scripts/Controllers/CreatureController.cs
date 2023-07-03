using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CreatureController : MonoBehaviour
{
    public int Id { get; set; }

    protected bool _updated = false;
    StatInfo _stat = new StatInfo();

    public StatInfo Stat
    {
        get { return _stat; }
        set 
        {
            if (_stat.Equals(value))
                return;

            _stat.Hp  = value.Hp; 
            _stat.MaxHp  = value.MaxHp; 
            _stat.Speed  = value.Speed; 
        }
    }

    public float Speed
    {
        get { return _stat.Speed; }
        set { _stat.Speed = value; }
    }

    PositionInfo _positionInfo = new PositionInfo();
    public PositionInfo PosInfo
    {
        get { return _positionInfo; }
        set
        {
            if (_positionInfo.Equals(value))
                return;

            CellPos = new Vector3Int(value.PosX, value.PosY, 0);
            State = value.State;
            Dir = value.MoveDir;
        }
    }
    public void SyncPos()
    {
        Vector3 destPos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
        transform.position = destPos;
    }

    /// <summary>
    /// 현재위치
    /// </summary>
    public Vector3Int CellPos 
    {
        get
        {
            return new Vector3Int(PosInfo.PosX, PosInfo.PosY, 0);
        }
        set
        {
            if (PosInfo.PosX == value.x && PosInfo.PosY == value.y)
                return;

            PosInfo.PosX = value.x;
            PosInfo.PosY = value.y;
            _updated = true;
        }
    }
    protected Animator _animator;
    protected SpriteRenderer _sprite;

    public CreatureState State
    {
        get { return PosInfo.State; }
        set
        {
            if (PosInfo.State == value)
                return;

            PosInfo.State = value;
            UpdateAnimation();
            _updated = true;
        }
    }

    public MoveDir Dir
    {
        get { return PosInfo.MoveDir; }
        set
        {
            if (PosInfo.MoveDir == value)
                return;

            PosInfo.MoveDir = value;

            UpdateAnimation();
            _updated = true;
        }
    }
    /// <summary>
    /// 내가 바라보고있는 셀의 위치
    /// </summary>
    /// <returns></returns>
    public Vector3Int GetFrontCellPos(int range)
    {
        Vector3Int cellPos = CellPos;

        switch (Dir)
        {
            case MoveDir.Up:
                cellPos += Vector3Int.up * range;
                break;
            case MoveDir.Down:
                cellPos += Vector3Int.down * range;
                break;
            case MoveDir.Left:
                cellPos += Vector3Int.left * range;
                break;
            case MoveDir.Right:
                cellPos += Vector3Int.right * range;
                break;
        }
        return cellPos;
    }
    protected virtual void UpdateAnimation()
    {
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
        else if (State == CreatureState.Dead)
        {
           
        }
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        UpdateController();
    }

    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        Vector3 pos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
        transform.position = pos;

        //초기값 부여
        State= CreatureState.Idle;
        UpdateAnimation();
    }

    protected virtual void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Idle:
                UpdateIdle();
                break;
            case CreatureState.Moving:
                UpdateMoving();
                break;
            case CreatureState.Skill:
                break;
            case CreatureState.Dead:
                break;
        }
    }

    // 이동 가능한 상태일 때, 실제 좌표를 이동한다
    protected virtual void UpdateIdle()
    {
      
    }
    // 스르륵 이동하는 것을 처리
    protected void UpdateMoving()
    {
        Vector3 destPos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
        Vector3 moveDir = destPos - transform.position;

        // 도착 여부 체크
        float dist = moveDir.magnitude;
        if (dist < Stat.Speed * Time.deltaTime)
        {
            transform.position = destPos;
            MoveToNextPos(); 
        }
        else
        {
            transform.position += moveDir.normalized * Stat.Speed * Time.deltaTime;
            State = CreatureState.Moving;
        }
    }
    protected virtual void MoveToNextPos()
    {
       
    }

    protected void UpdateSkill() { }
    protected void UpdateDead() { }

    public virtual void OnDamaged()
    {
        State = CreatureState.Dead;
        _animator.Play("vanish");
        Managers.Object.Remove(Id);
        Managers.Resource.Destroy(gameObject, 0.5f);
    }
}