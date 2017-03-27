# Battlesnake
This repository contains the foundation of an Battlesnake player. It also includes
a very simple implementation of some control logic.

To add an implementation just modify the `Move` function in `Snake.cs` (and `Create`
  if you'd like other settings for your snake).

## Running the player
The 'Battlesnake' project can be run as any ASP.NET site. If you're running it IIS-Express
(e.g. debugging in Visual Studio) remember that IIS-Express only binds to 120.0.0.1
so remember to make it bind to all ip addresses or set up a reverse proxy.

Alternatively, you can run the 'SelfHosted' project which is a console application.

## API
This project aims to encapsulate the battlesnake.io REST API and to provide basic datatype mappings for it.

Also, we provide some convenience functions, most notably Coordinates, Directions, and 'Peek' on the 'Board' structure addition to GameStatus which 
allows for convenient analysis of the board using coordinates.  
