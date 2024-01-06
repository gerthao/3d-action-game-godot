class_name Player

extends CharacterBody3D

@onready var visual: Node3D = $Visual
@onready var animation_player: AnimationPlayer = $Visual/AnimationPlayer
@onready var footstep_vfx: GPUParticles3D = $Visual/VFX/Footstep_GPUParticles3D

var direction: Vector3
var can_slide: bool

const SPEED = 5.0
const ANGULAR_SPEED = 7.0

var coins: int:
	set(value):
		coins = value
		emit_signal("coin_number_updated", coins)

signal coin_number_updated(value: int)

func _physics_process(delta: float) -> void:
	var input_dir := Input.get_vector("move_left", "move_right", "move_up", "move_down").rotated(-45)
	direction = (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()

	can_slide = Input.is_action_just_pressed('slide')

	move_and_slide()


func add_coin(value: int) -> void:
	coins += value
	print(coins)
