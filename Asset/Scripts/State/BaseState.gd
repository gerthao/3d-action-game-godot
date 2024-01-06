class_name BaseState extends Node

var state_machine: StateMachine
var character: Player
var animation_player: AnimationPlayer

@export var animation_name: String = ''


func enter() -> void:
	if (animation_name == ''):
		return
		
	print('entering state: ', name)
	animation_player.play(animation_name)


func exit() -> void:
	print('exiting state: ', name)
	
	
func state_update(delta: float) -> void:
	pass
	
	
func show_info() -> void:
	print(name, '/', character, '/', state_machine)
