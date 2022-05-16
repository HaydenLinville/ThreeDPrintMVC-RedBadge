# ThreeDPrintMVC README

ThreeDPrint MVC is an MVC that allows you to create, look up, update, and delete
 3D Printers, Material, and Custom Settings.

## Features
- Full CRUD of Printers, Material, and Settings 
- Able to assign multiple Settings to your Printers which include Material. 
- Contains seeded content of some example Printers, Material, and Settings.


## Seeding instructions
To obtain the seed content for the api:
1. Within visual studio open up tools
2. Go to NuGet Package Manager
```sh
Package Manager Console
```
3. Within the default project in the Package Manager Console at the top switch to:
```sh
Data
```
4. Type in: 
```sh
Update-Database
```

## How to use
1. First register an account by clicking the "Register" button at the top of the screen. If on phone click on the "Login" dropdown menue then click Register.

2. After you register you can create your own Printer or Material by clicking the "Add Printer" Or "Add Material" button. 

3. There you will be able to fill out information about your Printer and/or Material.

4. Once done filling out the information click "Create".

5. After you have created a Printer AND at least one Material you can create custom settings by clicking the "Create Settings" button at the top of your screen, the "Create Custom Settings" at the bottom of the landing page, or the "Add New Custom Settings" button on the Printer detail page. 



## Info
You can find more gernal information about Printers and Material by clicking on the more info buttons on the landing page.

## Examples
You can view the examples by clicking on the "See Printer Examples" or "See Material Examples" buttons on the landing page. 
