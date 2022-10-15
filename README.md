# Satisfactory.Calculator

This is a sandbox for playing around with Satisfactory calculations. The aim here is to build a system where you can load all available recipies, and it can suggest the best recipies based on your selected inputs.

## Dependencies

* [GraphViz](https://www.graphviz.org/download/) - Renderer for graphs represented in the DOT file format. You will need this installed.

## Configuration
We currently have two configuration values in the app settings:

* `output-directory` - The directory to generate DOT files and their associated rendering.
* `dot-exe` - Path to the graphviz executable.

## How to test

Run `dotnet test` from the root directory.

Currently this application is only tests. Running the tests will generate a number of DOT files and images of a few pre-configured recipes.

## Output

Output currently looks like this:

![Diagram](/assets/Crystal Oscillator.png)