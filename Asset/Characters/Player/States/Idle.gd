class_name Idle
extends BaseState


func state_update(delta: float):
	if character.can_attack:
		state_machine.switch_state('Attack')
	if character.can_slide:
		state_machine.switch_state('Slide')
	if character.direction:
		state_machine.switch_state('Run')
