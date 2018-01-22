Developed By: Michael Dessureault

Create a puzzle through the inspector

The puzzle can be made up of 2 different "puzzle object types"; Rotation and PressurePad

Note: The Puzzle scripts are being invoked with SendMessage from the related Mixin object
      This is changeable with another way of invoking the methods within the puzzle script
	  Methods: Rotate and Pressed

How to use it:

 1. Create 1 or many Puzzle GameObjects by adding the Puzzle script onto any GameObject

 2. Select which type of object you want either Rotation or PressurePad
	a) If PressurePad is selected add the IsTriggerable script onto the GameObject
		Populate the IsTriggerable values:
			i)   Key: Puzzle (This is not required to be filled in, side note: a Puzzle script will be added to the GameObject when the key is filled in with Puzzle (not case sensitive) if it's not found)
			ii)  Call Back : Pressed
			iii) Touch Type : IsaPlayer
	b) If Rotation is selected add the IsRotateable script onto the GameObject
		Populate the IsRotateable values:
			i)  Key: Puzzle
			ii) Call Back: Rotate

 2. Select which type of object you want either Rotation or PressurePad
	
    a) If PressurePad is selected add the IsTriggerable script onto the GameObject (can add the PressurePadDownAction script on the PressurePad for a small visual effect of it going down for demo purposes)
		
	Populate the IsTriggerable values:
			
	i)   Key: Puzzle (This is not required to be filled in, side note: a Puzzle script will be added to the GameObject when the key is filled in with Puzzle (not case sensitive) if it's not found)
			
	ii)  Call Back : Pressed
			
	iii) Touch Type : IsaPlayer
	
    b) If Rotation is selected add the IsRotateable script onto the GameObject
		
	Populate the IsRotateable values:
			
	i)  Key: Puzzle
			
	ii) Call Back: Rotate


 2. Have a Gameobject that is contains the PuzzleSolver script, this is the object that will have a solved action occur when the puzzle is completed.  
	
    (Example: A door has the PuzzleSolver and when the puzzle is completed the door opens)


 3. Update the set the Required Solved State field to what is wanted.  
    
    i) PressurePad objects will see a bool for on and off
    
    ii) Rotation objects will see an enum of 4 rotation states (Font, Right, Back, Left)


 4. Add a SolvedAction to PuzzleSolver GameObject
 
 

 5. Run the game and solve the puzzle

 SolvedActions: 
  There are currently 2 solved actions PuzzleSolvedColourChangeAction and PuzzleSolvedDisableAction.
  A solved action is easily creatable by making a new script and extending from IPuzzleSolverAction
