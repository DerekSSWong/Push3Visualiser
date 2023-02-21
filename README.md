# Introduction

Push 3.0 is a stack-based language aimed to be used in evolutionary computation. Its robust syntax makes it easy to procedually generate, recombine, and mutate programs.
This application aims to visualise the operation of the language, making it more intuitive to understand how each block of code is executed.


# Downloading the Visualiser

Download the contents within the Visualiser(Win) directory and run the executable within to start the visualiser.


# Visualiser Guide

![alt text](https://i.imgur.com/Rb5fgTL.png)

1. **Search Bar:** Allows the user to look up specific instructions.
2. **Instructions List:** Provides a comprehensive list of instructions that are included in the Visualiser, the Search Bar can be used to narrow down the list.
3. **Input Textbox:** The place where the the user can write their own program and have it be loaded into the Visualiser. A default program is provided to act as an immediate example of how a Push program works; it also gives the user something to experiment with without making them have to come up with a program on their own, which could prove to be difficult when one is just starting to learn the language.
4. **Forward Step Button:** Runs the program by a single step. Any items popped from stacks will quickly fade away, instead of outright disappearing, this gives the user an impression of which items are popped. If items are generated from the step, they are spawned at the top of the stack and quickly 'falls' to the bottom. The button is disabled during the process.
5. **Undo Button:** Reverts the most recent step of the program. Unlike the Forward Step button, no animation is played during the process due to limitations the code.
6. **Fast Forward Button:** Unlike its counterpart in the Interpreter, which immediately calculates the end result of the program, the FF button effectively moves the program a step forward as soon as the previous step finishes. This means all the animation involved in the steps will still be played out. While being a lot slower than the online interpreter at producing an end result, this method will be clearer at demonstrating to the user how the end result is produced. This method of implementation also allows the user to stop the fast-forwarding process at any time.
7. **Load Button:** Initially named the ”Parse” button, but was changed later due to some users not being familiar with the terminology. This button will attempt to convert what was written in the Input Textbox into either instructions or literals, and pushes them individually into the Exec Stack.
8. **Stack Displays:** Whenever a new item is pushed onto any stack, it will appear on the far left, and quickly moves its way to the right, stopping just before the previous top item in the stack, or the right edge of the display.
9. **Tutorial Button:** The Visualiser comes with a set of simple tutorials to introduce the basic concepts of a stack-based programming language, they roughly outline the definitions and properties of stacks, instructions, and types. Examples of each are also included, which are akin to loading the default program in the Input Textbox.
10. **Quit Button:** The Visualiser will attempt to open in fullscreen mode. In order to quit, the user can either click on the Quit Button, or press the escape key on their keyboard.
11. **Instruction Display:** Whenever the user clicks on an instruction in the Instruction List (2), the properties of the instruction will be displayed. It includes the number of parameters required from each stack, as well as a brief description of its function.
