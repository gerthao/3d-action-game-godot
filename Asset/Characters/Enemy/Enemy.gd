class_name Enemy
extends CharacterBody3D


const SPEED = 2.0

@onready var navigation_agent: NavigationAgent3D = $NavigationAgent3D
@onready var visual: Node3D = $Visual
@onready var animation_player: AnimationPlayer = $Visual/AnimationPlayer
@onready var player: Player

var stop_distance: float = 2.2
var direction: Vector3


func _ready() -> void:
	player = get_tree().get_first_node_in_group("player")


func _physics_process(delta: float) -> void:
	navigation_agent.target_position = player.global_position
	
	direction = (navigation_agent.get_next_path_position() - global_position).normalized()

	if is_close_to_player():
		animation_player.play("NPC_01_IDLE")
		return
	
	velocity = velocity.lerp(direction * SPEED, delta)
	animation_player.play("NPC_01_WALK")
	
	if is_moving():
		var look_direction = Vector2(velocity.z, velocity.x)
		visual.rotation.y = look_direction.angle()
	
	move_and_slide()


func is_close_to_player() -> bool:
	return navigation_agent.distance_to_target() < stop_distance


func is_moving() -> bool:
	return velocity.length() > 0.2
