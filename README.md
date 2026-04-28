# GDIM33 Vertical Slice
## Milestone 1 Devlog
- One of the visual scripting graph in my game controls the Player movement. I have two nodes, Get AxisRaw Horizontal and Get AxisRaw Vertical that checks for horizontal and vertical
input from the player when they press WASD or the arrow keys. I plugged these two nodes into a Vector2 node as the X and Y to make them into Vector2 movement. 
I multiplied this Vector 2 node with the speed variable from the get Variable Node. This Vector2 movement is then plugged as input for the Rigidbody2d Set Velocity so that the Player will actually move. 
I have the OnFixedUpdate connecting to the Rigidbody2D so that the movement and physics is updated. 

Pick 1 Visual Scripting Graph in your game and explain how it works. This answer should be about a paragraph.
Update your break-down with the state machine system you're using in your game, and attach your new break-down in your Devlog. 
Then, explain what you updated about your break-down, how your state machine works, and how your state machine is related to OTHER systems in your game. 
This answer may take about 2 paragraphs.

- My state machine has two states: Following Player and Attacking. In the Following Player state. I get the position of 
the cat and the positino of the Player gameobject by subtracting Player position - Cat position and then input the result of this
into a Vector2 node and compared if it was greater than stopDistance. If the Player position - Cat position is greater then stopDistance 
then the Cat would follow the player. I executed this thorugh the Rigidbody2D set Velocity. 
- In the Attacking state, I set the Rigidbody2D velocity to zero because when it is attacking the enemy, it should not be moving. 
I also invoked the method Attack() from my C# script Cat so that the Cat will attack the enemy
- In my Following to Attack transition, I got the distance from enemy through Transform Get Position nodes for the enemy gameObject and
cat gameobject. I plugged it into a Vector2 distance and checked if it is less than attackRange using the Get Variable node. If it is true, 
the transition is triggered and in the Attack to Following transition, I put the same nodes but in the if node, I connected the false to the trigger transition. 
So if the distance from the enemy is not less than attackRange, the transition will trigger. 


## Milestone 2 Devlog
Milestone 2 Devlog goes here.
## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
- Cite any external assets used here!
