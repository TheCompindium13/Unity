
# Guide for both TopDown Shooter and JumpyRatKing Tag!!

A brief description of what this project does and who it's for


# JumpyRatKing FAQ

How to move:

    * Player 1
        * A to move left
        * D to move right 
        * W to jump
    * Player 2
        * Left to move left 
        * Right to move right 
        * Up to jump

# TopDownShooter FAQ

How to move:

    * Player 1
        * A to move left
        * D to move right
        * S to move down 
        * W to move up
        * Click to shoot



## JumpyRatKing Documentation
GameManager:

This script is used to manage game states and transitions between scenes. It is a singleton to be sure there’s only one GameManager instance in the whole the game.

    _instance (GameManager): This is a private static field that holds the singleton instance of the GameManager.

    OnGoToNextScene (UnityEvent): This is a public UnityEvent that can be used to trigger actions when the game goes to the next scene. The [HideInInspector] attribute hides this field in the Unity inspector.

    Instance (GameManager): This is a public static property that provides access to the singleton instance of the GameManager.

    Awake(): This method is called when the script instance is being loaded. It’s used to ensure that there is only one instance of GameManager (singleton pattern). If an instance already exists, it destroys the new one. If not, it assigns the new one to _instance and makes sure it persists across scenes with DontDestroyOnLoad(gameObject).

    GoToNextScene(): This public method is used to load the next scene in the build index. It uses SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1) to load the next scene.

HudBehaviour:

This script is used to manage the Heads-Up Display (HUD) in a game. It keeps track of the game time and displays game over messages.

    _Time (TMP_Text): This is a private field that holds the TextMeshPro Text object used to display the remaining time on the HUD.

    _gameOver (TMP_Text): This is a private field that holds the TextMeshPro Text object used to display a game over message.

    _PlayerTagBehaviour (PlayerTagBehaviour): This is a private field that holds a reference to a script that handles player tagging behavior.

    _remainingTime (float): This is a private field that keeps track of the remaining time in the game.

    _maxTime (float): This is a private field that represents the maximum time or start time for the game.

    _currentUser (GameObject): This is a private field that represents the current user or player.

    RemainingTime() (float): This public method returns the remaining time in the game.

    Start(): This method is called when the script instance is being loaded. It’s currently empty.

    Update(): This method is called once per frame. It updates the _remainingTime and _Time.text based on whether the player is tagged or not, and whether the remaining time is greater than 0. If the remaining time reaches 0, it updates the _gameOver.text to show that the current user has won.

MultipleTargetsCamera:

This script is used to manage a camera that follows multiple targets. It adjusts the camera’s position and zoom level based on the targets positions.

    targets (List<Transform>): This is a private field that holds a list of the targets that the camera should follow.

    offset (Vector3): This is a private field that holds the offset of the camera from the center point of the targets.

    smoothTime (float): This is a private field that determines the smoothness of the camera movement.

    minZoom and maxZoom (float): These are private fields that determine the minimum and maximum zoom levels of the camera.

    zoomLimiter (float): This is a private field that limits the zoom level based on the greatest distance between the targets.

    velocity (Vector3): This is a private field used by the Vector3.SmoothDamp function to smooth the camera movement.

    cam (Camera): This is a private field that holds a reference to the Camera component attached to the same GameObject as this script.

    Start(): This method is called when the script instance is being loaded. It gets and stores the Camera component in the cam field.

    LateUpdate(): This method is called once per frame, after all Update methods have been called. It calls the Move and Zoom methods if there are any targets.

    Zoom(): This method adjusts the camera’s field of view based on the greatest distance between the targets.

    GetGreatestDistance(): This method calculates and returns the greatest distance between the targets.

    Move(): This method calculates the center point of the targets, adds the offset, and smoothly moves the camera to the new position.

    GetCenterPoint(): This method calculates and returns the center point of the targets.
PlayerMovement:

This script is used to manage the movement and jumping behavior of a player. It uses the Rigidbody component for physics based movement.

    _isPlayer1 (bool): This private field determines whether the player is Player 1. If true, the script uses Player 1’s input axes; otherwise, it uses Player 2’s.

    _maxSpeed, _acceleration, _jumpHeight (float): These private fields determine the player’s maximum speed, acceleration, and jump height, respectively. They are serialized so they can be set in the Unity inspector.

    _rigidbody (Rigidbody): This private field holds a reference to the Rigidbody component attached to the player.

    _groundCheck (Vector3), _groundCheckRadius (float): These private fields are used to check if the player is grounded by creating a sphere at the player’s feet and checking if it overlaps with any other colliders.

    _jumpInput (bool), _isGrounded (bool): These private fields keep track of whether the jump button is pressed and whether the player is grounded, respectively.

    _moveDirection (Vector3): This private field stores the direction in which the player should move.

    Awake(): This method is called when the script instance is being loaded. It gets and stores the Rigidbody component in the _rigidbody field.

    Update(): This method is called once per frame. It updates _moveDirection and _jumpInput based on the player’s input.

    FixedUpdate(): This method is called every fixed framerate frame. It updates _isGrounded, applies force to the Rigidbody based on _moveDirection and _acceleration, clamps the Rigidbody’s velocity to _maxSpeed, and applies an upward force if the jump button is pressed and the player is grounded.

    OnDrawGizmosSelected(): This method is called in the Unity editor when the GameObject is selected. It draws a green wire sphere at the ground check position.

PlayerTagBehaviour:

This script is used to manage the tagging behavior of a player in a game. It uses the UnityEvent and ParticleSystem components for event-driven programming and visual effects.

    _isTagged (bool): This private field determines whether the player is tagged. If true, the player is currently “it” in the game.

    _taggedParticles (ParticleSystem): This private field holds a reference to the ParticleSystem component attached to the player. This is used to create a visual effect when the player is tagged.

    OnTagged (UnityEvent): This public event is invoked when the player is tagged.

    _canBeTagged (bool): This private field keeps track of whether the player can be tagged.

    isTagged (bool): This public property gets the value of _isTagged.

    Tag() (bool): This public method is used to tag the player. It returns false if the player cannot be tagged, and true otherwise.

    SetCanBeTagged(): This private method sets _canBeTagged to true.

    Start(): This method is called before the first frame update. It gets the TrailRenderer component and enables or disables it based on whether the player is tagged.

    OnCollisionEnter(Collision collision): This method is called when this collider/rigidbody has begun touching another rigidbody/collider. It handles the logic for tagging other players.

    OnCollisionExit(Collision collision): This method is called when this collider/rigidbody has stopped touching another rigidbody/collider. It invokes the SetCanBeTagged method after a delay of 0.5 seconds.


