Old scratched XNA/Monogame project of a top-down sandbox.
Contains:
- Menu system
- Tilemaps loading (i was creating maps in Tiled, then somehow converting the .tmx file to .xnb and loading it using normal content loader. Tile's id were global, thanks to tileset manager).
- Random objects generation without spawning multiple objects on same tile. (each object described in objectdata has a tile list that it can spawn on).
- World saving (to the binary file) & world loading.
- Character creation (colors of bodyparts only)
- Character saving (to the yaml file so player can modify it easly) & loading.
- Bad collisions system.
- In game menu.
- Xact audio engine.

If you find anything in here useful i'll be happy, do whatever you want.
