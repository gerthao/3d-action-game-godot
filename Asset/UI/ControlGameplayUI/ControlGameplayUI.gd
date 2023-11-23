class_name ControlGameplayUI

extends Control

@onready var coin_label: Label = $CoinHBoxContainer/CoinLabel

@export var player: Player

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	player.coin_number_updated.connect(update_coin_label)

func update_coin_label(value: int) -> void:
	coin_label.text = str(value)
