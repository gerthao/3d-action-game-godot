class_name Run
extends BaseState


@export var footstep_vfx: GPUParticles3D


func state_update(delta: float) -> void:
	if character.direction:
		character.velocity.x = character.direction.x * character.SPEED
		character.velocity.z = character.direction.z * character.SPEED
	else:
		character.velocity.x = move_toward(character.velocity.x, 0, character.SPEED)
		character.velocity.z = move_toward(character.velocity.z, 0, character.SPEED)
		
	if character.velocity.length() > 0.2:
		var look_dir = Vector2(character.velocity.z, character.velocity.x)
		character.visual.rotation.y = lerp_angle(character.visual.rotation.y, look_dir.angle(), character.ANGULAR_SPEED * delta)

	if character.can_slide:
		state_machine.switch_state('Slide')
	if character.direction == Vector3.ZERO:
		state_machine.switch_state('Idle')

func enter() -> void:
	super.enter()
	footstep_vfx.emitting = true
	
	
func exit() -> void:
	super.exit()
	footstep_vfx.emitting = false
	
