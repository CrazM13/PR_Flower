using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D {
	[Export] private float Speed = 300.0f;
	[Export] private float JumpVelocity = -400.0f;
	[Export] private float gravityScale = 1;

	private Vector2I lookDirection = Vector2I.Right;
	public Vector2I Direction { get => lookDirection; set => lookDirection = value; }
	public bool IsGrounded { get => IsOnFloor(); }

	public bool Enabled { get; set; } = true;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta) {
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor()) velocity.Y += gravity * (float) delta * gravityScale;

		// Handle Jump.
		if (Enabled && Input.IsActionJustPressed("movement_jump") && IsOnFloor()) velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = Enabled ? Input.GetAxis("movement_left", "movement_right") : 0;
		float lookDirection = Enabled ? Input.GetAxis("look_up", "look_down") : 0;
		if (direction != 0) {
			velocity.X = direction * Speed;

			this.lookDirection.X = direction > 0 ? 1 : -1;
		} else {
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (lookDirection != 0) {
			this.lookDirection.Y = lookDirection > 0 ? 1 : -1;
		} else {
			this.lookDirection.Y = 0;
		}

		Velocity = velocity;
		MoveAndSlide();
	}



}
