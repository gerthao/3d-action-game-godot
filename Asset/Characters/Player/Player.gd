class_name Player

extends CharacterBody3D

@onready var visual: Node3D = $Visual
@onready var animation_player: AnimationPlayer = $Visual/AnimationPlayer
@onready var footstep_vfx: GPUParticles3D = $Visual/VFX/Footstep_GPUParticles3D

const SPEED = 5.0
const ANGULAR_SPEED = 7.0

var coins: int:
	set(value):
		coins = value
		emit_signal("coin_number_updated", coins)

signal coin_number_updated(value: int)

func _physics_process(delta: float) -> void:
	var input_dir := Input.get_vector("move_left", "move_right", "move_up", "move_down").rotated(-45)
	var direction := (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	if direction:
		velocity.x = direction.x * SPEED
		velocity.z = direction.z * SPEED
		animation_player.play("LittleAdventurerAndie_Run")
		footstep_vfx.emitting = true
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		velocity.z = move_toward(velocity.z, 0, SPEED)
		animation_player.play("LittleAdventurerAndie_Idel")
		footstep_vfx.emitting = false
		
	if velocity.length() > 0.2:
		var look_dir = Vector2(velocity.z, velocity.x)
		visual.rotation.y = lerp_angle(visual.rotation.y, look_dir.angle(), ANGULAR_SPEED * delta)

	move_and_slide()


func add_coin(value: int) -> void:
	coins += value
	print(coins)
