class_name StateMachine
extends Node

@export var initial_state  = NodePath()
@onready var current_state: BaseState = get_node(initial_state) as BaseState


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	initialize_states()
	current_state.enter()


func _physics_process(delta: float) -> void:
	current_state.state_update(delta)


func initialize_states() -> void:
	for child in get_children():
		var state = child as BaseState
		state.state_machine = self
		state.character = get_parent()
		state.animation_player = get_parent().get_node('Visual/AnimationPlayer')
		state.show_info()
		
		
func switch_state(target: String) -> void:
	if !has_node(target):
		print('Could not find the targeted state.')
		return
	
	current_state.exit()
	current_state = get_node(target) as BaseState
	current_state.enter()
	
