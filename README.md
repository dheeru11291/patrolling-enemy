

features::

	Player Behavior:
	 1.Player can move using the keyboard (W / A / S / D) and arrow keys(used Get Axis function).
	 2.Player can collect stars in the scene.
	 3.On collecting a star: Increase the score.
	
	enemy behavior:
	 1.enemy moves around the area boundary until player enters in the area zone.
	 2.then enemy chase player to eliminate with constant acceleration velocity.
	 3.when the player collide with player game over panel appear.

	star behavior:
	 1.player collide with star its deactivate in the hierarchy window and activate after two sec.
	 2.I use coroutine functionalities not prefab property.
	 3.Star activate in the area zone randomly.
