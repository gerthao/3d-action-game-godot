class_name CollectableCoin

extends Node3D

@onready var visual: Node3D = $Visual
@onready var pickup_vfx: GPUParticles3D = $VFXNode
@onready var animation_player: AnimationPlayer = $Visual/AnimationPlayer

const COIN_VALUE = 1
var rotation_speed = 1.0

func _process(delta: float) -> void:
	visual.rotate_y(rotation_speed * delta)
	
	if !visual.visible && !pickup_vfx.emitting:
		queue_free() 


func _on_area_3d_body_entered(body: Node3D) -> void:
	if body.is_in_group("player"):
		pickup_vfx.emitting = true
		animation_player.play("collected")
		rotation_speed = 10.0
		body.add_coin(COIN_VALUE)
