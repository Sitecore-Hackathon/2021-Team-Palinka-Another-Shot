# Hackathon Submission Entry form

## Team name
⟹ Team Palinka - Another shot

## Category
⟹ The best enhancement to the Sitecore Admin (XP) for Content Editors & Marketers

## Description
⟹ Advanced Sitecore Workbox (or Refurbished Workbox)

### Purpose of the module

- The main purpose of this module to have a new and cleaner UI for the workbox
- The new UI provides more Visual overview about Different items in different workflows
- The new UI provides more information about the items and about their workflow state/history than the previous one. 

## Video link
⟹ Demo Video

⟹ [Replace this Video link](#video-link)



## Pre-requisites and Dependencies

⟹ The submission requires the following softwares on the client machine:

- Docker Desktop on Windows, running in Windows Containers mode
- The module can be used on XM or XP configuration. (Please note, the current solution uses XP)
- No other program is required in order to spin up the container images

## Installation instructions
⟹ Installation instructions how to spin up the docker image  

**Notes:** You do not have to build Visual Studio solution or the React client applications, everything is done in Docker

**Possible issues around the Docker build**
- The MSBuild might not be able to restore nuget packages
  - Solution: Add "dns":["8.8.8.8"] to the Docker Engine Configuration
    - ![File](documentation/screenshots/file.png)
- The CM site shows 404 empty page
  - Solution: You might have to wait a bit more, because the CM container starts a bit slower.

**Installation steps**

- Clone this repository
- Copy a Sitecore Licence into the **.\docker\Licence** folder
- Call the **.\start-hackathon.ps1** script in the repository root in **PowerShell** console with **Administrator** priviliges
- Wait for the containers and wait for the CM site
- Once the https://cm.team-palinka.localhost is loaded, call the following commands in the powershell
- Call **dotnet tool restore** command
- Call **dotnet sitecore login --authority https://id.team-palinka.localhost --cm https://cm.team-palinka.localhost --allow-write true**  command
- A login page will be opened, please login with admin credentials and add access to the API. (Username: admin, password: b)
- Call **dotnet sitecore ser push** command
  - This will push the module related and some sample item into the Sitecore database.
- Open Sitecore Launchpad, navigate to the control panel, and populate **Solr Managed Schema** for all indexes
- Navigate to the control panel, and rebuild every search indexes in the **Indexing Manager**
- Start Advanced Sitecore Workbox on the Launchpad (Find next to the original Workbox Shortcut)
![File2](documentation/screenshots/file2.png)



## Usage instructions
⟹ Provide documentation about your module, how do the users use your module, where are things located, what do the icons mean, are there any secret shortcuts etc.

Include screenshots where necessary. You can add images to the `./images` folder and then link to them from your documentation:

![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")

You can embed images of different formats too:

![Deal With It](docs/images/deal-with-it.gif?raw=true "Deal With It")

And you can embed external images too:

![Random](https://thiscatdoesnotexist.com/)

## Comments
If you'd like to make additional comments that is important for your module entry.
