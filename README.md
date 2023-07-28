# Crazy intersection problem solver
The program solves the specific problem called "crazy intersections". The problem consists of of an NxN map with different cars on it. Each car can have a different size and can move either on the Vertical or on the Horizontal axis. There is a special car on the Hozrizontal axis which is usually placed in the leftmost side of the map and in line with the car there is an exit in the rightmost side of the map. The goal of the game is to get this special car to the exit, but in most situations there are other cars in the way. These cars have to be moved in a special order in order to free up the way for the special car to get to the right side of the map.

<img src="/Showcase/problem.png">

This algorithm solves this problem. For the implementation Tree structures are used. Each node is a move on a specific car in a specific direction and the result can be found by two search methods:
- BFS search
- DFS search

Both of these funtions are implemented.

## Documentation
More in-depth documentation of the program and the algorithm can be found in the `Documentation` folder.

## Input
Input for the map is hardcoded, but can be defined in the following format:

`(({special car's color} {size} {position Y} {position X} {orientation})({car's color} {size} {position Y} {position X} {orientation})({car's color} {size} {position Y} {position X} {orientation}))`

For example:

`((red 2 3 3 h)(orange 3 2 5 v)(yellow 3 1 3 h)(pink 4 1 2 v)(green 5 5 2 h))`

## Output
If the map is solvable then the output of the program will be both BFS and DFS solutions and the movement of each car in order. (The directions are in slovak)
- VLAVO = LEFT
- VPRAVO = RIGHT
- HORE = UP
- DOLE = DOWN

The steps will contain the movement direction, color of the car and the amount of steps to move, in the following format:
`{direction}({color} {numOfSteps})`

For example:

`VPRAVO(orange 1)`

## Result
<img src="/Showcase/output.png">