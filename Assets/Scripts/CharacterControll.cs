using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public Camera camera;
    
    private CharacterController _caracterController;

    private readonly float _translateSpeed = 3f;
    private readonly float _rotateSpeed = 80;
    private readonly float _gravity = 8;

    private Vector3 _moveVector = Vector3.zero;
    private Vector3 _rotateVector = Vector3.zero;

    Animator _anim;

    private bool _forwardMove = true;
    private bool _backMove = true;

    void Start()
    {
        _caracterController = GetComponent<CharacterController>();
        _caracterController.enabled = true;

        _anim = GetComponent<Animator>();

        RaycastHit CamRayCast;
        
        Debug.DrawRay(transform.position, camera.transform.position, Color.red);

        if (Physics.Raycast(transform.position, camera.transform.position, out CamRayCast, 1))
        {
            Debug.Log("Cam touchée");
        }
    }

    void Update()
    {

        if (_caracterController.isGrounded)
        {

            _moveVector = new Vector3( 0, 0, Input.GetAxis("Vertical")  );
           _rotateVector = transform.eulerAngles;
            
            _moveVector = transform.TransformDirection(_moveVector);
            _moveVector *= _translateSpeed * Time.deltaTime;


            if(_moveVector != Vector3.zero) {
                _anim.SetBool("Walk", true);
            } else {
                _anim.SetBool("Walk", false);
            }

            if (Input.GetButtonDown("Jump"))
            {
                _anim.SetTrigger("Jump");
                //_moveVector.y += 50 * Time.deltaTime;
            }


            _rotateVector.y += Input.GetAxis("Horizontal") * _rotateSpeed * Time.deltaTime;
            
            transform.eulerAngles = _rotateVector;
        }

        _moveVector.y -= _gravity * Time.deltaTime;
        
        _caracterController.Move( _moveVector );
    }
}




