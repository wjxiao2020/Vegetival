# Vegetival

## GAME DESIGN DOCUMENT

### 1. Characters
#### Player

Gordon: The player represents Gordon. He is a celebrity chef from a town, known for cooking great food that has tons of flavors. He is incredibly skilled with a spatula, and is able to cook various cuisines with no trouble at all. He is an incredibly talented individual.

#### Enemies

The enemies consist of a variety of vegetables, each having their own unique abilities. They can either shoot projectiles or have melee capabilities. On each level, the vegetable enemies will attack and charge towards the player in hopes of damaging/killing. We have implemented 3 levels and here are some of the characters.

Potato Platoon: The first enemy that the player will face in level 1. It has two health bars, and its primary attack consists of hitting and knocking back the player, dealing considerable damage. It also has an ability where it is able to run the player down at an incredible speed, though with a cool down. Upon reaching the second health bar, the potato enemy will enrage and turn red, jumping up high to the sky and slamming to the ground dealing area damage. It is imperative to destroy potato platoon quickly during stage 2 as it can pose a significant threat towards the player.

Broccoli Baron: The boss enemy from level 2. The Broccoli wields a banana gun and shoots bullets towards the player with a cool down time. This is the first enemy that the player will encounter that has ranged attacks. Additionally, it will also spawn mini-broccolis periodically that will explode upon contact with the player. The combination of fast agile attacks along with self-exploding minions makes it extremely difficult for the player to navigate. The player will need to kill the minions in time before it deals damage.

Lettuce Lord: The boss enemy from level 3. The most powerful enemy that the player will encounter in this game. He will hurl fireballs at the player in intervals. He will also summon both of the previous enemies in a rage as his health is depleted. Potato and Broccoli will both spawn to help the lettuce lord attack the player. When all 3 bosses are present, it becomes incredibly difficult for the player to fight through. The player will have to be agile and attack them one by one.

#### NPC

Mousey: Mousey is a NPC in this game world. He arrived on this planet long ago and has become a helper in the arena for all challengers. He will offer aid to the player. Upon walking up to mousey (the player), mousey will give one of two potions, with one giving health aid and the other as a speed pickup (that can be stored later). He is a very helpful person to have.

### 2. Story/Narrative

One day, Gordon suddenly awakens and finds himself lying on the ground full of coarse sands and bones. As he stands up, he sees the surroundings in the mist vaguely resemble an arena, with him directly inside of it. Suddenly, shouts and cheers erupted all around him, and as the mist cleared, he glanced toward the source of the noises - the stands were full of human sized vegetables… 

“Silence!” At the far end of the arena, seated on a grand throne, is the imposing figure of the Broccoli King. His piercing eyes fixate on Gordon. "Earth person!” his voice echoing off the ancient stones. "You are the chosen for our 223rd Vege’tival, a time-honored tradition of our people!"

Gordon, still grappling with the surreal scene before him, can only stutter, "Vege’tival?"

The Broccoli King, with a tone of regal condescension, explains, "Yes, Earthling. You will face our most formidable warriors. Defeat them, and you earn your ticket back to your world. Fail, and well..."

As Gordon's eyes dart around, sizing up the arena, he spots the Vege-warriors emerging from beneath the stands – each more menacing and bizarre than the last, armed with weapons.

The air is thick with anticipation. The crowd's shouts crescendo into a deafening roar as the Broccoli King declares, "Let the Vege’tival begin!"

With a deep breath, Gordon clenches his fists. He knows he must use every ounce of courage and strength to survive and find his way back home. He must use his knowledge as a chef and his will as a warrior. Gordon turns around and faces the enemies as the first warrior approaches…

### 3. Game World

The Game takes place on another planet, where vegetables rule the entire place. They are a highly developed galactic species with the capabilities to interstellar travel. One tradition they have is to host the “Vege’tival”  yearly, where they abduct an individual to compete in their arena. But little did they know that this time - they abducted a highly skilled chef. The game takes place in this arena, a large circular gated area with surrounding stands. There are tunnels that connect to the dungeons of the arena where the vegetable warriors will emerge. In the center of the arena, there is a portal. Upon stepping in, the player will be transferred to the next level of the arena - only after beating all the enemies of the current level. 

### 4. Gameplay

The game can be classified into a casual FPS game. The theme rivals those that we see online, such as the ever-famous PalWorld that just came out a couple of months ago. 

#### Gameplay Objective: 

The player controlling chef Gordon will be required to defeat enemies in each level and advance to the next one. Each vegetable warrior will fiercely attack, and the player will have to stay alive, dodge attacks, and fight back constantly. 

#### Long-term Gameplay Objective: 

To return home to earth, the player will have to defeat every level set up in the “Vege’tival”, and that includes beating the final boss, Lettuce Lord. Until then, the player will have to stay in the arena, survive through each boss and gain pick-ups that can boost health and speed.

### 5. Game Mechanics

#### Player (Gordon):

Attack: From a spatula, the player can fire various cuisine related “bullets” from the spatula to attack the enemies by pressing/holding left click at a set speed. The bullets, upon contact with the enemy, will deal damage.  

Movement WASD & Space: The basic movement set of any FPS game. Utilize these movement capabilities to evade incoming attacks from enemies in the arena as well as position yourself strategically to fight.

Pickups: Picking up and using various health/buffs throughout the gameplay by pressing e and f. Respective prompts will show up to signal the player can use those mechanics.

#### Enemies (Vegetable Warriors):

Attack: Each vegetable warrior uses their unique attack pattern to challenge the player, in the forms of melee attacks or projectile attacks, as well as minion attacks.

Movement: similarly, enemies will also move towards/away from the player to perform strategic attacks. Some enemies may be able to even jump and slam the ground, as well as magic attacks.

### 6. Items, Loots, and Power-ups

There are two types of pickups in the game, one being a health potion and the other being a speed pickup. These are distinguished from a red/blue glow.

Within each level, there will be one health potion and one speed potion on spawn. Upon defeating enemies, they will also drop the speed pickup. 

Mousey glows either red/blue at 5 second intervals, signaling what exact pickups are gonna spawn when the player approaches mousey. (Red being health, Blue being speed).

#### Health Buff: 

Player will gain an amount of health in the level

#### Speed Buff: 

Speed pickup will be stored as a value, and can be used by pressing F later.

### 7. Game Rules

#### 1. Winning a Battle/Level:

Defeat all vegetable warriors: To win a battle, the player must deplete the health of all vegetable warriors that will spawn. Each level can contain different warriors and the player must defeat them 

#### 2. Losing a Battle:

Health Depletion: The player loses if Gordon's health bar is completely depleted.

No Continues: If Gordon loses a battle, the player must start the battle over from the beginning (the same level).

#### 3. Health and Damage:

Health Bar: Gordon has a fixed health bar that shows the amount of damage he can take. 

Damage Taken: Gordon takes damage when hit by vegetable warrior attacks. The amount of damage depends on the type of the warrior and what he is hit by.

Healing: The player can pick up health pickups to heal throughout the level

#### 4. Game Completion:

Defeat All Levels: The game is completed after beating all levels. Gordon wins the tournament and goes home!

### 8. Target Audience

Audience of all ages since the game theme is relatively tame and the artwork style wouldn’t be too gory. Of course, casual style games target a younger audience, but this shouldn't stop anyone from enjoying a relaxing FPS that’s not as intensive as some of the more serious themes out there. We believe that the main demographic would be people between 12-25. 
