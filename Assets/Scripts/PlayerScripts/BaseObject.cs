/* 1. �������� ��� ������ ��� ������
 * 2. ������������ � ��� �����-�� ������� ������
 *  ��������: ������������� �������(�����) - ����.�������, ������� ���� ������ � ����������
 *  -> ������������� - ������� ��������
 *  
 *  � ������ �������� instance: ���������� ��������, �������� ����� get
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������� ����� ��� ����������� ������, ������� ����� ��� ���� ��������.
/// </summary>
public abstract class BaseObject : MonoBehaviour
{
    private Transform gOTransform;
    private GameObject gOInstance;
    private string _name;
    private bool isVisible;
    //private bool isWeapon1;
    //private bool isWeapon2;
    //private bool isWeapon3;
    //private bool isWeapon4;
    //private string _Flashlight, _Knife, _Glock, _SCAR;


    private Vector3 position;
    private Vector3 scale;
    private Quaternion rotation;

    private Material material;
    protected Color _color;

    private Rigidbody _rigidbody;

    private Camera mainCamera;
    private Animator anim; //������ ���������, ������� ����� ����� ��������������.


    protected Transform GOTransform { get => gOTransform; } //���� �� ������� ������� �������� 
    //������������� ����������, �� ���� �� ��� ����� ������ �� ����� ������������, ����� ����� ������������������ �����.
    protected GameObject GOInstance { get => gOInstance; }
    protected Camera MainCamera { get => mainCamera; }
    protected Material Material { get => material; }
    /// <summary>
    /// ��������
    /// </summary>
    protected Animator Anim { get => anim; }
    /// <summary>
    /// ���������� �������� ��������
    /// </summary>
    protected int ChCount { get => GOTransform.childCount; } //�������� ��� ����������� ���-�� �������� ���������
    protected Rigidbody RB { get => _rigidbody; }
    protected string Name { get => _name; set => _name = value; }
    protected Vector3 Position { get => position; set => position = value; }
    protected Vector3 Scale { get => scale; set => scale = value; }
    protected Quaternion Rotation { get => rotation; set => rotation = value; }
    //���������, ���� �� � ��� Renderer, ���� ����, �� �������� ���� enable - ������� ��� �������� ���������
    //�������� ���������� set
    protected bool IsVisible
    {
        get => isVisible;
        set
        {
            isVisible = value;
            if (gOInstance.GetComponent<Renderer>())
            {
                gOInstance.GetComponent<Renderer>().enabled = isVisible;
            }

        }
    }

    //public PhotonView Photon { get => _photon; }

    //public bool IsWeapon1 { get => isWeapon1; set => isWeapon1 = value; }
    //public bool IsWeapon2 { get => isWeapon2; set => isWeapon2 = value; }
    //public bool IsWeapon3 { get => isWeapon3; set => isWeapon3 = value; }
    //public bool IsWeapon4 { get => isWeapon4; set => isWeapon4 = value; }
    //public Transform GOTransformWeapon { get => goTransformWeapon; set => goTransformWeapon = value; }



    //�������������: ������� � ���� ����������� ������:
    #region 
    protected virtual void Awake() //�����, ������� ��� ������ �� ����������.
    {
        gOInstance = gameObject;
        gOTransform = gameObject.transform; // = GetComponent
        Name = gameObject.name;
        mainCamera = Camera.main;
        //_Flashlight = "Flashlight"; _Knife = "Knife"; _Glock = "Glock"; _SCAR = "SCAR";
        //����� ����������, ������� ����� ����, � ������� ����� � �� ����.
        //if (GetComponent<PhotonView>())
        //{
        //    _photon = GetComponent<PhotonView>();
        //    //_photon = PhotonView.Get(this);
        //    if (_photon.IsMine)
        //    {
        //        mainCamera = Camera.main;
        //    }
        //    else
        //    {
        //        mainCamera = transform.GetComponentInChildren<Camera>();
        //    }
        //}
        //else
        //{
        //    if (gameObject.tag == "Weapons")
        //    {
        //        //GameObject GO = transform.GetComponentInParent<SinglePlayer>().gameObject;
        //        mainCamera = GetComponentInParent<SinglePlayer>().mainCamera;
        //        //if (gameObject.name == "Flashlight") { isWeapon1 = gameObject.activeInHierarchy; }
        //        //if (gameObject.name == "Knife") { isWeapon2 = gameObject.activeInHierarchy; }
        //        //if (gameObject.name == "Glock") { isWeapon3 = gameObject.activeInHierarchy; }
        //        //if (gameObject.name == "SCAR") { isWeapon4 = gameObject.activeInHierarchy; }
        //    }
        //}


        if (GetComponent<Rigidbody>())  //�������� ������ �� �������, ���� �� � ������� Rigidbody.
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        if (GetComponent<Animator>())  // - //- 
        {
            anim = GetComponent<Animator>();
        }
        if (GetComponent<Renderer>())  // - //- 
        {
            material = GetComponent<Renderer>().material;
        }
    }
    #endregion
}