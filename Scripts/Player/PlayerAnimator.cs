using Godot;
using System;
using System.Runtime.InteropServices;

public partial class PlayerAnimator : AnimatedSprite2D {

	private const string ANIMATION_IDLE = "idle";
	private const string ANIMATION_WALK = "walk";
	private const string ANIMATION_JUMP = "jump";

	private enum FacingDirection { LEFT, RIGHT, MAINTAIN }

	public class AnimationEvent {
		public float animationTime;
		public string animationName;

		public AnimationEvent(float animationTime, string animationName) {
			this.animationTime = animationTime;
			this.animationName = animationName;
		}
	}

	[Export] private PlayerMovement player;

	private float overrideTime = -1;
	private string overrideState = "";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

		UpdateFacingDirection();


		UpdateAnimation((float) delta);
	}

	private void UpdateFacingDirection() {
		FacingDirection flip = player.Direction.X < 0 ? FacingDirection.LEFT : player.Direction.X > 0 ? FacingDirection.RIGHT : FacingDirection.MAINTAIN;
		switch (flip) {
			case FacingDirection.LEFT:
				if (!this.FlipH) this.FlipH = true;
				break;
			case FacingDirection.RIGHT:
				if (this.FlipH) this.FlipH = false;
				break;
		}
	}

	private void UpdateAnimation(float delta) {
		string currentAnimation = ANIMATION_IDLE;

        if (overrideTime >= 0) {
			currentAnimation = overrideState;

			overrideTime -= delta;
			if (overrideTime < 0) {
				ForceOverride(null);
			}
		}

        if (!player.IsGrounded) currentAnimation = ANIMATION_JUMP;
		else if (player.Velocity.X != 0) currentAnimation = ANIMATION_WALK;

		if(this.Animation != currentAnimation) {
			this.Play(currentAnimation);
		}
	}

	public void ForceOverride(string name, float duration = 1) {
		if (string.IsNullOrEmpty(name)) {
			overrideState = "";
			overrideTime = -1;
			player.Enabled = true;
		} else {
			overrideState = name;
			overrideTime = duration;
			player.Enabled = false;
		}
	}

	public void ForceOverride(AnimationEvent @event) {
		if (@event != null) ForceOverride(@event.animationName, @event.animationTime);
		else ForceOverride("", -1);
	}
}
