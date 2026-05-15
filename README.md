# GDIM33 Vertical Slice
## Milestone 1 Devlog
- One of the visual scripting graph in my game controls the Player movement. I have two nodes, Get AxisRaw Horizontal and Get AxisRaw Vertical that checks for horizontal and vertical
input from the player when they press WASD or the arrow keys. I plugged these two nodes into a Vector2 node as the X and Y to make them into Vector2 movement. 
I multiplied this Vector 2 node with the speed variable from the get Variable Node. This Vector2 movement is then plugged as input for the Rigidbody2d Set Velocity so that the Player will actually move. 
I have the OnFixedUpdate connecting to the Rigidbody2D so that the movement and physics is updated. 

<img width="1476" height="1104" alt="Screenshot 2026-04-28 164432" src="https://github.com/user-attachments/assets/5b8b7b69-ca45-44b5-b688-40fafe7d045b" />

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
### 1
Big steps:
My complicated gameplay feature is a powerup item that increases attack stat on the Cat GameObject. 
- Create a scriptableObject that has the increase attack stat
- Implement how the scriptableObject will act on the Player and Cat through coding. 
- Text indication that the powerup item worked

Smaller steps:
- Create a variable on the scriptableObject script called plusPowerUp
- Create a method in the Item script that calls the plusPowerUp variable from the SO script and increases attack damage when powerup Item is consumed. 
- Create a method in Cat script that subscribes to the event that Item script calls when an item is consumed and add the attack buff onto the cat stat. Check in the inspector if the damage stat of cat is increased. 
- In the method, implement that the powerup can only be used within a certain duration. 
- Hook up the item script onto the powerup item in Unity. 
- Create text in Unity that displays attack damage
- Update attack damage text when powerup Item is consumed to check if the powerup works and if it the buff sucessfully stops after a while.

### 2
The task steps break-down activity helped me build a feature for this Milestone because I was able to break it into numerous small steps that I could check after each step whether or 
not I implemented it correctly. Also it made building the features feel less overwelming. To improve my break-downs to be more helpful in the future, I would try to be even more 
specific as I had some steps that I overlooked while I was doing the steps. The quiz question form W5 also helped me implement my chosen Unity system because it made me think and
plan where I would be using ScriptableObject in my game.

### 3 
I am bridging visual scripting and code in my game through my state machine. On my Cat gameObject, it has the Attacking state graph. In the Attacking state graph,
I call upon a C# method Attack() from my Cat script. This C# method Attack() serves in my architecture as the controller part of the Cat while the state graph handles the transition 
and movement of the cat. 
<img width="1364" height="733" alt="Screenshot 2026-05-08 182111" src="https://github.com/user-attachments/assets/b73ca0ba-6ece-4b28-92db-b318283b6b21" />

### 4
I want to be graded on ScriptableObjects as my Unity system. The scriptableObjects are the items on the map (The potion and the bowl).

## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
[Cat Take Damage Sound](https://uppbeat.io/sfx/cat-threatening-growl/13302/32826)
[Player Take Damage Sound](https://pixabay.com/sound-effects/film-special-effects-damage-blowhole-402072/)
[Enemy Take Damage Sound](https://pixabay.com/sound-effects/horror-ghost-horror-sound-382709/)