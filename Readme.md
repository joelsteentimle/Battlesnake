# Battlesnake
This repository contains the foundation of an Battlesnake player. It also includes
an very simple implementation some control logic.

To add an implementation just modify the `Move` function in `Snake.cs` (and `Create`
  if you'd like other settings for your snake).

## Running the player
The solution can be run as any ASP.NET site. If you're running it IIS-Express
(e.g. debugging in Visual Studio) remember that IIS-Express only binds to 120.0.0.1
so remember to make it bind to all ip addresses or set up a reverse proxy.
