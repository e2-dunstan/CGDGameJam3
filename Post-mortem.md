# Faster Than You

#### The Boomers: Ellie, Alex, Dan, Ben, Lewis, Sol
#### Audio: George Dyuddridge, Hugo Rickaby, Jack Grint, Joe Cooper, Alex Paxman

#
## Teamwork and Project Development

### GitHub

For the third jam, GitHub was used much better. From the beginning of the jam issues were created for the core systems and potential juice. These were tagged appropriately depending on priority and then assigned to members of the group. The utilisation of the issue number in commit messages was also much better with most commits being related to an existing issue. The projects board was also set up to automatically manage the movement of issues between columns based on whether they were being worked on or if they were closed. Milestones weren't used in this jam mainly due to the core features being implemented within the first few days of the jam beginning.

### Communication

Communication for this jam was extremely good. There were scenarios in which people couldn't make it in but every time this was the case; the group was made aware via Slack. Meeting times were decided in advance via Slack and once the group met up, the jam was the focus for the day and everyone gave their full effort. At this point, the group understands where each others strengths lie and less time was wasted overall by playing to them.

#
## The Game

### Code

For this jam, having complicated systems being part of the gameplay wasn't the key focus. The group wanted to focus on making the player feel uneasy in a dark maze, surrounded by monsters so the priority was tweaking the lighting to give the intended atmosphere with spatial sound to add the tension of the monsters that surround you. This resulted in the core systems being simple and quick to implement, such as the top-down follow camera and player controller. Although this was the case, the enemy system was made with a fairly robust pathing/chasing system to ensure that the build of tension through sound had a good visual payoff.

### Design

Level Design (maze generator) - Lewis

All of the characters and their animations were sourced from Mixamo. The advantage of using this is that all of the characters were pre-rigged and set up to be easily imported into the game. The only elbow grease from our end was setting up the animator controllers for each character and controlling them via scripts. 

<p float="left">
  <img src="/Jam 3 post mortem/enemy model.PNG" width="400" />
  <img src="/Jam 3 post mortem/enemy skeleton.PNG" width="400" />
</p>

The textures were found on the Unity Asset Store. It was important that those textures had normal maps at the very least as the way light interacts with them was another contributer to the overall atmosphere of the game.

UI - Sol

Effects (wall torches, footsteps, etc.) - Sol

Keys - Alex, Ben, Sol (emphasise communication on this issue, plus use of colours)
The key system at is base is very simple. Two arrays, one of the keys themselves and one of the lights relating to those keys. Once a key is collected the key calls back to its manager to disable that key in the scene and turn on the appropriate light. Once all keys have been collected a function is then called which the other systems can hook into to play cutscenes, end the game, etc.

Enemies - Dan

Flashlight - Ben

#
## Improvements

Level - more variety, static objects like clutter

Whilst the level that the group created was good, it needed to be handcrafted from the result of the maze generator. This was due to the purpose of the generator being revolved around just making a complex maze with one entrance and one exit. To give extra variety to the game without requiring a lot of extra time handcrafting levels; tweaking the generator so that it also automatically placed a predefined set of rooms would add more variety to the game. Also fleshing out rooms and corridors in the game with clutter objects will add a bit more life to the level. Physics enabled objects could also add another dimension to the game, allowing the player to block corridors with a bookcase for example.

Another improvement would involve hinting the player about key locations and potentially having a key counter to see how many you need to collect. The group wanted to avoid just putting lots of UI elements for these because it would ruin/belittle the atmosphere we tried to create via the lighting and sounds.

Enemy improvements like more complex behaviours and variety

#
## Gameplay

Video and pictures here
