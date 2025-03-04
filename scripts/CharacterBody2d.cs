using Godot;
using System;

public partial class CharacterBody2d : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public bool wasOnGround = false, justJumped = false;
	public Timer koyoteTimer;
	public Timer jumpedTimer;
	public AnimatedSprite2D animatedSprite;
	
	public void _on_jumped_timer_timeout()
	{
		justJumped = false;
		GD.Print("jump end");
	}
	public void _on_koyote_timer_timeout()
	{
		wasOnGround = false;
	}
	public override void _Ready()
	{
		koyoteTimer = GetNode<Timer>("KoyoteTimer");
		jumpedTimer = GetNode<Timer>("JumpedTimer");
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;


		if (!wasOnGround && IsOnFloor())
		{
			wasOnGround = true;
		}

		// Add the gravity.
		if (!IsOnFloor())
		{
			animatedSprite.Play("jump");
			if (koyoteTimer.TimeLeft == 0 && wasOnGround && !justJumped)
			{
				koyoteTimer.Start();
			}
			velocity += GetGravity()/3 * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionPressed("ui_accept") && wasOnGround)
		{
			GD.Print("Jumped");
			jumpedTimer.Start();
			wasOnGround = false;
			justJumped = true;
			velocity.Y = JumpVelocity/3;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			if (direction.X > 0)
			{
				
				animatedSprite.FlipH = false;
			}
			else if (direction.X < 0)
			{
				animatedSprite.FlipH = true;
			}
			velocity.X = Mathf.MoveToward(Velocity.X, direction.X*Speed/3, Speed);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if ( IsOnFloor() )
		{
			if (direction.X != 0)
			{
				animatedSprite.Play("run");
			}
			else
			{
				animatedSprite.Play("idle");
			}
		}


		Velocity = velocity;
		MoveAndSlide();
	}
}
