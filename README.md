# Quick-Maths

## About
This is my first game that I don't consider to be a direct clone of a pre-existing game, and the first game that I intend to publish to mobile app stores.

---

## Overview
This is a game that tests the player's mental arithmetic skills. The concept was loosely based on [Nintendo's Brain Age](https://en.wikipedia.org/wiki/Brain_Age) series, with more customization for the player.  
Addition, subtraction, multiplication, and division questions are randomly generated for the player to answer, with the objective of getting as many correct and as quickly as possible.

### Game modes
The game features 3 game modes:

**Classic Mode** - Answer all the questions as quickly as possible  
**Timed Mode** - Answer as many questions as possible before time runs out.  
**Challenge Mode** - Answer questions until you get one wrong, or until time runs out. A correct answer will add 10 seconds to the clock. Questions increase in difficulty as the game goes on.

### Settings
The player has a number of settings to customize:

#### Game settings
* increase/decrease number of questions per Classic Mode session
* increase/decrease time limit per Timed Mode session
* toggle clock display on/off

#### Difficulty settings
* increase/decrease difficulty of each operation individually (5 difficulty levels)
  * e.g. Lv. 5 addition, Lv. 3 subtraction, Lv. 4 multiplication, Lv. 2 division, etc.
* toggle each difficulty on/off
  * e.g. addition and multiplication only, everything except subtraction, division only, etc.

---

## Notes + other details

### Graphics (a.k.a. I'm not an artist)
I wanted to go for a more back-to-school oriented theme so I decided on a chalkboard-style aesthetic.
* background - just a single image for the whole game, made using Photoshop's Clouds filter. Ideally I'd like to randomly generate a clouds pattern using ShaderGraph though
* fonts - had to find two 100% free fonts that fit well with the theme. Additionally I made an alpha map to add onto them to give them more of a chalk look to it
* settings menu sliders, checkboxes, buttons - done in Photoshop
* built-in keyboard buttons - Unity defaults :)
