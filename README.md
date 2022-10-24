# Satisfactory.Calculator

[Satisfactory](https://www.satisfactorygame.com/) is a factory simulation game. Mine stuff, smelt stuff, construct stuff, use the constructed stuff to construct more stuff. Rinse and repeat.

This is a sandbox for playing around with Satisfactory calculations. The aim here is to build a system where you can load all available recipies, and it can suggest the best recipies based on your selected inputs.

Currently we generate a DOT graph from a simple model in a C# application, and then feed this into GraphViz which generates an image.

## Existing Work

There's already an excellent implementation of at [satisfactory-calculator.com/](https://satisfactory-calculator.com/) and this project is not affiliated with this in any way. I'm using this project as an environment to play with tree / graph walking algorithms. 

## Dependencies

* [GraphViz](https://www.graphviz.org/download/) - Renderer for graphs represented in the DOT file format. This is included via NuGet package GraphVizNet.

## Configuration
We currently have two configuration values in the app settings:

* `output-directory` - The directory to generate DOT files and their associated rendering.
* `dot-exe` - Path to the graphviz executable.

## How to test

Run `dotnet test` from the root directory.

Currently this application is only tests, and only tested on windows. Running the tests will generate a number of DOT files and images of a few pre-configured recipes.

## Output

Output currently looks like this:

![Diagram](https://raw.githubusercontent.com/TristanRhodes/Satisfactory.Calculator/master/assets/Reinforced%20Iron%20Plate.png)
