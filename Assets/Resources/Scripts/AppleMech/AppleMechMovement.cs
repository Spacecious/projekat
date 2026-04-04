    using UnityEngine;
    using UnityEngine.InputSystem;

    public class AppleMechMovement : MonoBehaviour
    {
        
        [SerializeField] float moveSpeed;
        private Vector2 minBounds, maxBounds;
        [SerializeField] float paddingLeft = 0.5f, paddingRight = 0.5f;

        void Start()
        {
            InitBounds();
        }

        

        void Update()
        {
            Move();
        }

        void InitBounds()
        {
            Camera cam = Camera.main;
            minBounds = cam.ViewportToWorldPoint(new Vector2(0, 0));
            maxBounds = cam.ViewportToWorldPoint(new Vector2(1, 1));
        }

        private void Move()
        {
            float moveX = 0;
            if (Keyboard.current.aKey.isPressed)
            {
                moveX = -1;
            }
            else if(Keyboard.current.dKey.isPressed)
            {
                moveX = 1;
            }

            float deltaX = moveX * moveSpeed * Time.deltaTime;
            float newX = transform.position.x + deltaX;

            newX = Mathf.Clamp(newX, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
