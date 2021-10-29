# Quick-Maths

## Overview
Mental arithmetic app made in Unity. The concept was loosely based on [Nintendo's Brain Age](https://en.wikipedia.org/wiki/Brain_Age) series, but with some more customization for the player.  
Addition, subtraction, multiplication, and division questions are randomly generated for the player to answer as quickly as possible, under various rulesets based on selected play mode and difficulty levels.

### Game modes
3 game modes:

**Classic Mode** - Answer all the questions as quickly as possible.  
**Timed Mode** - Answer as many questions as possible before time runs out.  
**Challenge Mode** - Answer questions until you get one wrong, or until time runs out. A correct answer will add 10 seconds to the clock. Questions increase in difficulty as the game goes on.

### Settings

#### Game settings
* increase/decrease number of questions per Classic Mode session
* increase/decrease time limit per Timed Mode session
* toggle clock display on/off

#### Difficulty settings
* increase/decrease difficulty of each operation individually (5 difficulty levels)
  * e.g. Lv. 5 addition, Lv. 3 subtraction, Lv. 4 multiplication, Lv. 2 division, etc.
* toggle each operator on/off
  * e.g. addition and multiplication questions only, everything except subtraction questions, division questions only, etc.

---

## Notes + other details

### Graphics (a.k.a. I'm not an artist)
I wanted to go for a more back-to-school oriented theme so I decided on a chalkboard-style aesthetic.
* background - just a single image for the whole game, made using Photoshop's Clouds filter. Ideally I'd like to randomly generate a clouds pattern using ShaderGraph though
* fonts - 100% free fonts found on dafont.com. Overlayed with an alpha map to give them more of a chalk look to it
* settings menu sliders, checkboxes, buttons - done in Photoshop
* built-in keyboard buttons - Unity defaults :)
