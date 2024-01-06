class_name Slide
extends BaseState

var speed: float = 650
var duration: float = 0.3
var remaining_duration: float
var direction: Vector3

func state_update(delta: float):
	remaining_duration -= delta
	direction = character.visual.transform.basis.z
	
	if (remaining_duration > 0):
		character.velocity.x = direction.x * speed * delta
		character.velocity.z = direction.z * speed * delta
	else:
		character.velocity.x = 0
		character.velocity.z = 0
		state_machine.switch_state('Idle')

func enter():
	super.enter()
	character.velocity.x = 0
	character.velocity.z = 0
	remaining_duration = duration
